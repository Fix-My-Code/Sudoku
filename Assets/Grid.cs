using System;
using Meta.Cell;
using Newtonsoft.Json;

[Serializable]
public class Grid
{
    [JsonProperty]
    public Cell[,] BoardCells
    {
        get => _cells;
        set => _cells = value;
    }

    private Cell[,] _cells;

    public Grid()
    {

    }

    public Grid(Cell[,] cells)
    {
        _cells = cells;
    }

    public Grid FillGrid(Cell[,] cells)
    {
        return new Grid(cells);
    }
}