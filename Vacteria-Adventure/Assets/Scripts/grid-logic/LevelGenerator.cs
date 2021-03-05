using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public Color[] colors;

    private void Start()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                Color PixelColor = map.GetPixel(x, y);
                if (PixelColor == colors[0])
                {
                    grid.instance.CreateCell(new int[] { x, y }, alib.CellType.head);
                }
            }
        }
    }
}