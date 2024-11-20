using UnityEngine;

public class TurretSelect : MonoBehaviour
{
    public GameObject turret;
    public float cost;
    public void turretSelect()
    {
        if (BuildModeManager.Instance.getResourceX() >= cost)
        {
            BuildModeManager.Instance.setSelectedTurret(turret, cost);
        }
    }
}
