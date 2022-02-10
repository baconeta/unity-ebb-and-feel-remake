using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;

    private Rigidbody2D rB2D;
    
    private void Start()
    {
        rB2D = gameObject.GetComponent<Rigidbody2D>();
        isJumping = false;
    }
    
    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (!isJumping && Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveVertical = Input.GetAxisRaw("Vertical");
        }
        
        if (isJumping)
        {
            moveVertical = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (moveHorizontal > 0.01f || moveHorizontal < -0.01f)
        {
            rB2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (!isJumping && (moveVertical > 0.01f || moveVertical < -0.01f))
        {
            // need to zero any lingering y velocity for jump height to be consistent
            rB2D.velocity = new Vector2(rB2D.velocity.x, 0f);

            rB2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            isJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            isJumping = true;
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log("player left screen");
    }
}