using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class TileCell : MonoBehaviour
    {
        private Image image;

        private GameObject dir_object;

        private void Awake()
        {
            image = transform.GetComponent<Image>();
            dir_object = transform.Find("dir").gameObject;
        }

        #region 状态

        private CellState cellState = CellState.None;

        public CellState GetCellState()
        {
            return cellState;
        }

        public void SetCellState(CellState state)
        {
            cellState = state;
            switch (state)
            {
                case CellState.None:
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                    break;
                case CellState.ToWalk:
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                    break;
                case CellState.Walked:
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                    dir_object.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                    break;
            }
        }

        #endregion

        #region 方向

        public Direction cellDir = Direction.None;

        public Direction GetCellDir()
        {
            return cellDir;
        }

        public void SetCellDir(Direction dir)
        {
            cellDir = dir;
            dir_object.gameObject.SetActive(true);
            switch(dir)
            {
                case Direction.Up:
                    dir_object.transform.localEulerAngles = new Vector3(0, 0, 90);
                    break;
                case Direction.Right:
                    dir_object.transform.localEulerAngles = Vector3.zero;
                    break;
                case Direction.Down:
                    dir_object.transform.localEulerAngles = new Vector3(0, 0, 270);
                    break;
            }
        }

        #endregion
    }

    public enum Direction
    {
        None = 0,
        Up = 1,
        Right = 2,
        Down = 3,
    }

    public enum CellState
    {
        None = 0,
        ToWalk = 1,
        Walked = 2,
    }
}

