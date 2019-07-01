using UnityEngine;
using Map;

public class Role : MonoBehaviour {

    protected Direction m_dir;
    protected RoleState m_state;

    private Animator animator;
    public int line, col;
    protected float pos_x, pos_y;

    private int xIndex = 0;
    private int yIndex = 0;

    private Transform _transform;
    private float speed = 300;
    private float tileWidth;

    private void Awake()
    {
        xIndex = 0;
        yIndex = 0;
        _transform = transform;
        m_dir = Direction.None;
        m_state = RoleState.None;
    }

    public void SetRole(int line, int col, TileCell cell)
    {
        animator = _transform.GetComponent<Animator>();
        this.line = line;
        this.col = col;
        pos_x = cell.transform.position.x;
        pos_y = cell.transform.position.y;
        tileWidth = TileManager.m_instance.tileWidth;
        Move(cell);
    }

    public void StopMove()
    {
        xIndex = 0;
        yIndex = 0;
        m_dir = Direction.None;
        m_state = RoleState.None;
        animator.Play("idle");
    }

    void Move(TileCell cell)
    {
        m_state = RoleState.Walking;
        _transform.position = new Vector3(pos_x, pos_y, _transform.position.z);
        currentCell = cell;
        nextCell = null;
        m_dir = cell.GetCellDir();
        switch(m_dir)
        {
            case Direction.Up:
                animator.Play("walk_up");
                WalkOneTile(-1, 0);
                break;
            case Direction.Right:
                animator.Play("walk_right");
                WalkOneTile(0, 1);
                break;
            case Direction.Down:
                animator.Play("walk_down");
                WalkOneTile(1, 0);
                break;
        }
    }

    private TileCell nextCell = null;
    private TileCell currentCell = null;

    public void WalkOneTile(int line, int col)
    {
        xIndex = col;
        yIndex = -line;
        this.line += line;
        this.col += col;
        pos_x += col * tileWidth;
        pos_y -= line * tileWidth;
        if(TileManager.m_instance.GetCell(this.line, this.col) != null && TileManager.m_instance.GetCell(this.line, this.col).GetCellState() == CellState.ToWalk)
        {
            nextCell = TileManager.m_instance.GetCell(this.line, this.col);
        }
        else
        {
            StopMove();
        }
        currentCell.SetCellState(CellState.Walked);
    }

    public float GetRoleTilePosX()
    {
        return pos_x;
    }
    public float GetRoleTilePosY()
    {
        return pos_y;
    }

    private void Update()
    {
        if(m_state == RoleState.Walking)
        {
            _transform.Translate(Vector3.up * speed * yIndex * Time.deltaTime);
            _transform.Translate(Vector3.right * speed * xIndex * Time.deltaTime);
            if(m_dir == Direction.Up)
            {
                if (_transform.position.y - pos_y > 0)
                {
                    Move(nextCell);
                }
            }
            else if(m_dir == Direction.Right)
            {
                if (_transform.position.x - pos_x > 0)
                {
                    Move(nextCell);
                }
            }
            else if (m_dir == Direction.Down)
            {
                if (pos_y - _transform.position.y > 0)
                {
                    Move(nextCell);
                }
            }
        }
    }
}

public enum RoleState
{
    None = 0,
    Walking = 1,
}

