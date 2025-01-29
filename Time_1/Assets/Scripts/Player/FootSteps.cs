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
        // Verifica se o jogador está se movendo (W, A, S, D pressionados)
        bool moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        // Verifica se o jogador está correndo (Shift pressionado)
        bool running = Input.GetKey(KeyCode.LeftShift);

        // Se o jogador está se movendo
        if (moving)
        {
            // Se estiver correndo, toca o som de corrida e para o som de caminhada
            if (running)
            {
                audioManager.PlaySound(runSound);
                audioManager.StopSound(walkSound);
            }
            // Se não estiver correndo, toca o som de caminhada e para o som de corrida
            else
            {
                audioManager.StopSound(runSound);
                audioManager.PlaySound(walkSound);
            }
        }
        // Se o jogador não está se movendo
        else
        {
            audioManager.StopSound(walkSound);
            audioManager.StopSound(runSound);
        }
    }
}
