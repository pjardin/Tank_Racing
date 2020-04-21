using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinishLineEvent : MonoBehaviour
{
    public GameObject tank;
    private AudioSource fanFare;
    public UnityEngine.Events.UnityEvent FinishLine;

    // Start is called before the first frame update
    void Start()
    {
        fanFare = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject==tank)
        {
            
            Debug.Log("tank Entered");
            fanFare.Play();
            StartCoroutine(waitForFan()); // waits until fanfare is done
            
        }
    }
    
    IEnumerator waitForFan()
    {
        yield return new WaitForSeconds(fanFare.clip.length);
        FinishLine.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
