using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{

    [SerializeField]
    public int playerHP;

    public float playerHunger;

    [SerializeField]
    public float maximumPlayerHunger;

    [SerializeField]
    float playerHungerDrainMultiplier;

    [SerializeField]
    float hungerDamageTime;

    [SerializeField]
    private TextMeshProUGUI healthDisplay;

    [SerializeField]
    private TextMeshProUGUI hungerDisplay;

    public Slider slider;

    bool hungerDamaging = false;

    public float hungerHeal;

    // Start is called before the first frame update
    void Start()
    {
        playerHunger = maximumPlayerHunger;
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = playerHP.ToString();

        if (playerHunger > 0)
        {
            hungerDamaging = false;

            if (playerHunger > maximumPlayerHunger)
            {
                playerHunger = maximumPlayerHunger;
            }
            playerHunger -= playerHungerDrainMultiplier * Time.deltaTime;
            hungerDisplay.text = ((int)playerHunger).ToString();
        }
        else if (!hungerDamaging)
        {
            InvokeRepeating("HungerDamage", 1, hungerDamageTime);

            hungerDamaging = true;
        }

        slider.value = playerHP;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Acid"))
        {
            playerHP--;
            Debug.Log(playerHP);
            if (playerHP == 0)
            {
                healthDisplay.text = playerHP.ToString();
                hungerDisplay.text = ((int)playerHunger).ToString();

                //Object.Destroy(gameObject);
            }
        }
    }
    private void HungerDamage()
    {
        playerHP--;
    }

    private void Eat()
    {
        playerHunger += hungerHeal;
    }
}
