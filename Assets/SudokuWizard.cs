using UnityEngine;

public class SudokuWizard : MonoBehaviour
{
    [SerializeField]
    private SudokuBoardView _sudokuBoardView;

    [SerializeField]
    public Grid Grid;

    public SudokuPresenter sudokuPresenter;

    [ContextMenu("Fill")]
    public void FillBase()
    {
        sudokuPresenter = new SudokuPresenter();
        _sudokuBoardView.FillGrid(sudokuPresenter.BoardGrid);
    }

    [ContextMenu("ImportAndFill")]
    public void Import()
    {
        var a = Serializer.Deserialize<Grid>("SudokuPreset.txt");
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
    public Grid BoardGrid => _boardGrid;
    private Grid _boardGrid;

    public SudokuPresenter()
    {
        var generator = new SudokuGenerator();
        var grid = generator.DrawGrid();
        _boardGrid = new Grid(grid);
    }
}