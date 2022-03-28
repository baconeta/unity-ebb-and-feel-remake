using PlayerNotifications;
using UnityEngine;

/// Attach this script to ny object to call a player notification to the attached controller on wake.
public class MessageOnWake : MonoBehaviour
{
    public NotificationController notificationController;

    public string messageToPlay = "default";

    [Tooltip("The amount of time to wait after start before this message plays")]
    public float delayTimeToPlay;

    [Tooltip("Priority level of the message - 1 is highest, increasing is lower priority.")]
    public int priority;

    public float timeToDisplayMessage;
    public bool canMessageBeReplayed;

    // Start is called before the first frame update
    void Start()
    {
        if (notificationController != default)
        {
            Invoke(nameof(PlayMessage), delayTimeToPlay);
        }
        else
        {
            Debug.Log("No notification controller set on " + name);
        }
    }

    private void PlayMessage()
    {
        notificationController.DisplayNotificationMessage(messageToPlay, priority, canMessageBeReplayed,
            timeToDisplayMessage);
    }
}