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
    public Sprite Head;
    public Sprite Simple;
    public Sprite Hardened;

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
        CreateCell(new int[] { 6, -2 }, alib.CellType.laser);
        CreateCell(new int[] { 0, 3 }, alib.CellType.dirt);
        CreateCell(new int[] { 1, 3 }, alib.CellType.dirt);
        CreateCell(new int[] { -1, 3 }, alib.CellType.stone);
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
                a.AddComponent<Material>();
                break;

            case alib.CellType.dirt:
                a.AddComponent<Material>();
                break;

            default: return;
        }
        Cell b = a.GetComponent<Cell>();
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

    public void DestroyCell((int, int) a)
    {
        if (listOfCells.ContainsKey(a))
        {
            listOfCells[(a)].Destroy();
        }
    }
}