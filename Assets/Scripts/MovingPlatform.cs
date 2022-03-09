using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float sleepTime;
    [Range(0f, 2.0f)] public float basePlatformSpeed;
    private Vector3 _origin;
    public Vector3 targetLocation;
    private bool _isSleeping;
    private float _timeSlept;
    private bool _isReturning;
    private bool _playerConnected;

    private SanityManager _gameSanityManager;

    public float lowSanitySpeedup = 2.0f;
    public float highSanitySpeedup = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        _origin = transform.position;
        _isReturning = false;
        _gameSanityManager = FindObjectOfType<SanityManager>();
        _playerConnected = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float platformSpeedThisFrame = basePlatformSpeed;
        SanityManager.SanityLevel sanity = _gameSanityManager.GetSanityLevel();

        switch (sanity)
        {
            case SanityManager.SanityLevel.Low:
                platformSpeedThisFrame *= lowSanitySpeedup;
                break;
            case SanityManager.SanityLevel.High:
                platformSpeedThisFrame *= highSanitySpeedup;
                if (_playerConnected)
                {
                    platformSpeedThisFrame = basePlatformSpeed;
                }
                break;
            case SanityManager.SanityLevel.Medium:
                break;
        }

        if (_isSleeping)
        {
            return;
        }

        if (!_isReturning)
        {
            //move towards target position
            Vector3 smoothPosition = Vector3.MoveTowards(transform.position, targetLocation,
                platformSpeedThisFrame * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
        else
        {
            Vector3 smoothPosition =
                Vector3.MoveTowards(transform.position, _origin, platformSpeedThisFrame * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }

        if ((transform.position == targetLocation && !_isReturning) ||
            (transform.position == _origin && _isReturning))
        {
            Invoke(nameof(ChangeDirection), sleepTime);
            _isSleeping = true;
        }
    }

    void ChangeDirection()
    {
        _isReturning = !_isReturning;
        _isSleeping = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(transform);
            _playerConnected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(null);
            _playerConnected = false;
        }
    }
}