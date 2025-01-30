using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMenuSound : MonoBehaviour
{
    AudioManager audioManager;
    public string hoverOverSound;
    public string clickSound;
    public string startGameSound;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in scene.");
        }
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }

    public void OnMouseClick()
    {
        audioManager.PlaySound(clickSound);
    }

    public void PlayStartGameSound()
    {
        audioManager.PlaySound(startGameSound);
    }
}
