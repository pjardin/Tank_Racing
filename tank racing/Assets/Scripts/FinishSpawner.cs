using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinishSpawner : MonoBehaviour
{
    GameObject[] test;
    // Start is called before the first frame update
    public void MoveIt()
    {
        test = GameObject.FindGameObjectsWithTag("Ground");
        Debug.Log(test);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
