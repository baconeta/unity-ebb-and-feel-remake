using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerNotifications
{
    /// <summary>
    /// This object will handle all functionality involving controlling the text on the screen.
    /// It managers timers related to priority and fade outs on a message.
    ///   It should be attached to objects where you want the message to follow a player, and you must also
    /// enable doesMessageFollowController from the editor.
    /// Remember to place a Notification controller in the world where this object is first going to appear if you have
    /// persistBetweenScenes enabled, otherwise place it in every scene where you will use it.
    /// </summary>
    public class NotificationController : MonoBehaviour
    {
        [Tooltip("Put the PlayerNotifier object into the world and reference it here.")]
        public PlayerNotifier playerNotificationObject;

        [Tooltip(
            "Set whether the message will follow the controller on message shown or controller object moved." +
            "Otherwise, the message will be displayed at the PlayerNotifier world location.")]
        public bool doesMessageFollowController;

        [Tooltip("Enable to save non-repeating messages between levels.")]
        public bool persistBetweenScenes;

        [Tooltip("Enable to have all future messages fade out. Callable from code if you want to change on the fly.")]
        public bool doMessagesFadeOut;

        [Tooltip("Used in conjunction with doMessagesFadeOut, sets the time a message takes to fade to nothing.")]
        public float timeToFadeOutInSeconds;

        private bool _isMessageOnScreen;

        private readonly List<string> _alreadyPlayedMessages = new List<string>();

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// Call this function (Broadcast or otherwise) to display a message in the world.
        /// </summary>
        /// <param name="m">Message to display</param>
        /// <param name="timeToDisplay">How long should the message display for (seconds).</param>
        /// <param name="canMessageBeReplayed">True if this message should only play once ever</param>
        public void DisplayNotificationMessage(string m, float timeToDisplay = 0.0f, bool canMessageBeReplayed = false)
        {
            if (!playerNotificationObject)
            {
                Debug.Log("No PlayerNotification Object was attached to the controller to handle messages.");
                return;
            }

            if (_isMessageOnScreen)
            {
                return;
            }

            if (!canMessageBeReplayed)
            {
                if (_alreadyPlayedMessages.Contains(m))
                {
                    return;
                }
            }

            if (timeToDisplay == 0.0f)
            {
                timeToDisplay = CalculateTimeToDisplay(m);
            }

            _isMessageOnScreen = true;
            Invoke(nameof(ClearMessageFromScreen), timeToDisplay);
            playerNotificationObject.DisplayNotificationMessage(m);
            _alreadyPlayedMessages.Add(m);
        }

        private float CalculateTimeToDisplay(string s)
        {
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
            playerNotificationObject.ClearMessage(doMessagesFadeOut, timeToFadeOutInSeconds);
            if (doMessagesFadeOut)
            {
                StartCoroutine(DelayFade(timeToFadeOutInSeconds));
                return;
            }

            _isMessageOnScreen = false;
        }

        private IEnumerator DelayFade(float delayTime)
        {
            //Wait for the specified delay time before continuing.
            yield return new WaitForSeconds(delayTime);

            _isMessageOnScreen = false;
        }
    }
}