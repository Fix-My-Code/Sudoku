using UnityEngine;

public class SudokuBoardView : MonoBehaviour
{
    public void FillGrid(Grid grid)
    {
        var boardCells = GetComponentsInChildren<CellPresenter>();

        foreach(var cell in boardCells)
        {
            cell.Initialize(grid.BoardCells[cell.Data.x, cell.Data.y]);
        }
    }
}
