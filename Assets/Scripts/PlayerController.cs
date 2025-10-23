using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public bool DashUnlocked;
        bool Dashing = false;
        bool DashCool = false;
        private float moveInput;
        private bool AttackCooldown = false;

        private bool facingRight = false;
        [HideInInspector]
        public bool deathState = false;

        private bool isGrounded;

        public Rigidbody2D RB;
        public SpriteRenderer SpriteRend;
        public BoxCollider2D WallCheckL;
        public BoxCollider2D WallCheckR;
        public Transform groundCheck;
        public TilemapCollider2D Walls;
        public Animator Anim;
        public BoxCollider2D AttackBox;
        public Animator AttackAnim;
        public Transform AttackForm;

        public static PlayerController instance = null;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            moveInput = Input.GetAxis("Horizontal");
            Vector3 direction = transform.right * moveInput;
            if (Input.GetButton("Horizontal")) 
            {
                Anim.SetBool("Running", true);
                if (Stunned == false)
                {
                    if (moveInput < 0)
                    {
                        if (WallCheckL.IsTouching(Walls) == false)
                        {
                            AttackForm.localPosition = new Vector3(-0.7f, 0, 0);
                            AttackForm.localScale = new Vector3(-1, 1, 1);
                            RB.linearVelocity = new Vector2(0, RB.linearVelocity.y);
                            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                        }
                    }
                    else if (moveInput > 0)
                    {
                        if (WallCheckR.IsTouching(Walls) == false)
                        {
                            AttackForm.localPosition = new Vector3(0.7f, 0, 0);
                            AttackForm.localScale = new Vector3(1, 1, 1);
                            RB.linearVelocity = new Vector2(0, RB.linearVelocity.y);
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
                RB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
                Anim.SetBool("Grounded", isGrounded);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && DashUnlocked == true && DashCool == false)
            {
                if (moveInput != 0)
                {
                    DashCool = true;
                    Dashing = true;
                    StartCoroutine(Dash());
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (AttackCooldown == false)
                {
                    AttackCooldown = true;
                    StartCoroutine(Attack());
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

            if (RB.linearVelocity.y < 0)
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

        IEnumerator Attack()
        {
            var Hit = new ArrayList();
            AttackAnim.SetBool("Attacking", true);
            for (int i = 1; i <= 3; i++)
            {
                yield return new WaitForSeconds(0.05f);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(AttackForm.position, AttackForm.localScale, 0f);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Enemy") && !(Hit.Contains(collider)))
                    {
                        Hit.Add(collider);
                        EnemyHealthScript HPScript = (EnemyHealthScript)collider.gameObject.GetComponent("EnemyHealthScript");
                        StartCoroutine(HPScript.TakeDamage(1));
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.2f);
            AttackAnim.SetBool("Attacking", false);
            AttackCooldown = false;
        }

        IEnumerator Dash()
        {
            SpriteRend.color = new Color(0.9f, 0.8f, 0.8f);
            movingSpeed += 5;
            yield return new WaitForSeconds(0.5f);
            movingSpeed -= 7;
            Dashing = false;
            yield return new WaitForSeconds(3f);
            movingSpeed += 2;
            SpriteRend.color = new Color(1f, 1f, 1f);
            DashCool = false;
        }
    }
}
