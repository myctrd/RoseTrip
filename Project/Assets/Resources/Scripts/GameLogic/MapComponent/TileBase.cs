using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Map
{
    public class TileBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 originPosition;

        private Image backImage;

        private int maxLine = 8;
        private int maxCol = 4;
        public int GetMaxLine()
        {
            return maxLine;
        }

        public int GetMaxCol()
        {
            return maxCol;
        }

        private TileCell[,] cells;

        public int startLine, startCol;

        public TileCell GetCell(int line, int col)
        {
            return cells[line, col];
        }

        private TileCell startCell = null;

        public TileCell GetStartCell()
        {
            return startCell;
        }

        public void SetStartCell(int line, int col)
        {
            startLine = line;
            startCol = col;
            startCell = cells[line, col];
        }

        private void Awake()
        {
            cells = new TileCell[maxLine, maxCol];
            originPosition = transform.localPosition;
            backImage = transform.Find("Image").GetComponent<Image>();
            Transform grid = transform.Find("Grid");
            for (int i = 0; i < maxLine; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    cells[i, j] = grid.Find(i + "_" + j).GetComponent<TileCell>();
                }
            }
        }

        #region 操作

        public void SetImageAlpha(float a)
        {
            backImage.color = new Color(backImage.color.r, backImage.color.g, backImage.color.b, a);
        }

        public Vector3 GetOriginPosition()
        {
            return originPosition;
        }

        public void RevertPosition()
        {
            transform.localPosition = originPosition;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (TileManager.m_instance.roleMove)
                return;
            TileManager.m_instance.SetTargetTile(this);
            SetImageAlpha(0.8f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (TileManager.m_instance.roleMove)
                return;
            TileManager.m_instance.SetTargetTile(null);
            SetImageAlpha(1);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (TileManager.m_instance.roleMove)
                return;
            TileManager.m_instance.SetDraggedTile(this);
            originPosition = transform.localPosition;
            SetDraggedPosition(eventData, gameObject);
            transform.SetAsLastSibling();
            backImage.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (TileManager.m_instance.roleMove)
                return;
            SetDraggedPosition(eventData, gameObject);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (TileManager.m_instance.roleMove)
                return;
            TileManager.m_instance.SwitchTile();
            backImage.raycastTarget = true;
        }

        private void SetDraggedPosition(PointerEventData eventData, GameObject g)
        {
            var rt = g.GetComponent<RectTransform>();

            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
            {
                rt.position = globalMousePos;
            }
        }

        #endregion
    }
}


