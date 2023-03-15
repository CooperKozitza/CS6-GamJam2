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

        public Map SetMeshFilter(MeshFilter filter) {
            meshFilter = filter;
            return instance;
        }

        public Map SetMeshRenderer(MeshRenderer meshRenderer) {
            this.meshRenderer = meshRenderer;
            return instance;
        }

        private MeshData mesh;

        public Map GenerateMesh() {
            mesh = MeshGenerator.GenerateMeshData(heightMap, mapOptions.amplitude, mapOptions.meshResolution);
            return instance;
        }

        public void DrawMesh() {
            meshFilter.sharedMesh = mesh.CreateMesh();
            meshRenderer.sharedMaterial.mainTexture = TextureGenerator.CreateTexture(heightMap);
        }

        //-----------------------------------------------------
        // Terrain Generation

        private float[,] heightMap;

        public Map GenerateTerrain(int octaves = 3) {
            heightMap = new float[size.x, size.y];
            for (short octave = 1; octave < octaves + 1; octave++) {
                Vector2 sampleAreaOffset = new(Random.Range(0, 100f), Random.Range(0, 100f));
                for (short i = 0; i < (size.x * size.y); i ++) {
                    heightMap[i % size.x, i / size.y] += Mathf.PerlinNoise(
                        (sampleAreaOffset.x + (i % size.x * (float)(octave * octave) / mapOptions.noiseZoom)),
                        (sampleAreaOffset.y + (i / size.y * (float)(octave * octave) / mapOptions.noiseZoom))
                    ) / (float)Math.Pow(2, octave);
                }
            }
            return instance;
        }

        public float GetHeightAt(Vector2Int position) {
            if (position.x > 0 && position.x < size.x && position.y > 0 && position.y < size.y) {
                return heightMap[position.x, position.y] * mapOptions.amplitude;
            }
            throw new IndexOutOfRangeException("argument 'position' was outside of the bounds of the map");
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
                    TreePositions.Add(new Vector3(
                        groupCenter.x + (float)Math.Cos(angle) *  distance,
                        GetHeightAt(new Vector2Int(
                            (int)((float)((groupCenter.x + Math.Cos(angle) *  distance) / (mapOptions.size.x * mapOptions.meshResolution)) * mapOptions.size.x),
                            (int)((float)((groupCenter.y + Math.Sin(angle) * distance) / (mapOptions.size.y * mapOptions.meshResolution)) * mapOptions.size.y)
                        )),
                        groupCenter.y + (float)Math.Sin(angle) * distance
                    ));
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
        [Range(10, 100)]
        public float noiseZoom;
        [Range(1, 100)]
        public float amplitude;

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