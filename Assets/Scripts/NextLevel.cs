using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string nextLevelSceneName;
    public float delayBeforeLoadSeconds;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(LoadNextLevel), delayBeforeLoadSeconds);
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelSceneName);
    }
}