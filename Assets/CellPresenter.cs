using System;
using UnityEngine;

namespace Meta.Cell
{
    public class CellPresenter : MonoBehaviour
    {
        public static CellPresenter ActiveCell;
        public static Action<CellPresenter> onActiveCellChanged;
        public static Action<CellPresenter> onActiveCellValueChanged;

        [SerializeField]
        private CellView _cellView;

        public Cell Data => _data;
        public CellView CellView => _cellView;

        [SerializeField]
        private Cell _data;

        public void Initialize(Cell cell)
        {
            _data = cell;
            _cellView.Initialize(this, SelectCell);
        }

        public void SelectCell()
        {
            ActiveCell = this;
            onActiveCellChanged?.Invoke(this);
        }
    }
}
