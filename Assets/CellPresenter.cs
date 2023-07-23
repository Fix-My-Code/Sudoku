using System;
using UnityEngine;

public class CellPresenter : MonoBehaviour
{
    [SerializeField]
    private CellView _cellView;

    public Cell Data => _data;

    [SerializeField]
    private Cell _data;

    public void Initialize(Cell cell)
    {
        _data = cell;
        _cellView.Initialize(cell, UpdateData);
    }

    public void UpdateData()
    {
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
