using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour
{
    public Image ProductImage;
    public TextMeshProUGUI ProductNameLabel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowProductOnScreen(string productName)
    {
        ProductModel productData = ProductManager.Instance.GetProductData(productName);
        ShowProductOnScreen(productData);
    }

    public void ShowProductOnScreen(ProductModel productData)
    {
        if (productData != null)
        {
            ProductImage.sprite = productData.ProductSprite;

            if (ProductNameLabel != null)
            {
                ProductNameLabel.text = productData.ProductName;
            }
        }
        else
        {
            Debug.LogError("Product Data is null");
        }
    }
}
