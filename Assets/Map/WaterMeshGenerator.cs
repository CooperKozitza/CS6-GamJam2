using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class WaterMeshGenerator : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Vector2Int size;
    public float scale;

    private MeshData meshData;

    // Start is called before the first frame update
    void Start()
    {
        meshData = MeshGenerator.GenerateMeshData(size, transform.position.y, scale);
        Debug.Log(meshData.CreateMesh());
        meshFilter.mesh = meshData.CreateMesh();
    }
}
