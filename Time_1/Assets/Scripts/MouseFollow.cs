using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private Vector3 mousepos;
    private int colisionCounter = 0;

    private void Update()
    {
        mousepos = Input.mousePosition;
        mousepos.z = 2.0f;
        transform.position = Camera.main.ScreenToWorldPoint(mousepos);
    }

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        colisionCounter++;
        if (collision.gameObject.tag == "Turret")
        {
            BuildModeManager.Instance.CanPlaceTurret(false);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        colisionCounter--;
        if (collision.gameObject.tag == "Turret" && colisionCounter == 0)
        {
            BuildModeManager.Instance.CanPlaceTurret(true);
        }
    }

}
