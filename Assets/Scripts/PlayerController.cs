using PlayerNotifications;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private bool _isJumping;
    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _startLocation;
    public float fallingBoostPower;
    private Vector3 _respawnLocation;

    public bool isHoldingPotion;
    public int heldPotionSanityToAdd;
    public AudioClip[] landingSounds;
    [Range(0.0f, 1.0f)] public float landingVolume;

    private Rigidbody2D _rB2D;
    private SanityManager _gameSanityManager;
    private Animator _anim;
    private AudioSource _animationSoundPlayer;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private bool _isWaitingToDie;

    private NotificationController nc;

    private void Start()
    {
        nc = GetComponentInChildren<NotificationController>();
        
        _rB2D = gameObject.GetComponent<Rigidbody2D>();
        _gameSanityManager = FindObjectOfType<SanityManager>();
        _animationSoundPlayer = GetComponent<AudioSource>();
        _isJumping = false;
        _startLocation = transform.position;
        _respawnLocation = _startLocation;
        _anim = GetComponent<Animator>();
        _anim.SetBool(IsJumping, false);
        _anim.SetBool(IsRunning, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (isHoldingPotion)
            {
                UsePotion();
            }
        }

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
                nc.DisplayNotificationMessage("testing 123", 4.0f, true);
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

    private void UsePotion()
    {
        isHoldingPotion = false;
        _gameSanityManager.AddSanity(heldPotionSanityToAdd);
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
            PlayLandingSound();
            _isJumping = false;
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            ResetPlayer();
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

        if (col.gameObject.CompareTag("Respawn"))
        {
            //Change respawn location
            _respawnLocation = col.gameObject.transform.position;
            Destroy(col.gameObject);
        }
    }

    private void PickupPotion(PotionController potion)
    {
        isHoldingPotion = true;
        heldPotionSanityToAdd = potion.sanityEffectValue;

        if (!potion.isSanityBoost)
        {
            heldPotionSanityToAdd *= -1;
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
        _isWaitingToDie = true;
        Invoke(nameof(ResetPlayer), 1.5f);
    }

    private void ResetPlayer()
    {
        transform.position = _respawnLocation;
    }

    private void OnBecameVisible()
    {
        if (!_isWaitingToDie) return;

        _isWaitingToDie = false;
        CancelInvoke();
    }

    public void PlayFootstepSound()
    {
        _animationSoundPlayer.Play();
    }

    private void PlayLandingSound()
    {
        _animationSoundPlayer.PlayOneShot(landingSounds[Random.Range(0, landingSounds.Length)], 0.5f);
    }
}