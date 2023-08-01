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
    private bool _penEnabled = true;


    public void ValueChangeHandler(int value)
    {
        if (Globals.isPenEnabled && value != 0)
        {
            _activeCell.CellView.PenCells.Where(x => x.Value == value).FirstOrDefault().Activate(true);
            return;
        } else if(Globals.isPenEnabled)
        {
            _activeCell.CellView.PenCells.Where(x => x.Value == value).FirstOrDefault().Activate(false);
        }

        _activeCell.Data.Value = value;

        if(_sameValueCells != null)
        {
            _boardView.ClearCells(_sameValueCells);
            _boardView.ColorArea(_areaValidator.ValidateCell(_sudokuWizard.sudokuPresenter.CellsView, _activeCell.Data));
            _sameValueCells.Clear();
        }

        if (value == 0)
        {
            return;
        }

        _sameValueCells = FindSameValue(value, _sudokuWizard.sudokuPresenter.CellsView);
        _boardView.ColorSameCells(_sameValueCells);
        _boardView.ColorError(FindSameValue(value, _areaCells));
    }

    public List<CellPresenter> FindSameValue(int activeValue, List<CellPresenter> cells)
    {
        var sameValues = new List<CellPresenter>();

        foreach (var cell in cells)
        {
            if(cell.Data.Value == activeValue && !cell.Equals(_activeCell))
            {
                sameValues.Add(cell);
            }
        }

        return sameValues;
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
