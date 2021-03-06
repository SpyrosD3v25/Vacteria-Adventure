using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public Color[] colors;

    private void Start()
    {
        for (int x = 0; x < 32; x++)
        {
            for (int y = 0; y < 32; y++)
            {
                Color PixelColor = map.GetPixel(x, y);
                if (PixelColor == colors[0])
                {
                    grid.instance.CreateCell(new int[] { x, y }, alib.CellType.vines);
                }
                else if (PixelColor == colors[1])
                {
                    grid.instance.CreateCell(new int[] { x, y }, alib.CellType.ultisols);
                }
                else if (PixelColor == colors[2])
                {
                    grid.instance.CreateCell(new int[] { x, y }, alib.CellType.scoria);
                }
                else if (PixelColor == colors[3])
                {
                    grid.instance.CreateCell(new int[] { x, y }, alib.CellType.tuff);
                }
                else if (PixelColor == colors[4])
                {
                    grid.instance.CreateCell(new int[] { x, y }, alib.CellType.myionite);
                }
                else if (PixelColor == colors[5])
                {
                    grid.instance.CreateCell(new int[] { x, y }, alib.CellType.steel);
                }
                else if (PixelColor == colors[6])
                {
                    grid.instance.CreateCell(new int[] { x, y }, alib.CellType.titanium);
                }
            }
        }
    }
}