using UnityEngine;

public class UpgradeSelect : MonoBehaviour
{
    public TurretData data;

    public void upgradeSelectTurret()
    {
        if (BuildModeManager.Instance.canBuy(data))
        {
            BuildModeManager.Instance.enableUpgradeDefault(true);
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
        }
        else
        {
            BuildModeManager.Instance.enableUpgradeLaser(false);
        }
    }

}
