using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        private float moveInput;

        private bool facingRight = false;
        [HideInInspector]
        public bool deathState = false;

        private bool isGrounded;
        public Transform groundCheck;

        private new Rigidbody2D rigidbody;

        public SpriteRenderer SpriteRend;
        public BoxCollider2D WallCheckL;
        public BoxCollider2D WallCheckR;
        public TilemapCollider2D Walls;
        public Animator Anim;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            moveInput = Input.GetAxis("Horizontal");
            if (Input.GetButton("Horizontal")) 
            {
                Anim.SetBool("Running", true);
                if (moveInput < 0)
                {
                    if (WallCheckL.IsTouching(Walls) == false)
                    {
                        Vector3 direction = transform.right * moveInput;
                        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                    }
                }
                else if (moveInput > 0)
                {
                    if (WallCheckR.IsTouching(Walls) == false)
                    {
                        Vector3 direction = transform.right * moveInput;
                        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                    }
                }
            }
            else
            {
                Anim.SetBool("Running", false);
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded )
            {
                rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
                Anim.SetBool("Grounded", isGrounded);
            }

            if (facingRight == false && moveInput > 0)
            {
                SpriteRend.flipX = false;
                facingRight = true;
            }
            else if(facingRight == true && moveInput < 0)
            {
                SpriteRend.flipX = true;
                facingRight = false;
            }

            if (rigidbody.linearVelocity.y < 0)
            {
                Anim.SetBool("Falling", true);
            }
            else
            {
                Anim.SetBool("Falling", false);
            }

            CheckGround();
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.1f);
            bool Found = false;
            foreach (Collider2D collider in colliders)
            {
                if (collider.name == "Collide")
                {
                    Found = true;
                    break;
                }
            }
            if (Found == true)
            {
                isGrounded = true;
                Anim.SetBool("Grounded", isGrounded);
            }
            else
            {
                isGrounded = false;
                Anim.SetBool("Grounded", isGrounded);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Coin")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
