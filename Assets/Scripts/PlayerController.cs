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

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            CheckGround();
        }

        void Update()
        {
            if (Input.GetButton("Horizontal")) 
            {
                moveInput = Input.GetAxis("Horizontal");
                if (moveInput < 0)
                {
                    if (WallCheckL.IsTouching(Walls) == false)
                    {
                        Vector3 direction = transform.right * moveInput;
                        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, movingSpeed * Time.deltaTime);
                    }
                }
                else
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

            }
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded )
            {
                rigidbody.gravityScale = 1;
                rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
            if (Input.GetKey(KeyCode.Space) == false)
            {
                rigidbody.gravityScale = 3;
            }

            if (facingRight == false && moveInput > 0)
            {
                SpriteRend.flipX = false;
            }
            else if(facingRight == true && moveInput < 0)
            {
                SpriteRend.flipX = true;
            }
        }

        private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
            isGrounded = colliders.Length > 1;
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
