using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Terrain
{
    public class Map
    {
        private Vector2Int size;

        MapOptions mapOptions;

        // Implement Map as a singleton

        private static Map instance = null;

        private Map() { }

        public static Map Instance {
            get {
                if (instance == null) {
                    instance = new();
                }
                return instance;
            }
        }

        public Map SetSize(int width, int height) {
            size = new Vector2Int(width, height);
            return instance;
        }

        public Map Configure(MapOptions options) {
            size = options.size;
            mapOptions = options;

            return instance;
        }

        //-----------------------------------------------------
        // Mesh

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private MeshCollider meshCollider;

        public Map SetMeshFilter(MeshFilter filter) {
            meshFilter = filter;
            return instance;
        }

        public Map SetMeshRenderer(MeshRenderer meshRenderer) {
            this.meshRenderer = meshRenderer;
            return instance;
        }

        public Map SetMeshCollider(MeshCollider meshCollider) {
            this.meshCollider = meshCollider;
            return instance;
        }

        private MeshData meshData;

        public Map GenerateMesh() {
            meshData = MeshGenerator.GenerateMeshData(heightMap, mapOptions.noiseAmplitude, mapOptions.meshResolution, mapOptions.seaLevel);
            return instance;
        }

        public void DrawMesh() {
            Mesh mesh = meshData.CreateMesh();
            meshFilter.sharedMesh = mesh;
            meshCollider.sharedMesh = mesh;
            //meshRenderer.sharedMaterial.mainTexture = TextureGenerator.CreateTexture(heightMap);
        }

        //-----------------------------------------------------
        // Terrain Generation

        private float[,] heightMap;

        public Map GenerateTerrain() {
            heightMap = new float[size.x, size.y];
            for (short octave = 1; octave < mapOptions.noiseOctaves + 1; octave++) 
            {
                Vector2 sampleAreaOffset = new(Random.Range(0, 100f), Random.Range(0, 100f));
                for (int y = 0; y < size.y; y++)
                {
                    for (int x = 0; x < size.x; x++)
                    {
                        heightMap[x, y] += Mathf.PerlinNoise(
                            (sampleAreaOffset.x + (x * (float)(octave * octave) / mapOptions.noiseZoom)),
                            (sampleAreaOffset.y + (y * (float)(octave * octave) / mapOptions.noiseZoom))
                        ) / (float)Math.Pow(2, octave);
                    }
                }
            }
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    heightMap[x, y] = (float)(Math.Pow(heightMap[x, y] + 1, mapOptions.noiseExponent) - 1);
                }
            }


            return instance;
        }

        public float GetHeightAt(Vector2Int position) {
            if (position.x > 0 && position.x < size.x && position.y > 0 && position.y < size.y) {
                if (heightMap[position.x, position.y] * mapOptions.noiseAmplitude > mapOptions.seaLevel) {
                    return heightMap[position.x, position.y] * mapOptions.noiseAmplitude - mapOptions.seaLevel;
                }
                else return 0;
            }
            return 0;
        }

        //-----------------------------------------------------
        // Trees

        public List<Vector3> TreePositions { get; private set; }

        public Map DetermineTreePositions() {
            TreePositions = new();

            for (short i = 0; i < mapOptions.groupCount; i++) {
                Vector2 groupCenter = new Vector2(Random.Range(0, (float)size.x), Random.Range(0, (float)size.y));

                int groupSize = Random.Range(0, mapOptions.groupSize);

                for (short j = 0; j < groupSize; j++) {
                    float angle = Random.Range(0, 2 * (float)Math.PI);
                    float distance = Random.Range(0, mapOptions.spread);

                    Vector2 position = new(groupCenter.x + (float)Math.Cos(angle) * distance, groupCenter.y + (float)Math.Sin(angle) * distance);
                    
                    float height = GetHeightAt(new Vector2Int((int)(position.x / mapOptions.meshResolution), (int)(position.y / mapOptions.meshResolution)));
                    if (height <= 1) {
                        i--;
                        break;
                    }

                    Debug.Log(height);

                    TreePositions.Add(new Vector3(position.x, height, position.y));
                }
            }
            return instance;
        }
    }

    [System.Serializable]
    public struct MapOptions {
        // Transform
        public Vector2Int size;

        [Header("Colors")]
        // Colors
        public Dictionary<float, Color> terrainColors;
        public float seaLevel;

        [Space(10)]

        [Header("Terrain")]
        // Terrain
        [Range(1, 20)]
        public int noiseOctaves;
        [Range(10, 150)]
        public float noiseZoom;
        [Range(1, 50)]
        public float noiseAmplitude;
        [Range(0, 4)]
        public int noiseExponent;


        [Space(10)]

        [Header("Mesh")]
        // Mesh
        [Range(1, 5)]
        public int meshResolution;

        [Space(10)]

        [Header("Trees")]
        // Trees
        [Range(1, 10)]
        public float spread;
        [Range(0, 100)]
        public int groupCount;
        [Range(0, 10)]
        public int groupSize;
    }
}
