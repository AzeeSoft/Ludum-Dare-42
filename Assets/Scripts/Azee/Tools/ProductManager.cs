using System.Collections;
using System.Collections.Generic;
using BasicTools.ButtonInspector;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public static ProductManager Instance;

    [SerializeField] private List<ProductModel> CompleteProductList = new List<ProductModel>();

    private readonly Dictionary<string, ProductModel> _productHash = new Dictionary<string, ProductModel>();

    private readonly Dictionary<string, int> _productCounts = new Dictionary<string, int>();

    [SerializeField] [ReadOnly] private float _totalWeight = 0f;


    [Button("Print Product Counts", "PrintProductCountList")]
    public bool PrintProductCounts;

    void Awake()
    {
        Instance = this;
        CalculateProductHash();
    }

    // Use this for initialization
    void Start()
    {
//		CalculateProductCounts();
//        PrintProductCountList();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void CalculateProductHash()
    {
        _productHash.Clear();
        foreach (ProductModel productModel in CompleteProductList)
        {
            if (!_productHash.ContainsKey(productModel.ProductName))
            {
                _productHash[productModel.ProductName] = productModel;
            }
            else
            {
                Debug.LogError("Duplicate Product Name Found: " + productModel.ProductName);
            }
        }
    }

    public ProductModel GetProductData(string productName)
    {
        if (_productHash.ContainsKey(productName))
        {
            return _productHash[productName];
        }

        return null;
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

    public float GetTotalProductWeight()
    {
        return _totalWeight;
    }

    public List<string> GetProductNamesInScene(bool includeTrash = false)
    {
        List<string> productNames = new List<string>();
        foreach (KeyValuePair<string, int> keyValuePair in _productCounts)
        {
            if ((!_productHash[keyValuePair.Key].IsTrash || includeTrash) && keyValuePair.Value > 0)
            {
                productNames.Add(keyValuePair.Key);
            }
        }

        return productNames;
    }

    public List<ProductModel> GetCompleteProductList(bool includeTrash = true)
    {
        List<ProductModel> productList = new List<ProductModel>();
        foreach (ProductModel productModel in CompleteProductList)
        {
            if (!productModel.IsTrash || includeTrash)
            {
                productList.Add(productModel);
            }
        }

        return productList;
    }

    public void OnProductAdded(Product product)
    {
        if (!_productCounts.ContainsKey(product.ProductData.ProductName))
        {
            _productCounts[product.ProductData.ProductName] = 0;
        }

        _productCounts[product.ProductData.ProductName]++;

        _totalWeight += product.ProductData.Weight;
    }

    public void OnProductRemoved(Product product)
    {
        if (_productCounts.ContainsKey(product.ProductData.ProductName))
        {
            _productCounts[product.ProductData.ProductName]--;
            _totalWeight -= product.ProductData.Weight;

            if (_totalWeight < 0)
            {
                _totalWeight = 0f;
            }
        }
    }
}