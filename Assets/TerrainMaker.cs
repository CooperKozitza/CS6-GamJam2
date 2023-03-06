using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMaker : MonoBehaviour
{
    Vector3[] verticies;
    int[] triangles;
    public int xSize;
    public int zSize;
    public float pNoiseYMult;
    public float pNoiseXMult;
    public float pNoiseZMult;

    Mesh mesh;

    void Start()
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
        for(int z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                //pNoiseYMult = Random.Range(1f, 5f);
                float y = Mathf.PerlinNoise(x * pNoiseXMult, z * pNoiseZMult) * pNoiseYMult;
                verticies[i] = new Vector3(x, y, z);
                i++;
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
    }
}
