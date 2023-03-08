using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{

    [SerializeField]
    int playerHP = 3;

    [SerializeField]
    private TextMeshProUGUI healthDisplay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = playerHP.ToString();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Acid"))
        {
            playerHP--;
            Debug.Log(playerHP);
            if (playerHP == 0)
            {
                Object.Destroy(gameObject);
            }
        }
    }
}
