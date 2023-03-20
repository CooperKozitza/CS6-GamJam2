using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class PickupScript : MonoBehaviour
{

    //public int keyCount = 0;
    //public Material startColor;
    //public Material selectColor;
    public GameObject cam;
    //public GameObject[] pickups;
    public Drops drops;
    public float hitCooldown;
    public float hitDelay;
    public InventoryBearer inventoryBearer;

    // Start is called before the first frame update
    void Start()
    {
        hitCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPickup();
    }

    private void CheckPickup()
    {
        float length = 4;
        RaycastHit hit;
        Vector3 rayDirection = cam.transform.forward;

        if (hitCooldown > 0)
            hitCooldown -= Time.deltaTime;
        else
            hitCooldown = 0;

        if (Physics.Raycast(transform.position, rayDirection.normalized, out hit, length))
        {
            GameObject pickup;
            if (hit.transform.gameObject.CompareTag("Interactable")) {
                pickup = hit.transform.parent.gameObject;
            } else
            {
                return;
            }

            //Debug.Log(pickup.name);
            if (pickup.CompareTag("Interactable"))
            {
                //Debug.Log("Facing Resource");
                if (Input.GetMouseButtonDown(0) && hitCooldown == 0)
                {
                    Hit(pickup);
                    hitCooldown = hitDelay;
                }
            }
        }
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }

    private void Hit(GameObject pickup)
    {
        drops = pickup.GetComponent<Drops>();
        drops.Drop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            InventoryPickup pickup = collision.transform.GetComponentInParent<InventoryPickup>();

            Debug.Log(pickup == null ? "nope" : "yup");

            if (pickup == null) return;

            inventoryBearer.Pickup(pickup.item, 1);

            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }
}
