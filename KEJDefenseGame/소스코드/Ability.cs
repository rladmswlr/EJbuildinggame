using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "scriptble Object/AbilityData")]
public class Ability : ScriptableObject
{
    [Header("Abilty")]
    public int AbilityId;
    public string MainText;
    public string SubText;
}
