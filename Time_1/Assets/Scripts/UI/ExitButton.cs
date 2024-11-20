using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void doExitGame()
    {
        Application.Quit();
    }

    public void RestartGame(string SceneToGo)
    {
        SceneManager.LoadScene(SceneToGo);
    }
}
