using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinishLineEvent : MonoBehaviour
{
    public GameObject tank;
    private AudioSource fanFare;
    
    private AudioSource[] aSource; //array of all AudioSource Components
    
    public UnityEngine.Events.UnityEvent FinishLine;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponents<AudioSource>();

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject==tank || collision.gameObject.CompareTag("Tank"))
        {
            Debug.Log("tank Entered");
            
            int which = Random.Range(0, aSource.Length - 1);

            aSource[which].Play();


            StartCoroutine(waitForFan(which)); // waits until fanfare is done
            
        }
    }
    
    IEnumerator waitForFan(int which)
    {
        yield return new WaitForSeconds(aSource[which].clip.length);
        FinishLine.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
