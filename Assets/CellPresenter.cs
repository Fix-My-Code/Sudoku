using System;
using UnityEngine;

public class CellPresenter : MonoBehaviour
{
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
        _cellView.Initialize(cell, UpdateData);
    }

    public void UpdateData()
    {
        onActiveCellChanged?.Invoke(this);
        onActiveCellValueChanged?.Invoke(this);

        if (!Data.IsActive)
        {
            return;
        }

        _data++;
    }

    private void OnDestroy()
    {
    }
}
