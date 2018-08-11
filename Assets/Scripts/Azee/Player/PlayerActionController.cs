using System;
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

    private string _pickupButton = "Fire1";
    private string _shweepButton = "Fire2";

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
        if (Input.GetButtonDown(_shweepButton))
        {
            PerformShweep();
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
        int layerMask = -5; // All Layers

        Vector3 shweepOrigin = transform.position;

        RaycastHit[] raycastHits = Physics.SphereCastAll(shweepOrigin, ShweepData.Radius, transform.forward,
            ShweepData.MaxDistance, layerMask, QueryTriggerInteraction.Ignore);

        foreach (RaycastHit raycastHit in raycastHits)
        {
            Shweepable shweepable = raycastHit.transform.gameObject.GetComponent<Shweepable>();

            if (shweepable != null)
            {
                Vector3 pointOfContact = raycastHit.point;
                Vector3 playerToObject = (raycastHit.transform.position - shweepOrigin);

                float strength = ShweepData.MaxStrength - AZTools.Map(playerToObject.magnitude, 0, ShweepData.MaxDistance, 0, ShweepData.MaxStrength);

                Vector3 force = playerToObject.normalized * strength;

                shweepable.Shweep(force, pointOfContact);
            }
        }
    }
}