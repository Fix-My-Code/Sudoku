using Meta.Cell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public Action<int> onKeySelected;
    List<Key<int>> keys = new List<Key<int>>();


    public void Awake()
    {
        keys.AddRange(GetComponentsInChildren<Key<int>>().ToList());
        Initialize();
    }

    private void Initialize()
    {
        keys.ForEach(x => x.onKeyClick += KeyClickHandler);
    }

    private void KeyClickHandler(int value)
    {
        onKeySelected?.Invoke(value);
    }

    private void OnDestroy()
    {
        keys.ForEach(x => x.onKeyClick -= KeyClickHandler);
    }
}
