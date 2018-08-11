using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public string ProductName = "";
    public float Weight = 1f;

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
        if (collision.gameObject.CompareTag("ProductDeliveryArea"))
        {
            // TODO: Update LED and do whateva
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Incinerator"))
        {
            Destroy(gameObject);
        }
    }
}