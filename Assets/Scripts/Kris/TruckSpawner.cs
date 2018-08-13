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


    private MainLevelManager _mainLevelManager;




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
        _mainLevelManager = FindObjectOfType<MainLevelManager>();
    }

    void FixedUpdate()
    {
        
        if (_mainLevelManager.CurrentDifficulty == 2)
        {
            HowMuchStuff = 15;
            TruckTimer = 20;
        }

        else if (_mainLevelManager.CurrentDifficulty == 4)
        {
            HowMuchStuff = 20;
            TruckTimer = 20;
        }

        else if (_mainLevelManager.CurrentDifficulty == 6)
        {
            HowMuchStuff = 25;
            TruckTimer = 15;
        }

        else if (_mainLevelManager.CurrentDifficulty == 8)
        {
            HowMuchStuff = 25;
            TruckTimer = 10;

        }

        else if (_mainLevelManager.CurrentDifficulty == 10)
        {
            HowMuchStuff = 30;
            TruckTimer = 10;
        }

        else
        {
            //nothing
        }

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
            yield return new WaitForSeconds(Speed);
        }

        for (int i = 0; i < itemsToSpawn; i++)
        {
            List<ProductModel> allProducts = ProductManager.Instance.GetCompleteProductList();
            int random = Random.Range(0, allProducts.Count);
            Instantiate(allProducts[random].ProductPrefab, spawnPoint.position, spawnPoint.rotation);
            allProducts[random].ProductPrefab.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * 100);
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