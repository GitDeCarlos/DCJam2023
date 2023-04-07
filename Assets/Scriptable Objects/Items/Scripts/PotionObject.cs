using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion Object", menuName = "Inventory System/Items/Consumable")]
public class PotionObject : ItemObject
{
    public int restoreHealth;
    public void Awake(){
        type = ItemType.Consumable;
    }
}
