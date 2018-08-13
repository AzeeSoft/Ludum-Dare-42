using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSpawner : MonoBehaviour
{
    public Animator truck, bed;

    public Transform spawnPoint;

    public float Speed;

    public int HowMuchStuff;

    public int TruckTimer;

    public bool TruckEmpty = false;

    public bool TruckDrive = false;

    void TruckDrivingBack()
    {
        truck.SetTrigger("DriveBack");
        TruckEmpty = true;
        TruckDrive = true;
        Debug.Log("We're in the thing");
        // need animation stuff here
    }

    void TruckDrivingAway()
    {
        TruckEmpty = false;
        truck.SetTrigger("DriveAway");
        //if you need an exit animation put here
    }

    void TruckContainerRaise()
    {
        bed.SetBool("Raising", true);
        bed.SetBool("Lowering", false);
    }


    void TruckContainerLower()
    {
        bed.SetBool("Raising", false);
        bed.SetBool("Lowering", true);
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Shipment());
    }


    IEnumerator Shipment()
    {
        while (true)
        {
            StartCoroutine(spawnMoreStuff());
            yield return new WaitForSeconds(TruckTimer);
        }
    }

    IEnumerator spawnMoreStuff()
    {
        TruckDrivingBack();
        yield return
            new WaitForSeconds(2f); //edit this if you need to increase or decease time before container animation
        TruckContainerRaise();

        ProductModel currentlyRequestingProduct = ProductRequestManager.Instance.RequestingModel;

        int itemsToSpawn = HowMuchStuff;
        if (currentlyRequestingProduct != null)
        {
            Instantiate(currentlyRequestingProduct.ProductPrefab, spawnPoint.position,
                spawnPoint.rotation);

            itemsToSpawn--;
        }

        for (int i = 0; i < itemsToSpawn; i++)
        {
            List<ProductModel> allProducts = ProductManager.Instance.GetCompleteProductList();
            int random = Random.Range(0, allProducts.Count);
            Instantiate(allProducts[random].ProductPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(Speed);

            if (i >= HowMuchStuff)
            {
                TruckContainerLower();
            }
        }

        TruckContainerLower();
        TruckDrivingAway();
    }
}