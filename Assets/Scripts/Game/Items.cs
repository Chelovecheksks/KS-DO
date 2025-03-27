using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected ItemData _itemData;
    [SerializeField] protected GameObject _objectInHand;

    public abstract void Use();
}

public abstract class Gun : Item
{
    [SerializeField] protected Camera _camera;
}
