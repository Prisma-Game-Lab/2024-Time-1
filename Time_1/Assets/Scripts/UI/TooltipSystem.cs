using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    public Tooltip tooltip;

    public void Awake()
    {
        current = this;
    }

    public static void Show(string content, TurretData data, string header = "")
    {
        current.tooltip.SetData(content, data, header);
        current.tooltip.ShowTooltip();
    }

    public static void Hide()
    {
        current.tooltip.HideTooltip();
    }
}
