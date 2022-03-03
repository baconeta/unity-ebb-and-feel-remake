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

    // Start is called before the first frame update
    void Start()
    {
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
    void Update()
    {

        if (_isSleeping)
        {
            return;
        }

        if (_isMovingRight)
        {
            //move towards target position
            Vector3 smoothPosition = Vector3.MoveTowards(transform.position, _rightBounds,
                baseMovementSpeed * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
        else
        {
            Vector3 smoothPosition =
                Vector3.MoveTowards(transform.position, _leftBounds, baseMovementSpeed * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }

        if ((transform.position.x <= _leftBounds.x && !_isMovingRight) ||
            (transform.position.x >= _rightBounds.x && _isMovingRight))
        {
            Invoke(nameof(ChangeDirection), sleepTime);
            _isSleeping = true;
        }
    }

    void ChangeDirection()
    {
        _isMovingRight = !_isMovingRight;
        _anim.SetBool(IsMovingRight, _isMovingRight);

        _isSleeping = false;
    }
}