public class Grid
{
    public Cell[,] BoardCells => _cells;

    private Cell[,] _cells;

    public Grid(Cell[,] cells)
    {
        _cells = cells;
    }

    public Cell[,] FillGrid()
    {
        _cells = new Cell[9, 9];

        for (var row = 0; row < 9; row++)
        {
            for (var col = 0; col < 9; col++)
            {
                var value = UnityEngine.Random.Range(1, 10);
                _cells[row, col] = new Cell(row, col, value);
            }
        }

        return BoardCells;
    }

    public Grid FillGrid(Cell[,] cells)
    {
        return new Grid(cells);
    }
}
