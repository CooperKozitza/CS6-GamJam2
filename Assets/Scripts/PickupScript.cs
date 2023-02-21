using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class PickupScript : MonoBehaviour
{

    public int keyCount = 0;
    public Material startColor;
    public Material selectColor;
    public GameObject cam;
    public GameObject[] pickups;

    // Start is called before the first frame update
    void Start()
    {
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

        if (Physics.Raycast(transform.position, rayDirection.normalized, out hit, length))
        {
            GameObject pickup = hit.transform.gameObject;
            if (pickup.CompareTag("Interactable"))
            {
                UnityEngine.Debug.Log("Facing Tree");
                if (Input.GetKey(KeyCode.E))
                {
                    Hit(pickup);
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

    }
}
