using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float delay = 0.5f;
    public string header;
    [Multiline()]
    public string content;

    public TurretData data;


    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(waitToShow(delay));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        TooltipSystem.Hide();
    }

    private IEnumerator waitToShow(float delay)
    {
        yield return new WaitForSeconds(delay);
        TooltipSystem.Show(content, data, header);
    }
}
