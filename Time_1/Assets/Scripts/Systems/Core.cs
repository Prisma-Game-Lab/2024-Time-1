using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    [Header("References")]
    public Image healthBar;
    [Header("Atributos")]
    public float maxHealth = 10.0f;
    public string sceneToGo;

    [Header("Audio")]
    AudioManager audioManager;
    public string damageSound;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in scene.");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(sceneToGo);
        }
    }

    public void damageCore(float n)
    {
        audioManager.PlaySound(damageSound);
        currentHealth -= n;
        healthBar.fillAmount = currentHealth/maxHealth;
    }
}
