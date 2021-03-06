﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    [Serializable]
    public class ShweepDataModel
    {
        public bool EnableGizmo = true;
        public Color GizmoColor = Color.cyan;
        public float Radius = 5f;
        public float MaxDistance = 5f;
        public float MaxStrength = 500f;
    }

    public ShweepDataModel ShweepData;

    public float ThrowStrength = 5f;

    public bool IsCarrying
    {
        get { return _carryingObject != null; }
    }

    private string _carryButton = "Carry";
    private string _dropButton = "Carry";
    private string _throwButton = "Fire1";
    private string _shweepButton = "Fire2";

    private Player _player;
    private Camera _camera;

    private Carryable _carryingObject = null;

    void Awake()
    {
        _player = GetComponent<Player>();
        _camera = GetComponentInChildren<Camera>();
    }

    // Use this for initialization
    void Start()
    {
    }

    void OnDrawGizmos()
    {
        if (ShweepData.EnableGizmo)
        {
            DrawShweepGizmo();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCarrying)
        {
            if (Input.GetButtonDown(_dropButton))
            {
                Drop(_carryingObject);
            }
            else if (Input.GetButtonDown(_throwButton))
            {
                Throw(_carryingObject);
            }
        }
        else
        {
            if (Input.GetButtonDown(_carryButton))
            {
                CarryPointingObject();
            }
        }

        if (Input.GetButtonDown(_shweepButton))
        {
            PerformShweep();
        }
    }

    void CarryPointingObject()
    {
        RaycastHit raycastHit = new RaycastHit();
        bool didHit = Physics.Raycast(_player.RaycastOriginTransform.position, _player.RaycastOriginTransform.forward, out raycastHit);

        if (didHit)
        {
            Carryable carryable = raycastHit.transform.gameObject.GetComponent<Carryable>();
            if (carryable != null)
            {
                Carry(carryable);
            }
        }
    }

    void Carry(Carryable carryable)
    {
//        Debug.Log("Carrying.. " + carryable.gameObject);
        if (IsCarrying)
        {
            Drop(carryable);
        }

        _carryingObject = carryable;
        carryable.OnCarried(_player);
    }

    void Drop(Carryable carryable)
    {
        if (IsCarrying)
        {
            carryable.OnDropped();
            _carryingObject = null;
        }
    }

    void Throw(Carryable carryable)
    {
        if (IsCarrying)
        {
            carryable.OnThrown(_camera.transform.forward * ThrowStrength);
            _carryingObject = null;
        }
    }

    void DrawShweepGizmo()
    {
        Vector3 start = transform.position;
        Vector3 end = start + (transform.forward * ShweepData.MaxDistance);

        DebugExtension.DebugCapsule(start, end, ShweepData.GizmoColor, ShweepData.Radius);
    }

    void PerformShweep()    
    {
        //Tomas change
        GetComponentInChildren<ShweepEffects>().OnShweep();

        int layerMask = -5; // All Layers

        Vector3 shweepOrigin = transform.position;

        RaycastHit[] raycastHits = Physics.SphereCastAll(shweepOrigin, ShweepData.Radius, _player.RaycastOriginTransform.forward,
            ShweepData.MaxDistance, layerMask, QueryTriggerInteraction.Ignore);

        foreach (RaycastHit raycastHit in raycastHits)
        {
            Shweepable shweepable = raycastHit.transform.gameObject.GetComponent<Shweepable>();

            if (shweepable != null)
            {
                Vector3 playerToObject = (raycastHit.transform.position - shweepOrigin);

                float strength = ShweepData.MaxStrength - AZTools.Map(playerToObject.magnitude, 0,
                                     ShweepData.MaxDistance, 0, ShweepData.MaxStrength);

                Vector3 force = playerToObject.normalized * strength;

                shweepable.Shweep(force);
            }
        }
    }
}