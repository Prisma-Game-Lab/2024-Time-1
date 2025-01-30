using UnityEngine;

public class TurretSelect : MonoBehaviour
{
    AudioManager audioManager;
    public string clickSound;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in scene.");
        }
    }

    public GameObject turret;
    public TurretData data;
    public void turretSelect()
    {
        if (BuildModeManager.Instance.canBuy(data))
        {
            BuildModeManager.Instance.setSelectedTurret(turret, data);
            audioManager.PlaySound(clickSound);
        }
    }
}
