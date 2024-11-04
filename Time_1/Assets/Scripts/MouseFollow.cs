using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private Vector3 mousepos;

    private void Update()
    {
        mousepos = Input.mousePosition;
        mousepos.z = 2.0f;
        transform.position = Camera.main.ScreenToWorldPoint(mousepos);
    }

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Turret")
        {
            BuildModeManager.Instance.CanPlaceTurret(false);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Turret")
        {
            BuildModeManager.Instance.CanPlaceTurret(true);
        }
    }

}
