using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject TopPlayer;
    [SerializeField] private GameObject BottomPlayer;
    private CharacterController2d T_CharControl;
    private CharacterController2d B_CharControl;
    private PlayerMovement T_playerMove;
    private PlayerMovement B_playerMove;
    private CharacterFollow T_characterFollow;
    private CharacterFollow B_characterFollow;
    private int CharacterInControl;
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
    }

    void SwitchControl()
    {
        T_CharControl.enabled = !T_CharControl.isActiveAndEnabled;
        T_playerMove.enabled = !T_playerMove.isActiveAndEnabled;
        T_characterFollow.enabled = !T_characterFollow.isActiveAndEnabled;
        B_CharControl.enabled = !B_CharControl.isActiveAndEnabled; 
        B_playerMove.enabled = !B_playerMove.isActiveAndEnabled;
        B_characterFollow.enabled = !B_characterFollow.isActiveAndEnabled;
        /*if (CharacterInControl == 0)
        {
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
        }*/
    }
}
