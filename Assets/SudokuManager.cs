using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SudokuManager : MonoBehaviour
{
    [SerializeField]
    private SudokuWizard _sudokuWizard;
    [SerializeField]
    private SudokuBoardView _boardView;

    [SerializeField]
    private Color _sameValueColor;

    [SerializeField]
    private Color _gridColor;

    private CellPresenter _activeCell;

    private List<CellPresenter> _sameCellValue = new List<CellPresenter>();

    public void Awake()
    {
        CellPresenter.onActiveCellChanged += ChooseCell;
        CellPresenter.onActiveCellValueChanged += ColorCellWithSameValue;
    }

    public void ChooseCell(CellPresenter cell)
    {
        if (_activeCell != null)
        {
            _activeCell.CellView.Disactivate();
        }
        _activeCell = cell;
        _activeCell.CellView.Activate();
    }

    public void ColorCellWithSameValue(CellPresenter cell)
    {
        _boardView.ClearBoard();
        ValidateCell(_sudokuWizard.sudokuPresenter.cellsView, cell.Data);
        _sameCellValue.ForEach(x => x.CellView.Activate(_gridColor));
    }

    private const int BOX_SIZE = 3;
    private const int GRID_SIZE = 9;

    public void ValidateCell(List<CellPresenter> cells, Cell cell)
    {
        _sameCellValue = new List<CellPresenter>();
        ValidateBox(cells, cell);
        ValidateCol(cells, cell);
        ValidateRow(cells, cell);
    }

    private void ValidateCol(List<CellPresenter> cells, Cell cell)
    {
        var cellsView = cells.Where(x => x.Data.y == cell.y).ToList();
        _sameCellValue.AddRange(cellsView);
    }

    private void ValidateRow(List<CellPresenter> cells, Cell cell)
    {
        var cellsView = cells.Where(x => x.Data.x == cell.x).ToList();
        _sameCellValue.AddRange(cellsView);
    }

    private void ValidateBox(List<CellPresenter> cells, Cell cell)
    {
        int xBox = (cell.x / BOX_SIZE) * BOX_SIZE;
        int yBox = (cell.y / BOX_SIZE) * BOX_SIZE;

        var cellsView = cells.Where(x => x.Data.x >= xBox && x.Data.x < xBox + BOX_SIZE && 
                                         x.Data.y >= yBox && x.Data.y < yBox + BOX_SIZE).ToList();
        _sameCellValue.AddRange(cellsView);
    }
}
