using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float sleepTime;
    [Range(0f, 0.25f)] public float basePlatformSpeed;
    private Vector3 _origin;
    public Vector3 targetLocation;
    private bool _isSleeping;
    private float _timeSlept;
    private bool _isReturning;

    // Start is called before the first frame update
    void Start()
    {
        _origin = transform.position;
        _isReturning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSleeping)
        {
            return;
        }

        if (!_isReturning)
        {
            //move towards target position
            Vector3 smoothPosition = Vector3.MoveTowards(transform.position, targetLocation,
                basePlatformSpeed * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
        else
        {
            Vector3 smoothPosition =
                Vector3.MoveTowards(transform.position, _origin, basePlatformSpeed * Time.fixedDeltaTime);
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
}