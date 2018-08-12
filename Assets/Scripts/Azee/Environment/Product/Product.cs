using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public ProductModel ProductData;

    private bool _destroyedAtPortal = false;

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

        if (_destroyedAtPortal)
        {
            ProductRequestManager.Instance.OnProductReceived(this);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ProductRequestPortal"))
        {
            _destroyedAtPortal = true;  // Doing this because calling OnProductReceived on ProductRequestManager from here leads to wrong results (There is a possibility that this object could be picked even if it is the last one)
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Incinerator"))
        {
            Destroy(gameObject);
        }
    }
}