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
    public int dropAmount;

    // Start is called before the first frame update
    void Start()
    {
        //dropLocation = transform.GetChild(1).transform;
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
                Instantiate(dropItem.model, new Vector3(dropLocation.position.x, dropLocation.position.y, dropLocation.position.z), Quaternion.identity);
                
            }
        }
        dropAmount--;
        if (dropAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
