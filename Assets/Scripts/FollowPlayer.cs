using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform targetPlayer;
    public Vector3 offset;
    [Range(1, 10)] public float smoothness;

    // Update is called once per frame
    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 targetPosition = targetPlayer.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}