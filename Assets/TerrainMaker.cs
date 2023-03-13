using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMaker : MonoBehaviour
{
    Vector3[] verticies;
    int[] triangles;
    public int xSize;
    public int zSize;

    public float pNoiseXMult;
    public float pNoiseZMult;
    public float pNoiseYMult;

    [Range(0, 1)]
    public float baseLevel = 0.25f;

    int randChance;
    public GameObject endPoint;

    Mesh mesh;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;


        InitialShape();
        UpdateShape();

        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void Update()
    {
        
    }

    void InitialShape()
    {
        verticies = new Vector3[(xSize + 1) * (zSize + 1)];


        int i = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                //randChance = Random.Range(1, 20);

                //if(randChance == 2 || randChance == 3 || randChance == 4 || randChance == 5)
                //{
                //    float y = Mathf.PerlinNoise(x * pNoiseXMult, z * pNoiseZMult) * pNoiseYMult2;
                //    verticies[i] = new Vector3(x, y, z);

                //} else if (randChance == 6 || randChance == 7)
                //{
                //    float y = Mathf.PerlinNoise(x * pNoiseXMult, z * pNoiseZMult) * pNoiseYMult3;
                //    verticies[i] = new Vector3(x, y, z);

                //} else if(randChance == 1)
                //{
                //    float y = Mathf.PerlinNoise(x * pNoiseXMult, z * pNoiseZMult) * pNoiseYMult4;
                //    verticies[i] = new Vector3(x, y, z);
                //}
                //else
                //{
                //    float y = Mathf.PerlinNoise(x * pNoiseXMult, z * pNoiseZMult) * pNoiseYMult;
                //    verticies[i] = new Vector3(x, y, z);
                //}

                //could look into blending perlin noise for more realistic 

                float noise = Mathf.PerlinNoise((float)x / xSize * pNoiseXMult, (float)z / zSize * pNoiseZMult);
                verticies[i] = new Vector3(x, (noise > noise / 10 + baseLevel ? noise : noise / 10 + baseLevel) * pNoiseYMult, z);
                ++i;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for(int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                tris += 6;
                vert++;
            }
            vert++;
        }



    }

    void UpdateShape()
    {
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        endPoint.transform.position = new Vector3(verticies[verticies.Length - 1].x, 0, verticies[verticies.Length - 1].z) * gameObject.transform.localScale.x;
    }
}
