using UnityEngine;

public class UpgradeSelect : MonoBehaviour
{
    AudioManager audioManager;
    public string clickSound;

    public TurretData data;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in scene.");
        }
    }

    public void upgradeSelectTurret()
    {
        if (BuildModeManager.Instance.canBuy(data))
        {
            BuildModeManager.Instance.enableUpgradeDefault(true);
            audioManager.PlaySound(clickSound);
        }
        else
        {
            BuildModeManager.Instance.enableUpgradeDefault(false);
        }
    }

    public void upgradeSelectLaser()
    {
        if (BuildModeManager.Instance.canBuy(data))
        {
            BuildModeManager.Instance.enableUpgradeLaser(true);
            audioManager.PlaySound(clickSound);
        }
        else
        {
            BuildModeManager.Instance.enableUpgradeLaser(false);
        }
    }

}
