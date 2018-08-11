using System.Collections;
using System.Collections.Generic;
using BasicTools.ButtonInspector;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public static ProductManager Instance;

    private Dictionary<string, int> _productCounts = new Dictionary<string, int>();

    [Button("Print Product Counts", "PrintProductCountList")] public bool PrintProductCounts;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
//		CalculateProductCounts();
//        PrintProductCountList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PrintProductCountList()
    {
        Debug.Log("Product Counts");
        Debug.Log("==============");
        foreach (KeyValuePair<string, int> keyValuePair in _productCounts)
        {
            Debug.Log(keyValuePair.Key + ": " + keyValuePair.Value);
        }
    }

    public void CalculateProductCounts()
    {
        _productCounts.Clear();

        foreach (Product product in FindObjectsOfType<Product>())
        {
            OnProductAdded(product);
        }
    }

    public int GetProductCount(string productName)
    {
        if (_productCounts.ContainsKey(productName))
        {
            return _productCounts[productName];
        }

        return 0;
    }

    public void OnProductAdded(Product product)
    {
        if (!_productCounts.ContainsKey(product.ProductName))
        {
            _productCounts[product.ProductName] = 0;
        }

        _productCounts[product.ProductName]++;
    }

    public void OnProductRemoved(Product product)
    {
        if (_productCounts.ContainsKey(product.ProductName))
        {
            _productCounts[product.ProductName]--;
        }
    }
}
