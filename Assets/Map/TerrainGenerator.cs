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
            GameObject tree = Instantiate(treePrefab, position, treePrefab.transform.rotation);
            tree.transform.SetParent(treeParent.transform);
        }

        GameObject rockParent = new("rocks");

        _map.DetermineObjectPositions();
        foreach (Vector3 position in _map.ObjectPositions)
        {
            GameObject tree = Instantiate(rockPrefab, position, rockPrefab.transform.rotation);
            tree.transform.SetParent(rockParent.transform);
        }
    }
}
