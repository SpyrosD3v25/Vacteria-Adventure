using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCell : Cell
{
    public bool disable = false;
    public bool mayMove = true;
    public bool isMoving = false;

    public virtual void Move(int[] pos)
    {
        int[] old = position;
        if (grid.instance.isPositionValid((pos[0], pos[1]), (old[0], old[1])))
        {
            MoveBody(pos);
        }
    }

    public virtual void MoveBody(int[] pos)
    {
        int[] old = position;
        position = pos;
        UpdateKey();
        grid.instance.listOfCells.Remove((old[0], old[1]));
    }

    public void Update()
    {
        if (isMoving)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }

    public void TeamMove()
    {
        (int, int) offset = (0, 0);
        switch (grid.instance.current_direction)
        {
            case alib.Direction.UP:
                offset = (0, 1);
                break;

            case alib.Direction.DOWN:
                offset = (0, -1);
                break;

            case alib.Direction.RIGHT:
                offset = (1, 0);
                break;

            case alib.Direction.LEFT:
                offset = (-1, 0);
                break;
        }
        Cell a = null;
        if (!grid.instance.listOfCells.ContainsKey((position[0] + offset.Item1, position[1] + offset.Item2)))
        {
            grid.instance.MoveBody(position[0] + offset.Item1, position[1] + offset.Item2, this);
            if (grid.instance.listOfCells.ContainsKey((position[0] - offset.Item1 * 2, position[1] - offset.Item2 * 2)))
            {
                Debug.Log("a");
                a = grid.instance.listOfCells[(position[0] - offset.Item1 * 2, position[1] - offset.Item2 * 2)];
                if (a is MovingCell)
                {
                    ((MovingCell)a).TeamMove();
                }
            }
        }
    }

    public void setMove()
    {
        if (isMoving)
        {
            isMoving = false;
        }
        else isMoving = true;
    }
}