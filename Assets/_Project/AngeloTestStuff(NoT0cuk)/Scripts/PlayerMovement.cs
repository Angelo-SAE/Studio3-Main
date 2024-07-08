using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private BoolObject paused;
    private Rigidbody2D rb2d;
    private Animator animator;
    private int xMovement, yMovement;
    private Vector2 movementVel;

    private void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
      if(!paused.value)
      {
        TakePlayerInput();
        MovePlayer();
        AnimatePlayer();
      }
    }

    private void TakePlayerInput()
    {
      xMovement = (int)Input.GetAxisRaw("Horizontal");
      yMovement = (int)Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
      movementVel = (new Vector2(xMovement, yMovement).normalized) * movementSpeed;
      rb2d.velocity = movementVel;
    }

    private void AnimatePlayer()
    {
      if(xMovement > 0)
      {
        transform.localScale = new Vector3( 1f, 1f, 1f);
      } else if(xMovement < 0)
      {
        transform.localScale = new Vector3( -1f, 1f, 1f);
      }
      animator.SetInteger("xMovement", xMovement);
      animator.SetInteger("yMovement", yMovement);
    }


}
