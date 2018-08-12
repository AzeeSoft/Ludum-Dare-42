using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSpawner : MonoBehaviour {

    public Transform spawnPoint;

    public float Speed;

    public int HowMuchStuff;

    public int TruckTimer;


    // Use this for initialization
    void Start () {
        StartCoroutine(Shipment());
    }
	
	// Update is called once per frame
	void Update () {
        
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
        for(int i = 0; i < HowMuchStuff; i++)
        {
            List<ProductModel> allProducts = ProductManager.Instance.GetCompleteProductList();
            int random = Random.Range(0, allProducts.Count);
            Instantiate(allProducts[random].ProductPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(Speed);
        }
        
    }
        
    
}