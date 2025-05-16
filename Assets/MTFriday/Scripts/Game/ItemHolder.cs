using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    private List<Item> _items = new List<Item>();

    public Item SelectItem(int index)
    {
        foreach (Item item in _items)
            item.gameObject.SetActive(false);

        if (index < 0 || index >= _items.Count)
            index = 0;

        _items[index].gameObject.SetActive(true);
        return _items[index];
    }

    private void Awake()
    {
        _items.AddRange(GetComponentsInChildren<Item>());
    }

    void Update()
    {
        
    }
}
