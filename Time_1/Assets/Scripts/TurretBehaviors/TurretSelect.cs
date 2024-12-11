using UnityEngine;

public class TurretSelect : MonoBehaviour
{
    public GameObject turret;
    public TurretData data;
    public void turretSelect()
    {
        if (BuildModeManager.Instance.canBuy(data))
        {
            Debug.Log("CanBuy");
            BuildModeManager.Instance.setSelectedTurret(turret, data);
        }
    }
}
