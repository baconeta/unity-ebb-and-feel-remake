using UnityEngine;
using UnityEngine.UI;

namespace PlayerNotifications
{
    public class PlayerNotifier : MonoBehaviour
    {
        //Components and base setup
        private Text _notificationTextBase;
        private Canvas _displayCanvas;
        private RectTransform _rectTransform;

        private void Start()
        {
            ComponentSetup();
            SetupCanvas();
            SetTextDefaults();
        }

        private void ComponentSetup()
        {
            // Ensure component is set up to display text on the screen
            _notificationTextBase = gameObject.AddComponent<Text>();
            _displayCanvas = gameObject.AddComponent<Canvas>();
            _rectTransform = gameObject.GetComponent<RectTransform>();
        }

        private void SetupCanvas()
        {
            // Should this be enabled to be set manually as well? TODO
            _displayCanvas.worldCamera = FindObjectOfType<Camera>();

            _displayCanvas.renderMode = RenderMode.WorldSpace;
            _displayCanvas.sortingOrder = 5; // UI mode
        }

        private void SetTextDefaults()
        {
            _notificationTextBase.alignment = TextAnchor.MiddleCenter;
            _notificationTextBase.font = Font.CreateDynamicFontFromOSFont("LiberationSans", 14); // TODO generify
            _notificationTextBase.fontSize = 28;
        }

        public void DisplayNotificationMessage(string m)
        {
            _notificationTextBase.text = m;
            // _notificationTextBase.enabled = true;
        }

        public void SetNotifierLocation(float x, float y)
        {
            Vector3 transformPosition = _rectTransform.position;
            transformPosition.x = x;
            transformPosition.y = y;
            _rectTransform.position = transformPosition;
        }

        public void ClearMessage()
        {
            _notificationTextBase.text = ""; 
        }
    }
}