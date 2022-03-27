using UnityEngine;

namespace PlayerNotifications
{
    public class NotificationController : MonoBehaviour
    {
        public PlayerNotifier playerNotificationObject;

        // Start is called before the first frame update
        public void DisplayNotificationMessage(string m)
        {
            playerNotificationObject.DisplayNotificationMessage(m);
        }
    }
}