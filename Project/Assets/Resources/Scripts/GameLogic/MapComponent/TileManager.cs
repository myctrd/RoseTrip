using UnityEngine;
using Map;

public class TileManager : MonoBehaviour {

    public static TileManager m_instance;

    public TileBase draggedTile, targetTile;

    public TileBase[] tiles;

    public int tileWidth = 100;

    [SerializeField]
    private Role role;

    public bool roleMove = false;

    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
    }

    private void Start()
    {
        roleMove = false;
        GenerateRandomMap();
    }

    public void StartMove()
    {
        role.SetRole(tiles[0].startLine, tiles[0].startCol, tiles[0].GetStartCell());
        roleMove = true;
    }

    private int maxLine = 8;
    private int tileCol = 4;
    private int maxCol = 16;

    public TileCell GetCell(int line, int col)
    {
        int i = col / tileCol;
        if (col < 0 || col >= maxCol || line < 0 || line >= maxLine)
            return null;
        return tiles[i].GetCell(line, col % tileCol);
    }

    #region 地图生成

    private void GenerateRandomMap()
    {
        ActivateCell(Random.Range(0, maxLine), 0);
    }

    private Direction RandomDir(int line, int col)
    {
        var dir = Direction.None;
        if (line == 0)
        {
            int i = Random.Range(1, 3);
            switch (i)
            {
                case 1:
                    dir = Direction.Right;
                    break;
                case 2:
                    dir = Direction.Down;
                    break;
            }
        }
        else if (line == maxLine - 1)
        {
            int i = Random.Range(1, 3);
            switch (i)
            {
                case 1:
                    dir = Direction.Right;
                    break;
                case 2:
                    dir = Direction.Up;
                    break;
            }
        }
        else
        {
            int i = Random.Range(1, 4);
            switch (i)
            {
                case 1:
                    dir = Direction.Up;
                    break;
                case 2:
                    dir = Direction.Right;
                    break;
                case 3:
                    dir = Direction.Down;
                    break;
            }
        }

        switch (dir)
        {
            case Direction.Up:
                if (GetCell(line - 1, col).GetCellState() != CellState.None)
                    return (RandomDir(line, col));
                return dir;
            case Direction.Right:
                if (col == maxCol - 1)
                    return dir;
                if (GetCell(line, col + 1).GetCellState() != CellState.None)
                    return (RandomDir(line, col));
                return dir;
            case Direction.Down:
                if (GetCell(line + 1, col).GetCellState() != CellState.None)
                    return (RandomDir(line, col));
                return dir;
            default:
                return dir;
        }
    }

    private void ActivateCell(int line, int col)
    {
        GetCell(line, col).SetCellState(CellState.ToWalk);
        int i = col / tileCol;
        if(tiles[i].GetStartCell() == null)
        {
            tiles[i].SetStartCell(line, col % tileCol);
        }

        var dir = RandomDir(line, col);
        GetCell(line, col).SetCellDir(dir);
        if (col == maxCol - 1)
            return;
        switch (dir)
        {
            case Direction.Up:
                ActivateCell(line - 1, col);
                break;
            case Direction.Right:
                ActivateCell(line, col + 1);
                break;
            case Direction.Down:
                ActivateCell(line + 1, col);
                break;
        }
    }

    #endregion

    public void SetDraggedTile(TileBase tile)
    {
        draggedTile = tile;
    }

    public void SetTargetTile(TileBase tile)
    {
        targetTile = tile;
    }

    private TileBase tile_t;

    public void SwitchTile()
    {
        if(targetTile != null)
        {
            draggedTile.transform.localPosition = targetTile.transform.localPosition;
            targetTile.transform.localPosition = draggedTile.GetOriginPosition();

            int a = 0;
            int b = 0;
            for (int i = 0; i < tiles.Length; i ++)
            {
                if(tiles[i].name == draggedTile.gameObject.name)
                {
                    a = i;
                }

                if (tiles[i].name == targetTile.gameObject.name)
                {
                    b = i;
                }
            }
            tile_t = tiles[a];
            tiles[a] = tiles[b];
            tiles[b] = tile_t;
            tile_t = null;
        }
        else
        {
            draggedTile.RevertPosition();
        }
        draggedTile = null;
        targetTile = null;
    }
}
