using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    AudioManager audioManager;

    public void start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in scene.");
        }
        audioManager.StopSound("Theme");
    }

    public void doExitGame()
    {
        Application.Quit();
    }

    public void RestartGame(string SceneToGo)
    {
        //delay de 2 segundos para iniciar o jogo
        Invoke("PlayStartGameSound", 3f);
        SceneManager.LoadScene(SceneToGo);
    }
}
