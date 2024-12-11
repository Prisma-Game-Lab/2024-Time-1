using UnityEngine;
using TMPro;

public class UpdateScreenResources : MonoBehaviour
{
    public TextMeshProUGUI value;
    public ResourceData data;

    void Update()
    {
        if(data.eletronicAmount > 0)
            value.text = BuildModeManager.Instance.getResourceEletronic().ToString();
        if(data.metalAmount > 0)
            value.text = BuildModeManager.Instance.getResourceMetal().ToString();
        if(data.prismAmount > 0)
            value.text = BuildModeManager.Instance.getResourcePrism().ToString();
        if(data.uraniumAmount > 0)
            value.text = BuildModeManager.Instance.getResourceUranium().ToString();
    }   
}
