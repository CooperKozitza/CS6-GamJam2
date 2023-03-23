using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour
{
    public bool visible = false;
    public GameObject panel;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            visible = !visible;
            if (visible)
            {
                StartCoroutine(fadeIn());
            }
            else
            {
                StartCoroutine(fadeOut());
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
