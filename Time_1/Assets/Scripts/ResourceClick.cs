using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceClick : MonoBehaviour, IPointerClickHandler
{
    private float n;
    public float MaxValue = 10.0f;

    private void Start()
    {
        n = MaxValue;
    }

    private void FixedUpdate()
    {
        if(n <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clickou");
        n--;
        BuildModeManager.Instance.increaseResourceX(1.0f);
    }
}
