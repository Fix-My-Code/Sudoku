using Meta.Cell;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SudokuBoardView : MonoBehaviour
{
    [SerializeField]
    private ColorConfig _colorConfig;

    private List<CellPresenter> cellsview;

    public List<CellPresenter> FillGrid(Grid grid)
    {
        cellsview = GetComponentsInChildren<CellPresenter>().ToList();

        foreach(var cell in cellsview)
        {
            cell.Initialize(grid.BoardCells[cell.Data.x, cell.Data.y]);
            cell.CellView.InitializeBasedColor(_colorConfig.selectableText, _colorConfig.basedText);
        }

        return cellsview;
    }

    public void ActivateCell(CellPresenter cell)
    {
        cell.CellView.ColorCell(_colorConfig.activeBackground);
    }

    public void DeactivateCell(CellPresenter cell)
    {
        cell.CellView.ColorCell(_colorConfig.basedBackground);
    }


    public void ColorArea(List<CellPresenter> cells)
    {
        cells.ForEach(x => x.CellView.ColorCell(_colorConfig.areaColor));
    }

    public void ColorSameCells(List<CellPresenter> cells)
    {
        cells.ForEach(x => x.CellView.ColorCell(_colorConfig.sameBackground));
    }

    public void ClearBoard()
    {
        cellsview.ForEach(x =>
        {
            x.CellView.ColorCell(_colorConfig.basedBackground);
        });
    }

    public void ClearCells(List<CellPresenter> cells)
    {
        cells.ForEach(x =>
        {
            x.CellView.ColorCell(_colorConfig.basedBackground);
        });
    }
}
