using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    int playerHP = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
