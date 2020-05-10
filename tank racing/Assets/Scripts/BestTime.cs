using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestTime : MonoBehaviour
{
    public static float[] bestTime1;
    public static float[] bestTime2;
    public static float[] bestTime3;
    public static float[] bestTime4;
    public static float[] bestTime5;
    public enum Level { O, Tw, Th, Fo, Fi };
    public static Level lvl;

    // Start is called before the first frame update
    void Start()
    {
        bestTime1 = new float[] { 99f, 59f};
        bestTime2 = new float[] { 99f, 59f };
        bestTime3 = new float[] { 99f, 59f };
        bestTime4 = new float[] { 99f, 59f };
        bestTime5 = new float[] { 99f, 59f };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float[] getBest(int level)
    {
        switch(level)
        {
            case 1:
                return bestTime1;
                break;

            case 2:
                return bestTime2;
                break;

            case 3:
                return bestTime3;
                break;

            case 4:
                return bestTime4;
                break;

            case 5:
                return bestTime5;
                break;

            default:
                float[] tmp = { 0f, 0f };
                return tmp;
                
        }
    }

    public static void setBest(float[] newTime, int level)
    {
        

        switch (level)
        {
            case 1:
                bestTime1[0] = newTime[0];
                bestTime1[1] = newTime[1];
                break;

            case 2:
                bestTime2[0] = newTime[0];
                bestTime2[1] = newTime[1];
                break;

            case 3:
                bestTime3[0] = newTime[0];
                bestTime3[1] = newTime[1];
                break;

            case 4:
                bestTime4[0] = newTime[0];
                bestTime4[1] = newTime[1];
                break;

            case 5:
                bestTime5[0] = newTime[0];
                bestTime5[1] = newTime[1];
                break;

            default:
                break;
        }
    }
}
