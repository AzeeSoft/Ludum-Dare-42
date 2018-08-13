using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHandler : MonoBehaviour {

    private int CurrentLives;


    public GameObject[] LifeBlocks;  

	// Use this for initialization
	void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
        CurrentLives = ProductRequestManager.Instance.Strikes;


        if(CurrentLives == 0)
        {
            //do nothing
        }

        else if (4 > CurrentLives && CurrentLives > 0)
        {
            Component Halo = LifeBlocks[CurrentLives-1].GetComponent("Halo");
            Halo.GetType().GetProperty("enabled").SetValue(Halo, true, null);
           
            //this can be changed later, using halo for an effect to test strikes.
        }

        else if(CurrentLives == 4)
        {
            Component Halo = LifeBlocks[CurrentLives - 1].GetComponent("Halo");
            Halo.GetType().GetProperty("enabled").SetValue(Halo, true, null);

            // put end game stuff here
        }

	}
}
