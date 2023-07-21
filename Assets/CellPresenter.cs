using System;
using UnityEngine;
using UnityEngine.Events;

public class CellPresenter : MonoBehaviour
{
    [SerializeField]
    private CellView _cellView;

    public Cell Cell => _data;

    private Cell _data;

    public void Initialize(Cell cell)
    {
        _data = cell;
        _cellView.Initialize(cell, UpdateData);
    }

    public void UpdateData()
    {
        _data++;
    }

    private void OnDestroy()
    {
    }
}
