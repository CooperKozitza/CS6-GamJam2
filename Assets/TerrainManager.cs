using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{

    public GameObject[] thingsToSpawn;
    public int numOfObjects;
    int rand;
    Vector3 spawnPos;

    void Start()
    {
        spawnPos = GetComponentInChildren<Transform>().position;
        for(int i = 0; i < numOfObjects; i++)
        {
            rand = Random.Range(1, thingsToSpawn.Length);
            //Instantiate(thingsToSpawn[rand],)

        }




    }


    void Update()
    {
        
    }
}
