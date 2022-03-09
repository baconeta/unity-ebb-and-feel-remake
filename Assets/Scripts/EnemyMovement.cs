using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject platformForBounding;
    private Vector3 _leftBounds;
    private Vector3 _rightBounds;
    private bool _isMovingRight;
    private bool _isSleeping;
    [Range(0f, 0.25f)] public float baseMovementSpeed;
    public float sleepTime;
    private static readonly int IsMovingRight = Animator.StringToHash("IsMovingRight");
    private Animator _anim;
    private bool _isDisabled = true;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    private SanityManager _gameSanityManager;
    public float lowSanitySpeedupValue = 2.0f;

    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameSanityManager = FindObjectOfType<SanityManager>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _isMovingRight = true;
        _anim = GetComponent<Animator>();

        if (_anim != null)
        {
            _anim.SetBool(IsMovingRight, true);
        }

        if (platformForBounding != null)
        {
            // This is very ugly. To tidy later
            float halfPlatformWidth = platformForBounding.GetComponent<BoxCollider2D>().size.x / 2 * 0.8f;
            _leftBounds = platformForBounding.transform.position;
            _rightBounds = _leftBounds;
            _rightBounds.x += halfPlatformWidth;
            _leftBounds.x -= halfPlatformWidth;
            Vector3 position = transform.position;
            _leftBounds.z = position.z;
            _rightBounds.z = position.z;
            _leftBounds.y = position.y;
            _rightBounds.y = position.y;
        }
        else
        {
            Debug.Log("Enemy: " + name + ", has no platform bounding set");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        float movementSpeedThisFrame = baseMovementSpeed;
        SanityManager.SanityLevel sanity = _gameSanityManager.GetSanityLevel();

        switch (sanity)
        {
            case SanityManager.SanityLevel.Low:
                movementSpeedThisFrame *= lowSanitySpeedupValue;
                break;
            case SanityManager.SanityLevel.High:
                MakeInvisible();
                return;
            case SanityManager.SanityLevel.Medium:
                if (_isDisabled) MakeVisible();
                break;
        }


        if (_isSleeping)
        {
            return;
        }

        if (_isMovingRight)
        {
            //move towards target position
            Vector3 smoothPosition = Vector3.MoveTowards(transform.position, _rightBounds,
                movementSpeedThisFrame * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
        else
        {
            Vector3 smoothPosition =
                Vector3.MoveTowards(transform.position, _leftBounds, movementSpeedThisFrame * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }

        if ((transform.position.x <= _leftBounds.x && !_isMovingRight) ||
            (transform.position.x >= _rightBounds.x && _isMovingRight))
        {
            Invoke(nameof(ChangeDirection), sleepTime);
            _isSleeping = true;
        }
    }

    private void ChangeDirection()
    {
        _isMovingRight = !_isMovingRight;
        _anim.SetBool(IsMovingRight, _isMovingRight);

        _isSleeping = false;
    }

    private void MakeInvisible()
    {
        _isDisabled = true;
        _spriteRenderer.enabled = false;
        _boxCollider2D.enabled = false;
    }

    private void MakeVisible()
    {
        _isDisabled = false;
        _spriteRenderer.enabled = true;
        _boxCollider2D.enabled = true;
    }
}