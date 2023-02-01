using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface representing an item in the inventory
// can be a stack of multiple items, or represent a single item

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class InventoryItem : ScriptableObject
{
    [Header("Inventory Icon")]
    public Texture2D icon;

    //====================================================================================================

    [Header("Item Data")]
    public int count = 1;

    //====================================================================================================

    [Header("Item Characteristics")]
    [Range(1, 5)]
    public double mass;

    [Space(10)]
    public bool canStack;
    public int maxStackCount;

    [Space(10)]
    public Type type;

    public enum Type
    {
        Consumable, Tool, Structure, Weapon, Miscellaneous
    }

    //====================================================================================================

    [Header("Item Info")]
    public string itemName;
    [TextArea]
    public string description;

    public int id;
    public string uid;

    //====================================================================================================

    public InventoryItem(InventoryItem copy, int count = 1)
    {
        icon = copy.icon;

        mass = copy.mass;
        canStack = copy.canStack;
        maxStackCount = copy.maxStackCount;
        type = copy.type;

        itemName = copy.itemName;
        description = copy.description;

        id = copy.id;
        uid = createUid();

        this.count = count;
    }

    public InventoryItem()
    {
        uid = createUid();
    }

    //====================================================================================================

    private string createUid()
    {
        return Hash128.Compute(System.DateTime.Now.ToLongDateString()).ToString();
    }

    void mergeStack(InventoryItem stack)
    {
        if (stack.canStack == false || stack.id != this.id) return;
        if (count + stack.count > maxStackCount)
        {
            stack.count = maxStackCount - count;
            count = maxStackCount;
        }
        else
        {
            count += stack.count;
            // get rid of 'stack'
        }
    }
}
