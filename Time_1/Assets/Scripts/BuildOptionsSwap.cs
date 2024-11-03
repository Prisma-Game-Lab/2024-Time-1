using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExampleClass : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform SwapPositionTransform;
    private Vector3 SwapPosition;
    private Vector3 startPosition;
    private bool canSwap;
    public float duration;
    private float elapsedTime;
    private void Start()
    {
        startPosition = transform.position;
        canSwap = false;
        SwapPosition = SwapPositionTransform.position;
        elapsedTime = 0;
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / duration;

        if(canSwap)
        {
            transform.position = Vector3.Lerp(startPosition, SwapPosition, percentageComplete);
        }
        else
        {
            transform.position = Vector3.Lerp(SwapPosition, startPosition, percentageComplete);
        }        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        elapsedTime = 0;
        canSwap = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        elapsedTime = 0;
        canSwap = false;
    }
}