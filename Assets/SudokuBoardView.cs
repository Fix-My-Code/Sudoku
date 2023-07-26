using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SudokuBoardView : MonoBehaviour
{
    private List<CellPresenter> cellsview;

    public List<CellPresenter> FillGrid(Grid grid)
    {
        cellsview = GetComponentsInChildren<CellPresenter>().ToList();

        foreach(var cell in cellsview)
        {
            cell.Initialize(grid.BoardCells[cell.Data.x, cell.Data.y]);
        }

        return cellsview;
    }

    public void ClearBoard()
    {
        cellsview.ForEach(x =>
        {
            x.CellView.Disactivate();
        });
    }
}
