using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface representing an inventory

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriptableObjects/InventoryBearer", order = 1)]
public class InventoryBearer : ScriptableObject
{
    private List<InventoryItem> inventoryItems;

    public InventoryItem fetchItem(InventoryItem match)
    {
        return inventoryItems.Find(x => x == match);
    }

    public InventoryItem fetchNotFullStack(InventoryItem match)
    {
        return inventoryItems.Find(x => x.id == match.id && x.canStack == true && x.count < x.maxStackCount);
    }

    public InventoryItem fetchFullStack(InventoryItem match)
    {
        return inventoryItems.Find(x => x.id == match.id && x.canStack == true && x.count == x.maxStackCount);
    }

    public void pickup(InventoryItem item, int count = 1)
    {
        InventoryItem existingItem = fetchItem(item);
        if (existingItem != null && existingItem.canStack)
        {
            if (existingItem.count + count <= existingItem.maxStackCount)
            {
                existingItem.count += count;
            }
            else
            {
                InventoryItem stack = existingItem;
                do
                {
                    //fill existing stacks, and create new ones until count
                    if (stack.count + count > stack.maxStackCount)
                    {
                        count -= stack.maxStackCount - stack.count;
                        stack.count = stack.maxStackCount;

                        stack = new(stack);
                        inventoryItems.Add(stack);
                    }
                    else
                    {
                        stack.count += count;
                    }
                } while (count > 0);
            }
        }
        else
        {
            inventoryItems.Add(item);
        }

        // TODO: remove object after pickup
    }

    public void drop(InventoryItem item, int count = 1)
    {
        
    }
}
