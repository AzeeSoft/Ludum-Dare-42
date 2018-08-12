using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSpawner : MonoBehaviour {

    public Transform spawnPoint;

    public float Speed;

    public int HowMuchStuff;

    public int TruckTimer;

    public bool TruckEmpty = false;

    public bool TruckDrive = false;

    void TruckSimulator()
    {
        TruckEmpty = true;
        TruckDrive = true;
        Debug.Log("We're in the thing");
        // need animation stuff here
    }

    void StopSimulation()
    {
        TruckEmpty = false;
        //if you need an exit animation put here
    }


    // Use this for initialization
    void Start () {
        StartCoroutine(Shipment());
    }
	

    IEnumerator Shipment()
    {
        while(true)
        {
            StartCoroutine(spawnMoreStuff());
            yield return new WaitForSeconds(TruckTimer);
        }
        
    }

    IEnumerator spawnMoreStuff()
    {
        TruckSimulator();
        for (int i = 0; i < HowMuchStuff; i++)
        {
            List<ProductModel> allProducts = ProductManager.Instance.GetCompleteProductList();
            int random = Random.Range(0, allProducts.Count);
            Instantiate(allProducts[random].ProductPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(Speed);
        }
        StopSimulation();
        
    }
    
    
}