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
    public Transform dropLocation;

    // Start is called before the first frame update
    void Start()
    {
        dropLocation = transform.GetChild(1).transform;
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
    void Drop()
    {
        foreach (DropItem dropItem in dropItems)
        {
            float chance = Random.Range(0, 1f);
            if (chance <= dropItem.dropChance)
            {
                Instantiate(dropItem.model, dropLocation);
            }
        }
    }
}
