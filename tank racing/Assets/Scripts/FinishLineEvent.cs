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

            GTimer.thisTime[0] = GTimer.minutes;
            GTimer.thisTime[1] = GTimer.seconds;

            switch (GTimer.lvl)
            {
                case GTimer.Level.O:
                    GTimer.curBest = GTimer.getBest(1);
                    //Debug.Log("1");
                    break;

                case GTimer.Level.Tw:
                    GTimer.curBest = GTimer.getBest(2);
                    //Debug.Log("2");
                    break;

                case GTimer.Level.Th:
                    GTimer.curBest = GTimer.getBest(3);
                    //Debug.Log("3");
                    break;

                case GTimer.Level.Fo:
                    GTimer.curBest = GTimer.getBest(4);
                    //Debug.Log("4");
                    break;

                case GTimer.Level.Fi:
                    GTimer.curBest = GTimer.getBest(5);
                   // Debug.Log("5");
                    break;

                default:
                    break;
            }
            if (GTimer.thisTime[0] < GTimer.curBest[0])
            {
                GTimer.setBest(GTimer.thisTime);
            }

            else if (GTimer.thisTime[0] == GTimer.curBest[0])
            {
                if (GTimer.thisTime[1] < GTimer.curBest[1])
                {
                    GTimer.setBest(GTimer.thisTime);
                }
            }

            Menu.time1 = GTimer.getBestString(1);
            Menu.time2 = GTimer.getBestString(2);
            Menu.time3 = GTimer.getBestString(3);
            Menu.time4 = GTimer.getBestString(4);
            Menu.time5 = GTimer.getBestString(5);


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
