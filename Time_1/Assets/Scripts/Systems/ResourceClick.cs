using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceClick : MonoBehaviour, IPointerClickHandler
{
    private float maxClicks;
    public ResourceData data;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if(data.metalAmount > 0)
            maxClicks = data.metalAmount;
        if (data.eletronicAmount > 0)
            maxClicks = data.eletronicAmount;
        if(data.prismAmount > 0)
            maxClicks = data.prismAmount;
        if (data.uraniumAmount > 0)
            maxClicks = data.uraniumAmount;
    }

    private void FixedUpdate()
    {
        if(maxClicks <= 0)
        {
            TooltipSystem.Hide();
            Destroy(this.gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        maxClicks--;
        _animator.SetTrigger("Click");
        BuildModeManager.Instance.increaseResources(data);
    }
}
