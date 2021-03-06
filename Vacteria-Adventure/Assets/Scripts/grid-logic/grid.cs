using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    public static grid instance;
    public (int, int) headPosition;
    public Dictionary<(int, int), Cell> listOfCells = new Dictionary<(int, int), Cell>();
    public GameObject Cell;
    public GameObject liquidParticle;
    public Sprite[] stoneTypes;

    public delegate void Action();

    public Action UpdateCells;
    public Action OnLaser;

    public alib.Direction current_direction;
    public int tick = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        tick++;
        if (tick == 50)
        {
            if (!alib.isLab)
                BodyMovement();
            tick = 0;
        }
    }

    public void BodyMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        alib.Direction dir = alib.Direction.UP;
        if (horizontal == 1)
        {
            dir = alib.Direction.RIGHT;
        }
        else if (horizontal == -1)
        {
            dir = alib.Direction.LEFT;
        }
        else if (vertical == 1)
        {
            dir = alib.Direction.UP;
        }
        else if (vertical == -1)
        {
            dir = alib.Direction.DOWN;
        }
        else
        {
            return;
        }
        current_direction = dir;

        List<Cell> Cells = new List<Cell>();

        foreach (var cell in listOfCells.Values)
        {
            Cells.Add(cell);
        }
        foreach (var cell in Cells)
        {
            if (cell is MovingCell c)
            {
                Debug.Log(c.position[0] + " " + c.position[1]);
                c.TeamMove();
            }
        }
    }

    public void MoveCell(int x, int y, Cell c)
    {
        ((MovingCell)c).Move(new int[] { x, y });
        UpdateCells?.Invoke();
        OnLaser?.Invoke();
    }

    public void MoveBody(int x, int y, Cell c)
    {
        ((MovingCell)c).MoveBody(new int[] { x, y });
        UpdateCells?.Invoke();
        OnLaser?.Invoke();
    }

    private void Start()
    {
        CreateCell(new int[] { 12, 0 }, alib.CellType.head);
        CreateCell(new int[] { 13, 0 }, alib.CellType.simple);
        CreateCell(new int[] { 14, 0 }, alib.CellType.simple);
        CreateCell(new int[] { 15, 0 }, alib.CellType.simple);
        CreateCell(new int[] { 16, 0 }, alib.CellType.simple);
        CreateCell(new int[] { 17, 0 }, alib.CellType.simple);
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

            case alib.CellType.miner:
                a.AddComponent<MinerCell>();
                break;

            default:
                Destroy(a.GetComponent<Animator>());
                a.AddComponent<OreCell>();
                break;
        }
        Cell b = a.GetComponent<Cell>();
        GameObject c;
        if (!a.GetComponent<OreCell>())
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
}