using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{

    public GameObject[] thingsToSpawn;
    public int numOfObjects;
    int rand;
    public Vector3 spawnPos;
    public GameObject endpoint;
    public GameObject terrain;
    public float terrainX;
    public float terrainZ;
    public float endX;
    public float endZ;

    void Start()
    {
        terrainX = terrain.transform.position.x;
        terrainZ = terrain.transform.position.z;
        endX = endpoint.transform.position.x;
        endZ = endpoint.transform.position.z;
        spawnPos = GetComponentInChildren<Transform>().position;
        for(int i = 0; i < numOfObjects; i++)
        {
            rand = Random.Range(0, thingsToSpawn.Length);
            //spawnPos = new Vector3(Random.Range(1, 100), 0, Random.Range(1, 100));
            //spawnPos = new Vector3(Random.Range(terrain.transform.position.x, endpoint.transform.position.x), 0, Random.Range(terrain.transform.position.z, endpoint.transform.position.z));
            spawnPos = new Vector3(Random.Range(terrainX, endX), 0, Random.Range(terrainZ, endZ));
            Instantiate(thingsToSpawn[rand], spawnPos, Quaternion.identity);

        }




    }


    void Update()
    {
        
    }
}
