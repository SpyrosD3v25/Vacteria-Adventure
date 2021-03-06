using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class alib
{
    public enum CellType { head, simple, hardened, miner, vines, ultisols, scoria, tuff, myionite, steel, titanium };

    public enum Direction { UP, DOWN, RIGHT, LEFT };

    public enum GENES { Production, Speed, Hardness, Immune, MutationRate, ToxicAbility, Volume };

    public static bool isMoving = false;
    public static bool isLab = false;

    public static List<Gene> genes = new List<Gene>();

    public static void MoveCell(int x, int y, Cell a)
    {
        ((MovingCell)a).Move(new int[] { x, y });
        grid.instance.UpdateCells?.Invoke();
        grid.instance.OnLaser?.Invoke();
    }
}