using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testJoy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("OK");
        Debug.Log(Input.GetAxis("axis3"));
        //Debug.Log(Input.GetButton("btn7"));
    }
}
