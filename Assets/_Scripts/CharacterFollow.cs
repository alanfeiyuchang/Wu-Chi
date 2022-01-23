using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollow : MonoBehaviour
{
    public GameObject followTarget;
    public int followTargetInt;
    private Animator animator;

    private CharacterController2d copiedCharacterControl;
    private CharacterController2d m_CharacterControl;

    private string currentState;

    //Animation State
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jump";
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        copiedCharacterControl = followTarget.GetComponent<CharacterController2d>();
        m_CharacterControl = GetComponent<CharacterController2d>();
    }

    // Update is called once per frame
    void Update()
    {
        float yValue;
        if(followTargetInt == 0)
        {
            yValue = followTarget.transform.position.y - 5;
            if (yValue < -4.326995f) { yValue = -4.326995f; }
        }
        else
        {
            yValue = followTarget.transform.position.y + 5;
            if (yValue < 0.6730059f) { yValue = 0.6730059f; }
        }
        
        transform.position = new Vector3(followTarget.transform.position.x, yValue,transform.position.z);
        transform.localScale = followTarget.transform.localScale;
        m_CharacterControl.m_FacingRight = copiedCharacterControl.m_FacingRight;

        string copiedAnimation = copiedCharacterControl.currentState;
        ChangeAnimationState(copiedAnimation);
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        // play animation
        animator.Play(newState);
        currentState = newState;

    }
}
