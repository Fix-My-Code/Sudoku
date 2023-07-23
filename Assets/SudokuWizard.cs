using UnityEngine;
using System.Linq;
using System.Diagnostics;

public class SudokuWizard : MonoBehaviour
{
    [SerializeField]
    private SudokuBoardView _sudokuBoardView;

    [SerializeField]
    public Grid Grid;

    public SudokuPresenter sudokuPresenter;

    public void Awake()
    {
        sudokuPresenter = new SudokuPresenter();
    }

    [ContextMenu("Fill")]
    public void FillBase()
    {
        sudokuPresenter = new SudokuPresenter();
        _sudokuBoardView.FillGrid(sudokuPresenter.BoardGrid);
    }

    [ContextMenu("Validate")]
    public void ValidateSudoku()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        stopwatch.Start();
        foreach (var cell in sudokuPresenter.BoardGrid.BoardCells)
        {
            if(!SudokuGenerator.ValidateCell(sudokuPresenter.BoardGrid.BoardCells, cell))
            {
                return;
            }
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log("Решено" + stopwatch.ElapsedMilliseconds);
    }

    [ContextMenu("ImportAndFill")]
    public void Import()
    {
        var a = Serializer.Deserialize<Grid>("SudokuPreset.txt");
        
        sudokuPresenter.BoardGrid = a;
        _sudokuBoardView.FillGrid(a);
    }

    [ContextMenu("Export")]
    public void Export()
    {
        Serializer.Serialize(sudokuPresenter.BoardGrid, "SudokuPreset.txt");
    }
}

public class SudokuPresenter
{
    public Grid BoardGrid
    {
        get => _boardGrid;
        set => _boardGrid = value;
    }

    private Grid _boardGrid;

    public SudokuPresenter()
    {
        var generator = new SudokuGenerator();
        var grid = generator.DrawGrid();
        _boardGrid = new Grid(grid);
    }
}