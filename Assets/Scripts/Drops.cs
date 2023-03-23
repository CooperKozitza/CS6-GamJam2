using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    [System.Serializable]
    public struct DropItem
    {
        public GameObject model;
        public float dropChance;
    }

    public DropItem[] dropItems;
    public Vector3 dropLocation;
    public int dropAmount;

    // Start is called before the first frame update
    void Start()
    {
        dropAmount = (int)Random.Range(3f, 6f);
        if (name == "Rock(Clone)")
        {
            dropLocation = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        } else if (name == "Tree(Clone)")
        {
            dropLocation = new Vector3(transform.position.x + 1.5f, transform.position.y + 2f, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [InspectorButton("Drop")]
    public bool drop = false;
    /// <summary>
    /// Simulates hitting resource
    /// </summary>
    public void Drop()
    {
        foreach (DropItem dropItem in dropItems)
        {
            float chance = Random.Range(0, 1f);
            if (chance <= dropItem.dropChance)
            {
                Instantiate(dropItem.model, new Vector3(dropLocation.x, dropLocation.y, dropLocation.z), Quaternion.identity);
                
            }
        }
        dropAmount--;
        if (dropAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
