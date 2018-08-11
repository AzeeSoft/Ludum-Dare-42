using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public ProductModel ProductData;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        ProductManager.Instance.OnProductAdded(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDestroy()
    {
        ProductManager.Instance.OnProductRemoved(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ProductRequestPortal"))
        {
            ProductRequestManager.Instance.OnProductReceived(this);

            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Incinerator"))
        {
            Destroy(gameObject);
        }
    }
}