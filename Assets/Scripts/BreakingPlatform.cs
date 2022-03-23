using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreakingPlatform : MonoBehaviour
{
    public float baseRespawnTime;
    private bool _isBreaking;
    public float baseTimeToBreak;
    private AudioSource _breakingSoundAudioSource;
    private CameraShake _cameraShakeClass;

    private Vector3 _originLocation;
    private Quaternion _originRotation;

    public float platformShakeStrength = .1f;
    public float platformShakeDecay = 0.005f;
    public float basePlatformFallSpeed;

    private float _shakeIntensity;
    private Transform _transform;
    private float _velocity;
    private bool _isFalling;

    // Start is called before the first frame update
    private void Start()
    {
        // Save the basic location (where it respawns to)
        _transform = transform;
        _originLocation = _transform.position;
        _originRotation = _transform.rotation;
        _breakingSoundAudioSource = GetComponent<AudioSource>();
        _cameraShakeClass = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        _isBreaking = false;
        _isFalling = false;
        _velocity = 0;
    }

    void FixedUpdate()
    {
        if (_isFalling)
        {
            _velocity += basePlatformFallSpeed;
            Vector3 transformPosition = transform.position;
            transformPosition.y -= _velocity * Time.fixedDeltaTime;
            _transform.position = transformPosition;
        }
    }

    private void StartBreaking()
    {
        _isBreaking = true;
        
        // shake
        _cameraShakeClass.MakeItShake();
        Shake();

        // play sound
        _breakingSoundAudioSource.PlayOneShot(_breakingSoundAudioSource.clip);

        // prepare platform to fall
        Invoke(nameof(PlatformFall), baseTimeToBreak);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isBreaking)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // start breaking platform

            StartBreaking();
        }
    }

    private IEnumerator ShakeIt()
    {
        while (_shakeIntensity > 0f)
        {
            _transform.localPosition = _originLocation + Random.insideUnitSphere * _shakeIntensity;
            _transform.localRotation = new Quaternion(
                _originRotation.x + Random.Range(-_shakeIntensity, _shakeIntensity) * .2f,
                _originRotation.y + Random.Range(-_shakeIntensity, _shakeIntensity) * .2f,
                _originRotation.z + Random.Range(-_shakeIntensity, _shakeIntensity) * .2f,
                _originRotation.w + Random.Range(-_shakeIntensity, _shakeIntensity) * .2f);
            _shakeIntensity -= platformShakeDecay;
            yield return null;
        }

        ShakingStopped();

        yield return null;
    }

    private void ShakingStopped()
    {
        _transform.localPosition = _originLocation;
        _transform.localRotation = _originRotation;
    }

    private void PlatformFall()
    {
        _isFalling = true;
    }

    private void RespawnPlatform()
    {
        // reset platform values and settings
        _isBreaking = false;
        _isFalling = false;
        _transform.position = _originLocation;
        _transform.rotation = _originRotation;
        _velocity = 0;
    }

    private void Shake()
    {
        _shakeIntensity = platformShakeStrength;
        StartCoroutine(nameof(ShakeIt));
    }

    private void OnBecameInvisible()
    {
        // start a timer here to make the platform respawn
        Invoke(nameof(RespawnPlatform), baseRespawnTime);
    }
}