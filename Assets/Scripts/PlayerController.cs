using System;
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

    public bool isHoldingPotion;
    private int _heldPotionSanityToAdd;

    private Rigidbody2D _rB2D;
    private SanityManager _gameSanityManager;
    private Animator _anim;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");

    private void Start()
    {
        _rB2D = gameObject.GetComponent<Rigidbody2D>();
        _gameSanityManager = FindObjectOfType<SanityManager>();
        _isJumping = false;
        _startLocation = transform.position;
        _anim = GetComponent<Animator>();
        _anim.SetBool(IsJumping, false);
        _anim.SetBool(IsRunning, false);
    }

    private void Update()
    {
        // Already jumping
        if (_isJumping)
        {
            _moveVertical = 0f;
            _anim.SetBool(IsJumping, true);
        }

        else
        {
            // Begin jumping
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _moveVertical = Input.GetAxisRaw("Vertical");
            }

            _anim.SetBool(IsJumping, false);
        }

        _moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (_moveHorizontal == 0)
        {
            _anim.SetBool(IsRunning, false);
            return;
        }

        // X-axis animations and PlayerFacing direction
        Transform playerTransform = transform;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!_isJumping)
            {
                _anim.SetBool(IsRunning, true);
            }

            Vector3 transformLocalScale = playerTransform.localScale;
            transformLocalScale.x = 1;
            playerTransform.localScale = transformLocalScale;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!_isJumping)
            {
                _anim.SetBool(IsRunning, true);
            }

            Vector3 transformLocalScale = playerTransform.localScale;
            transformLocalScale.x = -1;
            playerTransform.localScale = transformLocalScale;
        }
        else
        {
            _anim.SetBool(IsRunning, false);
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
            _rB2D.velocity += Vector2.up * (Physics2D.gravity.y * (fallingBoostPower - 1) * Time.fixedDeltaTime);
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

        if (col.gameObject.CompareTag("Potion"))
        {
            if (!isHoldingPotion)
            {
                PotionController potionController = col.gameObject.GetComponent<PotionController>();
                PickupPotion(potionController);
                potionController.Pickup();
            }
        }
    }

    private void PickupPotion(PotionController potion)
    {
        isHoldingPotion = true;
        _heldPotionSanityToAdd = potion.sanityEffectValue;

        if (!potion.isSanityBoost)
        {
            _heldPotionSanityToAdd *= -1;
        }

        Debug.Log("Got a potion");
        Debug.Log("Will add " + _heldPotionSanityToAdd + " sanity when used.");
        // Make potion show in HUD?
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