// This object will handle all functionality involving controlling the text on the screen.
// It managers timers related to priority and fade outs on a message.
// It should be attached to objects where you want the message to follow a player, and you must also
// enable doesMessageFollowController from the editor.
// Remember to place a Notification controller in the world where this object is first going to appear if you have
// persistBetweenScenes enabled, otherwise place it in every scene where you will use it.

using UnityEngine;

namespace PlayerNotifications
{
    public class NotificationController : MonoBehaviour
    {
        public PlayerNotifier playerNotificationObject;
        private bool _isMessageOnScreen;
        public bool doesMessageFollowController;
        public bool persistBetweenScenes;

        public void DisplayNotificationMessage(string m, float timeToDisplay = 0.0f)
        {
            if (!playerNotificationObject)
            {
                Debug.Log("No PlayerNotification Object was attached to the controller to handle messages.");
                return;
            }

            if (timeToDisplay == 0.0f)
            {
                timeToDisplay = CalculateTimeToDisplay(m);
            }

            _isMessageOnScreen = true;
            Invoke(nameof(ClearMessageFromScreen), timeToDisplay);
            playerNotificationObject.DisplayNotificationMessage(m);
        }

        private float CalculateTimeToDisplay(string s)
        {
            // Used for default timing
            return s.Length / 10.0f + 2.0f;
        }

        private void Update()
        {
            if (doesMessageFollowController && _isMessageOnScreen)
            {
                playerNotificationObject.SetNotifierLocation(transform.position.x, transform.position.y);
            }
        }

        private void Awake()
        {
            if (persistBetweenScenes)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        private void ClearMessageFromScreen()
        {
            playerNotificationObject.ClearMessage();
        }
    }
}