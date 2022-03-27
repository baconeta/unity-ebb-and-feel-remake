using System;
using UnityEngine;

namespace PlayerNotifications
{
    public class NotificationController : MonoBehaviour
    {
        public PlayerNotifier playerNotificationObject;
        private bool _isMessageOnScreen;
        public bool doesMessageFollowController;

        public void DisplayNotificationMessage(string m)
        {
            if (!playerNotificationObject)
            {
                Debug.Log("No PlayerNotification Object was attached to the controller to handle messages.");
                return;
            }

            _isMessageOnScreen = true;
            playerNotificationObject.DisplayNotificationMessage(m);
        }

        private void Update()
        {
            if (doesMessageFollowController && _isMessageOnScreen)
            {
                playerNotificationObject.SetNotifierLocation(transform.position.x, transform.position.y);
            }
        }
    }
}