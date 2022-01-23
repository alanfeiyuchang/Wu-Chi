using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationControl : MonoBehaviour
{

    //Animation State
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jump";

    public bool copyAnimation;

    private string currentState;
    private Animator animator;
    private CharacterController2d characterController;
    private Rigidbody2D rb;
    private CharacterFollow characterfollow;

    private bool inAir;
    private Vector2 playerVelocity;
    private GameObject copiedTarget;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController2d>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        // play animation
        animator.Play(newState);
        currentState = newState;
    }


    private void AnimationControl()
    {
        if (inAir)
            ChangeAnimationState(PLAYER_JUMP);
        else if (playerVelocity.x != 0)
            ChangeAnimationState(PLAYER_RUN);
        else
            ChangeAnimationState(PLAYER_IDLE);
    }
}
