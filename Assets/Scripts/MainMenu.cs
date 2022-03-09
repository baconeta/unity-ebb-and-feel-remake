using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playGameScene;

    public void NewGame()
    {
        Debug.Log("Level one load?");
        SceneManager.LoadScene(playGameScene);
    }
}