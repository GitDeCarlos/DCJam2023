using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Curse Object", menuName = "Inventory System/Items/Curse")]
public class CurseObject : ItemObject
{
    public float damageDebuff;
    public float healthDebuff;
    public float armorDebuff;
    public float sanityDebuff;
    public float healthDamageOverTimeDebuff;
    
    public void Awake(){
        type = ItemType.Curse;
    }
}
