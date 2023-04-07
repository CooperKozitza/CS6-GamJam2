using UnityEngine;

// interface representing an item in the inventory
// can be a stack of multiple items, or represent a single item

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class InventoryItem : ScriptableObject
{
    [Header("Inventory Icon")]
    public Sprite icon;

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
        Consumable, Tool, Structure, Weapon, Edible, Miscellaneous
    }

    //====================================================================================================

    [Header("Item Info")]
    public string itemName;
    [TextArea]
    public string description;

    public int id;
    public string uid;

    //====================================================================================================

    public InventoryItem(InventoryItem copy)
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
}
