using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private bool _isJumping;
    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _startLocation;
    public float fallingBoostPower;

    private Rigidbody2D _rB2D;

    private void Start()
    {
        _rB2D = gameObject.GetComponent<Rigidbody2D>();
        _isJumping = false;
        _startLocation = transform.position;
    }

    private void Update()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (!_isJumping && Input.GetKeyDown(KeyCode.UpArrow))
        {
            _moveVertical = Input.GetAxisRaw("Vertical");
        }

        if (_isJumping)
        {
            _moveVertical = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (_moveHorizontal > 0.01f || _moveHorizontal < -0.01f)
        {
            _rB2D.AddForce(new Vector2(_moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (!_isJumping && (_moveVertical > 0.01f || _moveVertical < -0.01f))
        {
            // need to zero any lingering y velocity for jump height to be consistent
            _rB2D.velocity = new Vector2(_rB2D.velocity.x, 0f);

            _rB2D.AddForce(new Vector2(0f, _moveVertical * jumpForce), ForceMode2D.Impulse);
        }

        if (_isJumping && _rB2D.velocity.y < 0)
        {
            _rB2D.velocity += Vector2.up * (Physics2D.gravity.y * (fallingBoostPower-1) * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            _isJumping = false;
        }
        if (col.gameObject.CompareTag("Enemy"))
        {
            //RESET FUNCTION TBD
            transform.position = _startLocation;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _isJumping = true;
        }
    }

    private void OnBecameInvisible()
    {
        // reset player position
        transform.position = _startLocation;

        // todo add sanity reset too once implemented
    }
}