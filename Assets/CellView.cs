using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellView : MonoBehaviour, IPointerClickHandler
{
    private UnityEvent onPointerClick = new UnityEvent();

    [SerializeField]
    private Color _basedColor;

    [SerializeField]
    private TextMeshProUGUI _text;

    private Cell _data;

    private Image _backgroundImage;

    public void Initialize(Cell cell, UnityAction callback)
    {
        _backgroundImage = GetComponent<Image>();
        _data = cell;
        UpdateView(cell.Value);
        _data.onValueChanged += UpdateView;
        onPointerClick.AddListener(callback);

        if (_data.IsActive)
        {
            _text.color = Color.gray;
            return;
        }
        _text.color = _basedColor;
    }


    private void UpdateView(int value)
    {
        _text.text = value == 0 ? String.Empty : value.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onPointerClick?.Invoke();
    }

    public void Activate()
    {
        _backgroundImage.color = Color.green;
    }

    public void Activate(Color color)
    {
        _backgroundImage.color = color;
        
    }

    public void Disactivate()
    {
        _backgroundImage.color = Color.white;
    }

    private void OnDestroy()
    {
        onPointerClick.RemoveAllListeners();
        _data.onValueChanged -= UpdateView;
    }
}
