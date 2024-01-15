using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    public enum PlayerState
    {
        IdleState,
        WalkingState,
        FallingState,
        JumpingState,
        DeathState
    }

    public PlayerState playerState;

    private void Start()
    {
        playerState = PlayerState.IdleState;
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        if (Input.GetKey(KeyCode.Space) && grounded)
            jump();
        anim.SetBool("run",horizontalInput != 0);
        anim.SetBool("grounded",grounded);
        
        FallCheck();
    }

    private void jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        playerState = PlayerState.JumpingState;
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void FallCheck()
    {
        if (body.velocity.y < 0 && playerState != PlayerState.DeathState)
        {
            playerState = PlayerState.FallingState;
        }

        if (body.velocity.y == 0)
        {
            playerState = PlayerState.IdleState;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FallCollider"))
        {
            playerState = PlayerState.DeathState;
            Debug.Log(message:"Game Over");
        }
    }
}
