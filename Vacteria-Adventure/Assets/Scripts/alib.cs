using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class alib
{
    public enum CellType { head, simple, hardened, laser, stone, dirt, miner };

    public enum GENES { Production, Speed, Hardness, Immune, Foods, ToxicAbility, Volume };

    public static bool isMoving = false;
    public static bool isEditMode = true;

    public static void MoveCell(int x, int y, Cell a)
    {
        ((MovingCell)a).Move(new int[] { x, y });
        grid.instance.UpdateCells?.Invoke();
        grid.instance.OnLaser?.Invoke();
    }
}