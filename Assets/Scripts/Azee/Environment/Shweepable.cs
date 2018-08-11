using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shweepable : MonoBehaviour
{
    private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shweep(Vector3 force, Vector3 positionOfContact)
    {
        rigidbody.AddForceAtPosition(force, positionOfContact);
    }
}
