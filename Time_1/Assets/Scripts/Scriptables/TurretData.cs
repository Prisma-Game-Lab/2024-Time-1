using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turret Data", fileName = "New Turret")]
public class TurretData : ScriptableObject
{
    public int requiredEletronic = 0;
    public int requiredIron = 0;
    public int requiredPrism = 0;
    public int requiredUranium = 0;

    public Sprite[] ResourcesIcons;
}
