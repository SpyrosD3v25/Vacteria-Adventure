using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    public static grid instance;
    public (int, int) headPosition;
    public Dictionary<(int, int), Cell> listOfCells = new Dictionary<(int, int), Cell>();
    public GameObject Cell;
    public GameObject PlaceHolder;
    public GameObject liquidParticle;
    public Sprite Head;
    public Sprite Simple;
    public Sprite Hardened;

    //Movement stuff (I AM CURRENTLY DRINKING MILK!!!!!!)
    const int max_width = 100;
    const int min_width = 100;

    public int[,] positions2D = new int[max_width, min_width];

    public delegate void Action();

    public Action UpdateCells;
    public Action OnLaser;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateCell(new int[] { 0, 0 }, alib.CellType.head);
        CreateCell(new int[] { 1, 0 }, alib.CellType.simple);
        CreateCell(new int[] { 2, 0 }, alib.CellType.simple);
        CreateCell(new int[] { 3, 0 }, alib.CellType.simple);
        CreateCell(new int[] { 4, 0 }, alib.CellType.simple);
        CreateCell(new int[] { 5, 0 }, alib.CellType.simple);
        CreateCell(new int[] { 6, 0 }, alib.CellType.simple);
        CreateCell(new int[] { 6, 1 }, alib.CellType.simple);
        //CreateCell(new int[] { 6, -2 }, alib.CellType.laser);
        //CreateCell(new int[] { 0, 3 }, alib.CellType.dirt);
        //CreateCell(new int[] { 1, 3 }, alib.CellType.dirt);
        //CreateCell(new int[] { -1, 3 }, alib.CellType.stone);
    }

    public void CreateCell(int[] position, alib.CellType cellType)
    {
        GameObject a = Instantiate(Cell, new Vector2(position[0], position[1]), Quaternion.identity, transform);
        switch (cellType)
        {
            case alib.CellType.head:
                a.AddComponent<HeadCell>();
                break;

            case alib.CellType.simple:
                a.AddComponent<SimpleCell>();
                break;

            case alib.CellType.hardened:
                a.AddComponent<HardenedCell>();
                break;

            case alib.CellType.laser:
                a.AddComponent<Lasercell>();
                break;

            case alib.CellType.miner:
                a.AddComponent<MinerCell>();
                break;

            case alib.CellType.stone:
                a.AddComponent<SimpleCell>();
                break;

            case alib.CellType.dirt:
                a.AddComponent<SimpleCell>();
                break;

            default: return;
        }
        Cell b = a.GetComponent<Cell>();
        GameObject c;
        for (int i = 0; i < 45; i++)
        {
            c = Instantiate(liquidParticle, a.transform.position, Quaternion.identity);
            AttractToParent at = c.GetComponent<AttractToParent>();
            at.TargetPosition = a.transform;
        }
        b.position = position;
        b.cellType = cellType;
        b.UpdateData();
        b.UpdateKey();

    }

    public bool isPositionValid((int, int) pos, (int, int)? old)
    {
        List<(int, int)> searched = new List<(int, int)>();
        if (old != null && old != headPosition)
            searched.Add(old.Value);
        return CheckSidePositions(pos, searched);
    }

    public bool Check((int, int) pos, List<(int, int)> searched)
    {
        if (pos == headPosition) return true;
        searched.Add(pos);
        return CheckSidePositions(pos, searched);
    }

    public bool CheckSidePositions((int, int) pos, List<(int, int)> searched)
    {
        bool result = false;
        if (listOfCells.ContainsKey((pos.Item1 + 1, pos.Item2)) && !searched.Contains((pos.Item1 + 1, pos.Item2)))
        {
            if (!result) result = Check(((pos.Item1 + 1, pos.Item2)), searched);
        }
        if (listOfCells.ContainsKey((pos.Item1 - 1, pos.Item2)) && !searched.Contains((pos.Item1 - 1, pos.Item2)))
        {
            if (!result) result = Check(((pos.Item1 - 1, pos.Item2)), searched);
        }
        if (listOfCells.ContainsKey((pos.Item1, pos.Item2 + 1)) && !searched.Contains((pos.Item1, pos.Item2 + 1)))
        {
            if (!result) result = Check(((pos.Item1, pos.Item2 + 1)), searched);
        }
        if (listOfCells.ContainsKey((pos.Item1, pos.Item2 - 1)) && !searched.Contains((pos.Item1, pos.Item2 - 1)))
        {
            if (!result) result = Check(((pos.Item1, pos.Item2 - 1)), searched);
        }
        return result;
    }

    public Cell[] GetSideCells(int x, int y)
    {
        Cell[] a = new Cell[4];
        if (listOfCells.ContainsKey((x + 1, y)))
            a[0] = listOfCells[(x + 1, y)];
        if (listOfCells.ContainsKey((x - 1, y)))
            a[1] = listOfCells[(x - 1, y)];
        if (listOfCells.ContainsKey((x, y + 1)))
            a[2] = listOfCells[(x, y + 1)];
        if (listOfCells.ContainsKey((x, y - 1)))
            a[3] = listOfCells[(x, y - 1)];
        return a;
    }

    public void DestroyCell((int, int) a)
    {
        if (listOfCells.ContainsKey(a))
        {
            listOfCells[(a)].Destroy();
        }
    }
}