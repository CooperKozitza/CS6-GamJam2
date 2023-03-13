using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{

    [SerializeField]
    int playerHP;

    [SerializeField]
    float playerHunger;

    [SerializeField]
    int playerHungerDrainMultiplier;

    [SerializeField]
    private TextMeshProUGUI healthDisplay;

    [SerializeField]
    private TextMeshProUGUI hungerDisplay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = playerHP.ToString();
        playerHunger -= playerHungerDrainMultiplier * Time.deltaTime;
        hungerDisplay.text = playerHunger.ToString();
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
