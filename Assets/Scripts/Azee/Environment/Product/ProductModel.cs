using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "My New Product", menuName = "Data Models/Product", order = 1)]
[Serializable]
public class ProductModel : ScriptableObject
{
    public string ProductName = "My New Product";
    public Sprite ProductSprite = null;
    public float Weight = 1f;
}