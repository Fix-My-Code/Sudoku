using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Meta.Cell
{
    public class CellView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        public List<PenCell> PenCells = new List<PenCell>();

        private UnityEvent onPointerClick = new UnityEvent();

        private Color _basedCellText = new Color();
        private Color _selectableCellText = new Color(); 

        private Image _backgroundImage;

        private CellPresenter _presenter;
        private Cell _data => _presenter.Data;


        public void Initialize(CellPresenter cellPresenter, UnityAction callback)
        {
            _presenter = cellPresenter;
            SetData();

            onPointerClick.AddListener(callback);
            _backgroundImage = GetComponent<Image>();
        }

        public void InitializeBasedColor(Color bg, Color text)
        {
            _basedCellText = text;
            _selectableCellText = bg;

            _text.color = _data.IsActive ? _selectableCellText : _basedCellText;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClick?.Invoke();
        }

        public void ColorCell(Color color)
        {
            _backgroundImage.color = color;
        }

        private void SetData()
        {
            UpdateView(_data.Value);
            _data.onValueChanged += UpdateView;
        }

        private void UpdateView(int value)
        {
            _text.text = value == 0 ? String.Empty : value.ToString();
        }

        private void OnDestroy()
        {
            onPointerClick.RemoveAllListeners();
            _data.onValueChanged -= UpdateView;
        }
    }
}
