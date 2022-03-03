using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Camera Information
    public Transform cameraTransform;
    private Vector3 _originalCameraPos;

    // Shake Parameters
    public float shakeDuration = 2f;
    public float shakeAmount = 0.7f;

    private bool _isShaking = false;
    private float _shakeTimer;

 

    // Start is called before the first frame update
    void Start()
    {
        _originalCameraPos = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isShaking)
        {
            StartCameraShakeEffect();
        }
    }

    public void MakeItShake()
    {
        if (!_isShaking)
        {
            _originalCameraPos = cameraTransform.localPosition;
            ShakeCamera();
        }
    }

    public void ShakeCamera()
    {
        _isShaking = true;
        _shakeTimer = shakeDuration;
    }

    public void StartCameraShakeEffect()
    {
        if (_shakeTimer > 0)
        {
            cameraTransform.localPosition = _originalCameraPos + Random.insideUnitSphere * shakeAmount;
            _shakeTimer -= Time.deltaTime;
        }
        else
        {
            _shakeTimer = 0f;
            cameraTransform.position = _originalCameraPos;
            _isShaking = false;
        }
    }

}
