using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator
{
    public static Dictionary<float, Color> colorMap = new() {
        [0] = new Color(46f / 255, 78f / 255, 55f / 255),
        [0.55f] = new Color(84f / 255, 85f / 255, 55f / 255),
        [0.6f] = new Color(126f / 255, 135f / 255, 112f / 255)
    };

    public static Texture2D CreateTexture(float[,] heightMap) {
            Vector2Int size = new(heightMap.GetLength(0), heightMap.GetLength(1));

            Color[] colors = new Color[size.x * size.y];
            for (short i = 0; i < (size.x * size.y); i++)
            {
               foreach (var mapping in colorMap) {
                    if (heightMap[i % size.x, i / size.y] > mapping.Key){
                        colors[i] = mapping.Value;
                    }
                    else {
                        break;
                    }
               }
            }


            Texture2D texture = new(size.x, size.y, TextureFormat.RGBA32, -1, false);
            texture.SetPixels(colors);
            texture.Apply(true);

            return texture;
        }
}
