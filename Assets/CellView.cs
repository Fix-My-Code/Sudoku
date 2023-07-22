using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CellView : MonoBehaviour, IPointerClickHandler
{
    private UnityEvent onPointerClick = new UnityEvent();

    [SerializeField]
    private Color _lockedColor;

    [SerializeField]
    private TextMeshProUGUI _text;
    
    public void Initialize(Cell cell, UnityAction callback)
    {
        UpdateView(cell.Value);
        cell.onValueChanged += UpdateView;
        onPointerClick.AddListener(callback);
    }

    private void UpdateView(int value)
    {
        _text.text = value.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onPointerClick?.Invoke();
    }

    private void OnDestroy()
    {
        onPointerClick.RemoveAllListeners();
    }
}
