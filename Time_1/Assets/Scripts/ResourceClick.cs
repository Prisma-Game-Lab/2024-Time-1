using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ResourceClick : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI contador;
    private int n;
    private void start()
    {
        n = 0;
        contador.text = n.ToString();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clickou");
        n++;
        contador.text = n.ToString();
    }
}
