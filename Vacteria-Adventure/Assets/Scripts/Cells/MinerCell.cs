using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerCell : SimpleCell
{
    private int tick = 0;
    private int tries = 10;

    private void FixedUpdate()
    {
        if (isActive)
            tick++;
        tries = 10;
        if (tick == 100)
        {
            tick = 0;
            Mine();
        }
    }

    public int[] RandomValues()
    {
        int[] a = new int[2] { 0, 0 };
        if (Random.Range(0, 2) == 0)
        {
            a[0] = Random.Range(0, 2);
            if (a[0] == 0) a[0] = -1;
        }
        else
        {
            a[1] = Random.Range(0, 2);
        }
        if (a[1] == 0) a[1] = -1;
        return a;
    }

    public void Mine()
    {
        tries--;
        if (tries != 0)
        {
            int[] a = RandomValues();
            if (!grid.instance.listOfCells.ContainsKey((position[0] + a[0], position[1] + a[1]))) Mine();
            else
            {
                if (grid.instance.listOfCells[(position[0] + a[0], position[1] + a[1])] is OreCell)
                {
                    alib.CellType type = grid.instance.listOfCells[(position[0] + a[0], position[1] + a[1])].cellType;
                    grid.instance.listOfCells[(position[0] + a[0], position[1] + a[1])].Destroy();
                }
                else Mine();
            }
        }
    }
}