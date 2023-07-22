using System;

public class Cell
{
    public Action<int> onValueChanged;

    public int x, y;

    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;

            if (_value > 9)
            {
                _value = 1;
            }

            onValueChanged?.Invoke(_value);
        }
    }

    private int _value;

    public Cell()
    {
        this.x = 0;
        this.y = 0;

        this._value = 0;
    }

    public Cell(int x, int y, int value)
    {
        this.x = x;
        this.y = y;

        this._value = value;
    }

    public static Cell operator ++(Cell cell)
    {
        cell.Value++;
        return cell;
    }
}
