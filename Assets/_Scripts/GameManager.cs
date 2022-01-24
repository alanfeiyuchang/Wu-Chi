using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject TopPlayer;
    [SerializeField] private GameObject BottomPlayer;
    [SerializeField] private UIController UICon;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Transform backgroundTrans;
    private CharacterController2d T_CharControl;
    private CharacterController2d B_CharControl;
    private PlayerMovement T_playerMove;
    private PlayerMovement B_playerMove;
    private CharacterFollow T_characterFollow;
    private CharacterFollow B_characterFollow;
    public int CharacterInControl;
    private float timeElapesd;
    public static GameManager instance;

    Vector3 checkPointPos;

    [SerializeField] Color alittleGray;
    private void Awake()
    {
        instance = this;
    }

    public enum GameState
    {
        Starting,
        Playing,
        Dead,
        Pausing
    }

    public GameState currentState = GameState.Playing;

    // Start is called before the first frame update
    void Start()
    {
        T_CharControl = TopPlayer.GetComponent<CharacterController2d>();
        B_CharControl = BottomPlayer.GetComponent<CharacterController2d>();
        T_playerMove = TopPlayer.GetComponent<PlayerMovement>();
        B_playerMove = BottomPlayer.GetComponent<PlayerMovement>();
        T_characterFollow = TopPlayer.GetComponent<CharacterFollow>();
        B_characterFollow = BottomPlayer.GetComponent<CharacterFollow>();
        //CharacterInControl = 0;
        //SwitchControl();
        InitialControl();
        whiteMaterial.SetFloat("_Fade", 1);
        blackMaterial.SetFloat("_Fade", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*if (CharacterInControl == 0)
            {
                CharacterInControl = 1;
            }
            else
            {
                CharacterInControl = 0;
            }*/
            SwitchControl();
        }

        //control the ui menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
                SwitchGameState(GameState.Pausing);
            else if (currentState == GameState.Pausing)
                SwitchGameState(GameState.Playing);
        }

        backgroundTrans.position = new Vector3(TopPlayer.transform.position.x, 0f, 10f);
    }

    public void SwitchGameState(GameState state)
    {
        currentState = state;
        switch (state)
        {
            case GameState.Starting:
                break;
            case GameState.Playing:
                UICon.Resume();
                break;
            case GameState.Dead:
                //UICon.Death();
                Die();
                break;
            case GameState.Pausing:
                UICon.Pause();
                break;
            default:
                break;
        }
    }

    private void DisableCharacterController()
    {
        T_CharControl.enabled = false;
        T_playerMove.enabled = false;
        B_playerMove.enabled = false;
        B_CharControl.enabled = false;
    }

    void InitialControl()
    {
        if (CharacterInControl == 0)
        {
            //TopPlayer.GetComponent<CapsuleCollider2D>().enabled = true;
            //BottomPlayer.GetComponent<CapsuleCollider2D>().enabled = false;
            T_CharControl.enabled = true;
            T_playerMove.enabled = true;
            T_characterFollow.enabled = false;
            B_CharControl.enabled = false;
            B_playerMove.enabled = false;
            B_characterFollow.enabled = true;
            
        }
        else
        {
            T_CharControl.enabled = false;
            T_playerMove.enabled = false;
            T_characterFollow.enabled = true;
            B_CharControl.enabled = true;
            B_playerMove.enabled = true;
            B_characterFollow.enabled = false;
            //TopPlayer.GetComponent<CapsuleCollider2D>().enabled = false;
            //BottomPlayer.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

    void SwitchControl()
    {
        bool canSwitch = false;

        if (CharacterInControl == 0)
        {
            canSwitch = BottomPlayer.GetComponent<PlayerCollider>().CanSwitch();
        }
        else if (CharacterInControl == 1)
        {
            canSwitch = TopPlayer.GetComponent<PlayerCollider>().CanSwitch();
        }

        if (canSwitch)
        {
            CharacterInControl = (CharacterInControl + 1) % 2;
            T_CharControl.enabled = !T_CharControl.isActiveAndEnabled;
            T_playerMove.enabled = !T_playerMove.isActiveAndEnabled;
            T_characterFollow.enabled = !T_characterFollow.isActiveAndEnabled;
            B_CharControl.enabled = !B_CharControl.isActiveAndEnabled;
            B_playerMove.enabled = !B_playerMove.isActiveAndEnabled;
            B_characterFollow.enabled = !B_characterFollow.isActiveAndEnabled;
            //TopPlayer.GetComponent<CapsuleCollider2D>().enabled = !TopPlayer.GetComponent<CapsuleCollider2D>().enabled;
            //BottomPlayer.GetComponent<CapsuleCollider2D>().enabled = !BottomPlayer.GetComponent<CapsuleCollider2D>().enabled;
            if (CharacterInControl == 0)
            {
                TopPlayer.GetComponent<Rigidbody2D>().velocity = BottomPlayer.GetComponent<Rigidbody2D>().velocity;
                StartCoroutine(SwitchColor(0.5f, false));
            }
                
            else
            {
                StartCoroutine(SwitchColor(0.5f, true));
                BottomPlayer.GetComponent<Rigidbody2D>().velocity = TopPlayer.GetComponent<Rigidbody2D>().velocity;
            }
                
        }
    }

    /*void SwitchBackground()
    {
        float blackY = blackBackground.transform.position.y;
        float whiteY = WhiteBackground.transform.position.y;

        blackBackground.transform.position = new Vector3(blackBackground.transform.position.x,
            whiteY, blackBackground.transform.position.z);
        WhiteBackground.transform.position = new Vector3(WhiteBackground.transform.position.x,
            blackY, WhiteBackground.transform.position.z);
    }*/


    IEnumerator SwitchColor(float duraction, bool inverse)
    {
        SpriteRenderer TopRenderer = TopPlayer.GetComponent<SpriteRenderer>();
        SpriteRenderer BottomRenderer = BottomPlayer.GetComponent<SpriteRenderer>();
        float value;
        while (timeElapesd <= duraction)
        {
            if (inverse)
            {
                value = Mathf.Lerp(1f, 0f, timeElapesd / duraction);
                BottomRenderer.color = Color.Lerp(alittleGray, Color.black, timeElapesd / duraction);
                TopRenderer.color = Color.Lerp(Color.black, alittleGray, timeElapesd / duraction);
            }
            else
            {
                value = Mathf.Lerp(0f, 1f, timeElapesd / duraction);
                TopRenderer.color = Color.Lerp(alittleGray, Color.black, timeElapesd / duraction);
                BottomRenderer.color = Color.Lerp(Color.black, alittleGray, timeElapesd / duraction);
            }
                
            whiteMaterial.SetFloat("_Fade", value);
            blackMaterial.SetFloat("_Fade", value);
            timeElapesd += Time.deltaTime;
            yield return null;
        }

        timeElapesd = 0f;
    }

    public void Die()
    {
        if (CharacterInControl == 0)
        {
            TopPlayer.transform.position = checkPointPos;
        }
        else
        {
            BottomPlayer.transform.position = new Vector3(checkPointPos.x,
                checkPointPos.y - 5, checkPointPos.z);
        }
        currentState = GameState.Playing;
    }

    public void UpdateCheckPoint(Vector3 pos)
    {
        checkPointPos = pos;
    }

    private void OnApplicationQuit()
    {
        whiteMaterial.SetFloat("_Fade", 1);
        blackMaterial.SetFloat("_Fade", 1);
    }
}
