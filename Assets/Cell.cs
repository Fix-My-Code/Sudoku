using System;
using Newtonsoft.Json;

[Serializable]
public class Cell
{
    [JsonIgnore]
    public Action<int> onValueChanged;

    public int x, y;

    private int _value;
    private bool _isActive = true;

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
                _value = 0;
            }

            onValueChanged?.Invoke(_value);
        }
    }

    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }

    public Cell()
    {
        x = 0;
        y = 0;
        _value = 0;
    }

    public Cell(int x, int y, int value, bool isActive = true)
    {
        this.x = x;
        this.y = y;
        _isActive = isActive;
        _value = value;
    }

    public static Cell operator ++(Cell cell)
    {
        cell.Value++;
        return cell;
    }
}