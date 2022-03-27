using System.Collections;
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

        /// <summary>
        /// Set and display a message on the Text object of this object.
        /// Call SetNotifierLocation() to set its world location.
        /// </summary>
        /// <param name="m">Message to display on-screen</param>
        public void DisplayNotificationMessage(string m)
        {
            SetMessageAlpha(1f);
            _notificationTextBase.text = m;
            Debug.Log("new message.");
        }

        // This function is used to set the initial location of the notification message and
        // can be called on update functions to pin it to a target.
        public void SetNotifierLocation(float x, float y)
        {
            Vector3 transformPosition = _rectTransform.position;
            transformPosition.x = x;
            transformPosition.y = y;
            _rectTransform.position = transformPosition;
        }

        public void ClearMessage(bool fadeMessageOut, float fadeMessageTime = 0.0f)
        {
            if (fadeMessageOut)
            {
                StartCoroutine(FadeOutMessage(fadeMessageTime));
            }
            else
            {
                Debug.Log("clear message");
                _notificationTextBase.text = "";
            }
        }

        private void SetMessageAlpha(float alpha)
        {
            Color color = _notificationTextBase.color;
            color.a = alpha;
            _notificationTextBase.color = color;
        }

        IEnumerator FadeOutMessage(float duration)
        {
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                SetMessageAlpha(Mathf.Lerp(1, 0, normalizedTime));

                yield return null;
            }

            Debug.Log("clear message on fade call");
            _notificationTextBase.text = "";
        }

        public void CancelFade()
        {
            StopAllCoroutines();
        }
    }
}