using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private FloatObject playerSpeed;
    [SerializeField] private PlayerInteract pInteract;
    [SerializeField] private BoolObject paused;
    [SerializeField] private GameObjectObject player;
    [SerializeField] private FloatObject stress;
    private Rigidbody2D rb2d;
    private Animator animator;
    private int xMovement, yMovement;
    private Vector2 movementVel;
    private bool up, down, left, right;

    public bool carrying;

    private void Awake()
    {
      player.value = gameObject;
      paused.value = false;
    }

    private void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
    }

    private void Update()
    {
      if(Input.GetKeyDown(KeyCode.P))
      {
        Time.timeScale = 3;
      }
      if(Input.GetKeyDown(KeyCode.L))
      {
        Time.timeScale = 1;
      }
    }

    private void FixedUpdate()
    {
      if(!paused.value)
      {
        TakePlayerInput();
        MovePlayer();
        AnimatePlayer();
      } else {
        rb2d.velocity = Vector2.zero;
        animator.SetBool("Idle", true);
      }
    }

    private void TakePlayerInput()
    {
      xMovement = (int)Input.GetAxisRaw("Horizontal");
      yMovement = (int)Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
      movementVel = (new Vector2(xMovement, yMovement).normalized) * (playerSpeed.value - (stress.value/40));
      rb2d.velocity = movementVel;
    }

    private void AnimatePlayer()
    {
      if(yMovement == 0)
      {
        up = false;
        down = false;
        AltAnimate();
      } else if(yMovement == 1 && !up)
      {
        up = true;
        AnimatePlayer(0);
        pInteract.RotateDetection(0);
      } else if(yMovement == -1 && !down)
      {
        down = true;
        AnimatePlayer(2);
        pInteract.RotateDetection(2);
      }

      if(xMovement == 0)
      {
        right = false;
        left = false;
        AltAnimate();
      } else if(xMovement == 1 && !right)
      {
        right = true;
        transform.localScale = new Vector3( 1f, 1f, 1f);
        AnimatePlayer(1);
        pInteract.RotateDetection(1);
      } else if(xMovement == -1 && !left)
      {
        left = true;
        transform.localScale = new Vector3( -1f, 1f, 1f);
        AnimatePlayer(1);
        pInteract.RotateDetection(1);
      }
      if(!up && !down && !left && !right)
      {
        animator.SetBool("Idle", true);
      } else {
        animator.SetBool("Idle", false);
      }
    }

    private void AltAnimate()
    {
      if(up)
      {
        AnimatePlayer(0);
        pInteract.RotateDetection(0);
      } else if(down)
      {
        AnimatePlayer(2);
        pInteract.RotateDetection(2);
      }
      if(right)
      {
        transform.localScale = new Vector3( 1f, 1f, 1f);
        AnimatePlayer(1);
        pInteract.RotateDetection(1);
      } else if(left)
      {
        transform.localScale = new Vector3( -1f, 1f, 1f);
        AnimatePlayer(1);
        pInteract.RotateDetection(1);
      }
    }

    public void AnimatePlayer(int animationNumber)
    {
      if(carrying)
      {
        animationNumber += 6;
      }
      switch(animationNumber)
      {
        case(0):
        animator.Play("WalkUp");
        break;
        case(1):
        animator.Play("WalkSides");
        break;
        case(2):
        animator.Play("WalkDown");
        break;
        case(3):
        animator.Play("IdleUp");
        break;
        case(4):
        animator.Play("IdleSides");
        break;
        case(5):
        animator.Play("IdleDown");
        break;
        case(6):
        animator.Play("CWalkUp");
        break;
        case(7):
        animator.Play("CWalkSides");
        break;
        case(8):
        animator.Play("CWalkDown");
        break;
        case(9):
        animator.Play("CIdleUp");
        break;
        case(10):
        animator.Play("CIdleSides");
        break;
        case(11):
        animator.Play("CIdleDown");
        break;
      }
    }


}
