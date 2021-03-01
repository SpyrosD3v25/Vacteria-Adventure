using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCells : MonoBehaviour
{
    private bool isAlreadyPressed = false;
    private GameObject PlaceHolder;
    private Cell MovingCell;
    private bool isMoving = false;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isAlreadyPressed)
            {
                GetPositionData(true);
            }
            isAlreadyPressed = true;
        }
        else if (!Input.GetMouseButton(0))
        {
            if (isAlreadyPressed)
            {
                GetPositionData(false);
            }
            isAlreadyPressed = false;
        }
    }

    private void CreatePlaceHolder(alib.CellType type)
    {
        PlaceHolder = Instantiate(grid.instance.PlaceHolder);
        PlaceHolder.GetComponent<PlaceHolder>().cellType = type;
    }

    private void GetPositionData(bool onCreate)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = (int)Mathf.Round(mousePos.x);
        int y = (int)Mathf.Round(mousePos.y);
        if (onCreate)
        {
            if (grid.instance.listOfCells.ContainsKey((x, y)))
            {
                MovingCell = grid.instance.listOfCells[(x, y)];
                if (MovingCell is MovingCell && ((MovingCell)MovingCell).mayMove)
                {
                    isMoving = true;
                    CreatePlaceHolder(MovingCell.cellType);
                }
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                Destroy(PlaceHolder);
                if (grid.instance.listOfCells.ContainsKey((x, y)))
                {
                    Cell a = grid.instance.listOfCells[(x, y)];
                    if (a.cellType == alib.CellType.laser)
                    {
                        a.Destroy();
                        MoveCell(x, y);
                    }
                    else if (a.cellType == alib.CellType.simple)
                    {
                        if (MovingCell.cellType == alib.CellType.simple)
                        {
                            a.Destroy();
                            MovingCell.Destroy();
                            grid.instance.CreateCell(new int[] { x, y }, alib.CellType.hardened);
                            grid.instance.UpdateCells();
                            grid.instance.OnLaser();
                        }
                        if (MovingCell.cellType == alib.CellType.hardened)
                        {
                            a.Destroy();
                            MovingCell.Destroy();
                            grid.instance.CreateCell(new int[] { x, y }, alib.CellType.miner);
                            grid.instance.UpdateCells();
                            grid.instance.OnLaser();
                        }
                    }
                    else if (a.cellType == alib.CellType.hardened)
                    {
                        if (MovingCell.cellType == alib.CellType.simple)
                        {
                            a.Destroy();
                            MovingCell.Destroy();
                            grid.instance.CreateCell(new int[] { x, y }, alib.CellType.miner);
                            grid.instance.UpdateCells();
                            grid.instance.OnLaser();
                        }
                    }
                }
                else
                {
                    MoveCell(x, y);
                }
            }
        }
    }

    public void MoveCell(int x, int y)
    {
        ((MovingCell)MovingCell).Move(new int[] { x, y });
        grid.instance.UpdateCells?.Invoke();
        grid.instance.OnLaser?.Invoke();
    }
}