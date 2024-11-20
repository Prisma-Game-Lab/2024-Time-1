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

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
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
        currentHealth -= n;
        healthBar.fillAmount = currentHealth/maxHealth;
    }
}
