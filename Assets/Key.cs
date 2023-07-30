using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Key<T> : MonoBehaviour, IPointerClickHandler
{
    public Action<T> onKeyClick;

    [SerializeField]
    private T _value;

    public T Value => _value;

    public void OnPointerClick(PointerEventData eventData)
    {
        onKeyClick?.Invoke(Value);
    }
}

