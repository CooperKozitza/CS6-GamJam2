using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour
{
    public bool visible = false;
    public GameObject panel;
    public GameObject player;
    // Update is called once per frame

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            visible = !visible;
            if (visible)
            {
                StartCoroutine(fadeIn());
                Cursor.lockState = CursorLockMode.Confined;
                player.GetComponent<Rigidbody>().freezeRotation = true;

            }
            else
            {
                StartCoroutine(fadeOut());
                Cursor.lockState = CursorLockMode.Locked;
                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }

    IEnumerator fadeIn()
    {
        int transparency = 0;
        panel.SetActive(visible);
        while (transparency < 100)
        {
            transparency += 5;
            gameObject.GetComponent<CanvasGroup>().alpha = transparency / 100f;
            yield return null;
        }
    }

    IEnumerator fadeOut()
    {
        int transparency = 100;
        while (transparency > 0)
        {
            transparency -= 5;
            gameObject.GetComponent<CanvasGroup>().alpha = transparency / 100f;
            yield return null;
        }
        panel.SetActive(false);
    }
}
