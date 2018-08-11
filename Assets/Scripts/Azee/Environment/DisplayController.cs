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
		ShowProductOnScreen("Milk");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowProductOnScreen(string productName)
    {
//        ProductImage.sprite = productSprite;
        ProductNameLabel.text = productName;
    }
}
