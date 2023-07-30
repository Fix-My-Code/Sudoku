using Meta.Cell;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AreaValidator
{
    private List<CellPresenter> _areaCells;

    private const int BOX_SIZE = 3;

    public List<CellPresenter> ValidateCell(List<CellPresenter> cells, Cell cell)
    {
        _areaCells = new List<CellPresenter>();
        ValidateBox(cells, cell);
        ValidateCol(cells, cell);
        ValidateRow(cells, cell);

        return _areaCells;
    }

    private void ValidateCol(List<CellPresenter> cells, Cell cell)
    {
        var cellsView = cells.Where(x => x.Data.y == cell.y && x.Data.x != cell.x).ToList();
        _areaCells.AddRange(cellsView);
    }

    private void ValidateRow(List<CellPresenter> cells, Cell cell)
    {
        var cellsView = cells.Where(x => x.Data.x == cell.x && x.Data.y != cell.y).ToList();
        _areaCells.AddRange(cellsView);
    }

    private void ValidateBox(List<CellPresenter> cells, Cell cell)
    {
        int xBox = (cell.x / BOX_SIZE) * BOX_SIZE;
        int yBox = (cell.y / BOX_SIZE) * BOX_SIZE;

        var cellsView = cells.Where(x => x.Data.x >= xBox && x.Data.x < xBox + BOX_SIZE &&
                                         x.Data.y >= yBox && x.Data.y < yBox + BOX_SIZE &&
                                         x.Data.y != cell.y && x.Data.x != cell.x).ToList();
        _areaCells.AddRange(cellsView);
    }
}
