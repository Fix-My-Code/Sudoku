using Meta.Cell;
using System.Collections.Generic;
using UnityEngine;

public class SudokuManager : MonoBehaviour
{
    [SerializeField]
    private SudokuWizard _sudokuWizard;
    [SerializeField]
    private SudokuBoardView _boardView;
    [SerializeField]
    private KeyboardManager _keyBoardManager;

    private CellPresenter _activeCell;
    private AreaValidator _areaValidator = new AreaValidator();

    public void Awake()
    {
        CellPresenter.onActiveCellChanged += ActiveCell;
        _keyBoardManager.onKeySelected += ValueChangeHandler;
    }

    public void ActiveCell(CellPresenter cell)
    {
        if (_activeCell != null)
        {
            _boardView.ClearBoard();
            _sameValueCells.Clear();
        }

        _activeCell = cell;
        _boardView.ActivateCell(_activeCell);
        var areaCells = _areaValidator.ValidateCell(_sudokuWizard.sudokuPresenter.CellsView, cell.Data);
        _boardView.ColorArea(areaCells);
        ValueChangeHandler(_activeCell.Data.Value);
    }

    public void ValueChangeHandler(int value)
    {
        _activeCell.Data.Value = value;
        if(value == 0)
        {
            return;
        }

        if(_sameValueCells != null)
        {
            _boardView.ClearCells(_sameValueCells);
            _boardView.ColorArea(_areaValidator.ValidateCell(_sudokuWizard.sudokuPresenter.CellsView, _activeCell.Data));
            _sameValueCells.Clear();
        }

        _sameValueCells = FindSameValue(value);
        _boardView.ColorSameCells(_sameValueCells);
    }
    private List<CellPresenter> _sameValueCells = new List<CellPresenter>();
    public List<CellPresenter> FindSameValue(int activeValue)
    {
        var sameValues = new List<CellPresenter>();
        var cells = _sudokuWizard.sudokuPresenter.CellsView;

        foreach (var cell in cells)
        {
            if(cell.Data.Value == activeValue && !cell.Equals(_activeCell))
            {
                sameValues.Add(cell);
            }
        }

        return sameValues;
    }

    public void OnDestroy()
    {
        CellPresenter.onActiveCellChanged -= ActiveCell;
    }
}
