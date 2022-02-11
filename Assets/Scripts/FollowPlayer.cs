using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform targetPlayer;
    public Vector3 offset;
    [Range(1, 10)] public float smoothness;
    public Vector3 lowerLevelBounds;
    public Vector3 upperLevelBounds;
    private float _halfViewport;
    private Vector3 _lowerCameraBound;
    private Vector3 _upperCameraBound;

    private void Start()
    {
        Camera cameraComponent = gameObject.GetComponent<Camera>();
        float orthographicSize = cameraComponent.orthographicSize;
        _halfViewport = orthographicSize * cameraComponent.aspect;
        
        _lowerCameraBound.x = lowerLevelBounds.x + _halfViewport;
        _lowerCameraBound.y = lowerLevelBounds.y + orthographicSize;
        _upperCameraBound.x = upperLevelBounds.x - _halfViewport;
        _upperCameraBound.y = upperLevelBounds.y - orthographicSize;
        _lowerCameraBound.z = lowerLevelBounds.z;
        _upperCameraBound.z = upperLevelBounds.z;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPosition = targetPlayer.position + offset;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, _lowerCameraBound.x, _upperCameraBound.x),
            Mathf.Clamp(targetPosition.y, _lowerCameraBound.y, _upperCameraBound.y),
            Mathf.Clamp(targetPosition.z, _lowerCameraBound.z, _upperCameraBound.z)
        );

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothness * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}