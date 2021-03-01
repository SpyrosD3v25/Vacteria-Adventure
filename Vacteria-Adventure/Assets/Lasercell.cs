using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasercell : Cell
{
    private void Start()
    {
        grid.instance.OnLaser += LaserBeam;
    }

    public void LaserBeam()
    {
        for (int i = 1; i < 10; i++)
        {
            if (transform.eulerAngles.z == 0)
            {
                if (grid.instance.listOfCells.ContainsKey((position[0], position[1] + i)))
                {
                    Debug.Log(grid.instance.listOfCells[(position[0], position[1] + i)]);
                    if (destroy(grid.instance.listOfCells[(position[0], position[1] + i)]) == true) break;
                }
            }
            if (transform.eulerAngles.z == 90)
            {
            }
            if (transform.eulerAngles.z == 180)
            {
            }
            if (transform.eulerAngles.z == 270)
            {
            }
        }
    }

    private bool destroy(Cell a)
    {
        if (a is HardenedCell)
        {
        }
        else
            a.Destroy();
        return true;
    }

    public override void Destroy()
    {
        grid.instance.OnLaser -= LaserBeam;
        base.Destroy();
    }
}