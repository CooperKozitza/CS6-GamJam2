using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Terrain {
    public static class MeshGenerator {
        public static MeshData GenerateMeshData(float[,] heightMap, float amplitude = 5, int scale = 1, float seaLevel = 0) {
            Vector2Int size = new(heightMap.GetLength(0), heightMap.GetLength(1));

            MeshData data = new(size.x, size.y);

            int i = 0;
            for (int y = 0; y < size.y; y++) {
                for (int x = 0; x < size.x; x++) {
                    float height = heightMap[x, y] * amplitude;

                    height = height < seaLevel ? 0 : height - seaLevel;

                    data.vertices[y * size.x + x] = new Vector3(x, height, y);
                    data.uvs[i] = new Vector2(x / (float)size.x, y / (float)size.y);

                    if (x < size.x - scale && y < size.y - scale) {
                        data.AddTriangle(i, i + size.x + scale, i + size.x);
                        data.AddTriangle(i + size.x + scale, i, i + scale);
                    }
                    i++;
                }
            }
            return data;
        }
    }

    public struct MeshData {
        public Vector3[] vertices;
        public Vector2[] uvs;

        public int[] triangles;

        private int triangleIndex;

        public MeshData(int width, int height) {
            vertices = new Vector3[width * height];
            uvs = new Vector2[width * height];

            triangles = new int[(width - 1) * (height - 1) * 6];

            triangleIndex = 0;
        }

        public void AddTriangle(int a, int b, int c) {
            triangles[triangleIndex] = a;
            triangles[triangleIndex + 1] = b;
            triangles[triangleIndex + 2] = c;
            triangleIndex += 3;
        }

        public Mesh CreateMesh() {
            Mesh mesh = new();

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;

            mesh.triangles = mesh.triangles.Reverse().ToArray();

            mesh.RecalculateNormals();

            return mesh;
        }
    }
}
