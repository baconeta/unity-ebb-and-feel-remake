using UnityEngine;
using UnityEngine.UI;

namespace PlayerNotifications
{
    public class PlayerNotifier : MonoBehaviour
    {
        //Components and base setup
        private Text _notificationTextBase;
        private Canvas _displayCanvas;

        private void Start()
        {
            // Ensure component is set up to display text on the screen
            _notificationTextBase = gameObject.AddComponent<Text>();
            _displayCanvas = gameObject.AddComponent<Canvas>();

            // Should this be enabled to be set manually as well? TODO
            _displayCanvas.worldCamera = FindObjectOfType<Camera>();
            _displayCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            _notificationTextBase.font = Font.CreateDynamicFontFromOSFont("LiberationSans", 20);
            _displayCanvas.sortingOrder = 5;
            // DisplayNotificationMessage();
        }

        public void DisplayNotificationMessage(string m)
        {
            _notificationTextBase.text = m;
            // _notificationTextBase.enabled = true;
        }
    }
}