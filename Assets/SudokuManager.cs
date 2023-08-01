using Meta.Cell;
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
    private KeyboardManager _keyBoardManager;

    private CellPresenter _activeCell;
    private AreaValidator _areaValidator = new AreaValidator();

    private List<CellPresenter> _sameValueCells = new List<CellPresenter>();
    private List<CellPresenter> _areaCells = new List<CellPresenter>();

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
        _areaCells = _areaValidator.ValidateCell(_sudokuWizard.sudokuPresenter.CellsView, cell.Data);
        _boardView.ColorArea(_areaCells);
        ValueChangeHandler(_activeCell.Data.Value);
    }

    public void ValueChangeHandler(int value)
    {
        if (!_activeCell.Data.IsActive)
        {
            return;
        }

        if (value == 0)
        {
            DeactivatePenCells();
            return;
        }

        if (Globals.isPenEnabled && _activeCell.Data.Value == 0)
        {
            ActivatePenCellWithValue(value);
            return;
        }

        _activeCell.Data.Value = value;

        if (_sameValueCells != null)
        {
            _boardView.ClearCells(_sameValueCells);
            _sameValueCells.Clear();
        }

        if (value != 0)
        {
            DeactivatePenCells();
            var areaCells = _areaValidator.ValidateCell(_sudokuWizard.sudokuPresenter.CellsView, _activeCell.Data);
            _boardView.ColorArea(areaCells);
            _sameValueCells = FindSameValue(value, _sudokuWizard.sudokuPresenter.CellsView);
            _boardView.ColorSameCells(_sameValueCells);
            _boardView.ColorError(FindSameValue(value, _areaCells));
        }
    }

    private void DeactivatePenCells()
    {
        _activeCell.CellView.PenCells.ForEach(x => x.Activate(false));
    }

    private void ActivatePenCellWithValue(int value)
    {
        _activeCell.CellView.PenCells.Where(x => x.Value == value).FirstOrDefault()?.Activate();
    }

    private List<CellPresenter> FindSameValue(int value, List<CellPresenter> cells)
    {
        return cells.Where(x => x.Data.Value == value).ToList();
    }

    public void PenActive()
    {
        Globals.isPenEnabled = Globals.isPenEnabled ? false : true;
    }

    public void OnDestroy()
    {
        CellPresenter.onActiveCellChanged -= ActiveCell;
    }
}
