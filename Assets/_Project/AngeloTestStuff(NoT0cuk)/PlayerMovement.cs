using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    private Rigidbody2D rb2d;
    private Animator animator;
    private float xMovement, yMovement;
    private Vector2 movementVel;

    private void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
      TakePlayerInput();
      MovePlayer();
      AnimatePlayer();
    }

    private void TakePlayerInput()
    {
      xMovement = Input.GetAxis("Horizontal");
      yMovement = Input.GetAxis("Vertical");
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
        animator.SetInteger("xMovement", 1);
      } else if(xMovement < 0)
      {
        transform.localScale = new Vector3( -1f, 1f, 1f);
        animator.SetInteger("xMovement", -1);
      } else {
        animator.SetInteger("xMovement", 0);
      }

      if(yMovement > 0)
      {
        animator.SetInteger("yMovement", 1);
      } else if(yMovement < 0)
      {
        animator.SetInteger("yMovement", -1);
      } else {
        animator.SetInteger("yMovement", 0);
      }
    }


}
