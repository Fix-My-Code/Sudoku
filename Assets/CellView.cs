using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI _text;
    private Cell _data;

    

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
