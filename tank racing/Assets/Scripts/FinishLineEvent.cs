using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinishLineEvent : MonoBehaviour
{
    public GameObject tank;
    // Start is called before the first frame update
    public UnityEngine.Events.UnityEvent FinishLine;
    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject==tank)
        {
            Debug.Log("tank Entered");
            FinishLine.Invoke();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
