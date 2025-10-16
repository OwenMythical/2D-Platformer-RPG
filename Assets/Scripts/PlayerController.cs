using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public float movingSpeed;
        public float jumpForce;
        public bool Stunned;
        private float moveInput;
        private bool AttackCooldown = false;

        private bool facingRight = false;
        [HideInInspector]
        public bool deathState = false;

        private bool isGrounded;

        private new Rigidbody2D rigidbody;

        public SpriteRenderer SpriteRend;
        public BoxCollider2D WallCheckL;
        public BoxCollider2D WallCheckR;
        public Transform groundCheck;
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
                if (Stunned == false)
                {
                    if (moveInput < 0)
                    {
                        if (WallCheckL.IsTouching(Walls) == false)
                        {
                            Vector3 direction = transform.right * moveInput;
                            rigidbody.linearVelocity = new Vector2(0, rigidbody.linearVelocity.y);
                            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                        }
                    }
                    else if (moveInput > 0)
                    {
                        if (WallCheckR.IsTouching(Walls) == false)
                        {
                            Vector3 direction = transform.right * moveInput;
                            rigidbody.linearVelocity = new Vector2(0, rigidbody.linearVelocity.y);
                            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                        }
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
            if (Input.GetMouseButtonDown(0))
            {
                if (AttackCooldown == false)
                {
                    AttackCooldown = true;
                    StartCoroutine(AttackWait(0.5f));
                    Debug.Log("Attacking");
                    //////////////////////////////////////////////// finish attack functionality /////////////////////////////////////////////////
                }
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
            Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position,groundCheck.localScale,0f);
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

        IEnumerator AttackWait(float Time)
        {
            yield return new WaitForSeconds(Time);
            AttackCooldown = false;
        }
    }
}
