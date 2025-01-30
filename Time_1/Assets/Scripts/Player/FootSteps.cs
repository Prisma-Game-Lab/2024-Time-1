using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    AudioManager audioManager;
    public string walkSound;
    public string runSound;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("W") || Input.GetKeyDown("A") || Input.GetKeyDown("S") || Input.GetKeyDown("D"))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                audioManager.PlaySound(runSound);
            }
            else
            {
                audioManager.PlaySound(walkSound);
            }
        }
        audioManager.StopSound(walkSound);
        audioManager.StopSound(runSound);
    }
}
