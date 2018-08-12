using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : MonoBehaviour {

    public bool IsBeingCarried
    {
        get { return _playerCarrying != null; }
    }

    private Rigidbody _rigidbody;

    private Player _playerCarrying = null;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float lerpValue = Time.deltaTime * 5;

        if (IsBeingCarried)
	    {
	        transform.position = Vector3.Lerp(transform.position, _playerCarrying.CarryObjectTransform.position, lerpValue);
	        transform.rotation = Quaternion.Lerp(transform.rotation, _playerCarrying.CarryObjectTransform.rotation, lerpValue);
	      //  transform.localScale = Vector3.Lerp(transform.localScale, _playerCarrying.CarryObjectTransform.localScale, lerpValue);
	    }
	    else
	    {
	      //  transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, lerpValue);
        }
	}

    public void OnCarried(Player player)
    {
        _playerCarrying = player;
        _rigidbody.isKinematic = true;
    }

    public void OnDropped()
    {
        _playerCarrying = null;
        _rigidbody.isKinematic = false;
    }

    public void OnThrown(Vector3 force)
    {
        OnDropped();
        _rigidbody.AddForce(force);
    }
}
