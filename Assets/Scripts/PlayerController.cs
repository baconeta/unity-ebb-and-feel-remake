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

    // Start is called before the first frame update
    private void Start()
    {
        rB2D = gameObject.GetComponent<Rigidbody2D>();
        isJumping = false;
    }

    // Update is called once per frame
    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rB2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (!isJumping && (moveVertical > 0.1f || moveVertical < -0.1f))
        {
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
}