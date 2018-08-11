﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductRequestManager : MonoBehaviour
{
    public static ProductRequestManager Instance;

    [SerializeField] private DisplayController _displayController;

    public float MaxRequestWaitTime = 10f;


    private ProductModel _requestingProduct;

    public int Strikes
    {
        get { return _strikes; }
    }

    public float TimeRemaining
    {
        get { return _timeRemaining; }
    }

    [SerializeField] [ReadOnly] private int _strikes = 0;
    [SerializeField] [ReadOnly] private float _timeRemaining = 0;

    private bool _shownInitalRequest = false;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    if (!_shownInitalRequest)
	    {
            RequestNewProduct();
	        _shownInitalRequest = true;
	    }

		UpdateTimeRemaining();
	}

    void RequestNewProduct()
    {
        // TODO: Modify this to use Kris's Randomizer once that's ready

        List<string> productNameList = ProductManager.Instance.GetProductNamesInScene();
        if (productNameList.Count > 0)
        {
            int rand = Random.Range(0, productNameList.Count);
            _requestingProduct = ProductManager.Instance.GetProductData(productNameList[rand]);
            _displayController.ShowProductOnScreen(_requestingProduct);
        }

        ResetTimeRemaining();
    }

    void UpdateTimeRemaining()
    {
        _timeRemaining -= Time.deltaTime;
        if (_timeRemaining <= 0)
        {
            _timeRemaining = 0f;

            // Out of time!
            Strike();
        }
    }

    void ResetTimeRemaining()
    {
        _timeRemaining = MaxRequestWaitTime;
    }

    public void OnProductReceived(Product product)
    {
        if (product.ProductData.ProductName.Equals(_requestingProduct.ProductName))
        {
            // Right Product
            RequestNewProduct();
        }
        else
        {
            // Wrong Product
            Strike();
        }
    }

    void Strike()
    {
        _strikes++;
        RequestNewProduct();
    }
}