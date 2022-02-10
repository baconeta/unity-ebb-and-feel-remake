using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform targetPlayer;
    public Vector3 offset;
    [Range(1, 10)] public float smoothness;
    public Vector3 lowerBounds;
    public Vector3 upperBounds;

    // Update is called once per frame
    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPosition = targetPlayer.position + offset;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, lowerBounds.x, upperBounds.x),
            Mathf.Clamp(targetPosition.y, lowerBounds.y, upperBounds.y),
            Mathf.Clamp(targetPosition.z, lowerBounds.z, upperBounds.z)
        );

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothness * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}