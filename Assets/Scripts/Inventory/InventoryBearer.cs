using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface representing an inventory

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriptableObjects/InventoryBearer", order = 1)]
public class InventoryBearer : ScriptableObject
{
    public List<Item> inventoryItems;

    public Item fetchItem(InventoryItem match)
    {
        return inventoryItems.Find(x => x.itemData == match);
    }

    public Item fetchNotFullStack(InventoryItem match)
    {
        return inventoryItems.Find(x => x.itemData.id == match.id && x.itemData.canStack == true && x.count < x.itemData.maxStackCount);
    }

    public Item fetchFullStack(InventoryItem match)
    {
        return inventoryItems.Find(x => x.itemData.id == match.id && x.itemData.canStack == true && x.count == x.itemData.maxStackCount);
    }

    public void pickup(InventoryItem item, int pickupCount = 1)
    {
        Item existingItem = fetchItem(item);
        if (existingItem.itemData.canStack)
        {
            if (existingItem.count + pickupCount <= existingItem.itemData.maxStackCount)
            {
                existingItem.count += pickupCount;
            }
            else
            {
                Item stack = existingItem;
                do
                {
                    //fill existing stacks, and create new ones until count
                    if (stack.count + pickupCount > stack.itemData.maxStackCount)
                    {
                        pickupCount -= stack.itemData.maxStackCount - stack.count;
                        stack.count = stack.itemData.maxStackCount;

                        stack = new Item{ itemData = item, count = stack.count };
                        inventoryItems.Add(stack);
                    }
                    else
                    {
                        stack.count += pickupCount;
                    }
                } while (pickupCount > 0);
            }
        }
        else
        {
            inventoryItems.Add(new Item{ itemData = item, count = pickupCount });
        }

        // TODO: remove object after pickup
    }

    public void drop(InventoryItem item, int count = 1)
    {
        
    }
}
[System.Serializable]
public struct Item
{
    public InventoryItem itemData;
    [Range(0, 50)]
    public int count;

    public void mergeStack(Item stack)
    {
        if (stack.itemData.canStack == false || stack.itemData.id != this.itemData.id) return;
        if (count + stack.count > itemData.maxStackCount)
        {
            stack.count = itemData.maxStackCount - count;
            count = itemData.maxStackCount;
        }
        else
        {
            count += stack.count;
            // get rid of 'stack'
        }
    }
}
