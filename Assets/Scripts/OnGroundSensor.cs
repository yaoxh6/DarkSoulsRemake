﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider capcol;

    private Vector3 point1;
    private Vector3 point2;
    private float radius;

    void Awake()
    {
        radius = capcol.radius;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        point1 = transform.position + transform.up * radius;
        point2 = transform.position + transform.up * capcol.height - transform.up * radius;
        if (Physics.OverlapCapsule(point1, point2, radius, LayerMask.GetMask("Ground")).Length != 0)
        {
            Debug.Log("OnGround");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
