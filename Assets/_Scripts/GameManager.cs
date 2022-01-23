using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject TopPlayer;
    [SerializeField] private GameObject BottomPlayer;
    [SerializeField] private UIController UICon;
    private CharacterController2d T_CharControl;
    private CharacterController2d B_CharControl;
    private PlayerMovement T_playerMove;
    private PlayerMovement B_playerMove;
    private CharacterFollow T_characterFollow;
    private CharacterFollow B_characterFollow;
    private int CharacterInControl;
    public static GameManager instance;
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
                UICon.Death();
                DisableCharacterController();
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
            T_CharControl.enabled = true;
            T_playerMove.enabled = true;
            T_characterFollow.enabled = false;
            B_CharControl.enabled = false;
            B_playerMove.enabled = false;
            B_characterFollow.enabled = true;
            TopPlayer.GetComponent<CapsuleCollider2D>().enabled = true;
            BottomPlayer.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else
        {
            T_CharControl.enabled = false;
            T_playerMove.enabled = false;
            T_characterFollow.enabled = true;
            B_CharControl.enabled = true;
            B_playerMove.enabled = true;
            B_characterFollow.enabled = false;
            TopPlayer.GetComponent<CapsuleCollider2D>().enabled = false;
            BottomPlayer.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

    void SwitchControl()
    {
        CharacterInControl = (CharacterInControl + 1) % 2;
        T_CharControl.enabled = !T_CharControl.isActiveAndEnabled;
        T_playerMove.enabled = !T_playerMove.isActiveAndEnabled;
        T_characterFollow.enabled = !T_characterFollow.isActiveAndEnabled;
        B_CharControl.enabled = !B_CharControl.isActiveAndEnabled; 
        B_playerMove.enabled = !B_playerMove.isActiveAndEnabled;
        B_characterFollow.enabled = !B_characterFollow.isActiveAndEnabled;
        TopPlayer.GetComponent<CapsuleCollider2D>().enabled = !TopPlayer.GetComponent<CapsuleCollider2D>().enabled;
        BottomPlayer.GetComponent<CapsuleCollider2D>().enabled = !BottomPlayer.GetComponent<CapsuleCollider2D>().enabled;
        if(CharacterInControl == 0)
            TopPlayer.GetComponent<Rigidbody2D>().velocity = BottomPlayer.GetComponent<Rigidbody2D>().velocity;
        else
            BottomPlayer.GetComponent<Rigidbody2D>().velocity = TopPlayer.GetComponent<Rigidbody2D>().velocity;

    }

    public void Die()
    {
        SwitchGameState(GameState.Dead);
    }
    
}
