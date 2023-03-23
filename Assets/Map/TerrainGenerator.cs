using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Terrain;

public class TerrainGenerator : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    [Space(10)]
    public MapOptions options;

    public GameObject treePrefab;
    public GameObject rockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Map _map = Map.Instance;
        _map.Configure(options)
            .SetMeshFilter(meshFilter)
            .SetMeshRenderer(meshRenderer)
            .SetMeshCollider(meshCollider);

        _map.GenerateTerrain()
            .GenerateMesh()
            .DrawMesh();

        GameObject treeParent = new("trees");

        _map.DetermineObjectPositions();
        foreach(Vector3 position in _map.ObjectPositions) {
            GameObject tree = Instantiate(treePrefab, position, new Quaternion(Random.Range(0f, 360f), Random.Range(0f, 360f), 0, 0));
            tree.transform.SetParent(treeParent.transform);
            tree.transform.rotation = Quaternion.AngleAxis(Random.Range(0f,360f), Vector3.up);
        }

        GameObject rockParent = new("rocks");

        _map.DetermineObjectPositions();
        foreach (Vector3 position in _map.ObjectPositions)
        {
            GameObject tree = Instantiate(rockPrefab, position, new Quaternion(0, Random.Range(0f, 360f), 0, 0));
            tree.transform.SetParent(rockParent.transform);
            tree.transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        }
    }
}
