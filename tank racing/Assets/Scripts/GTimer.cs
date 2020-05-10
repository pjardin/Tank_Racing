using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GTimer : MonoBehaviour
{
    public Text timer;
    public static float seconds, minutes, offset;
    public static float[] bestTime1;
    public static float[] bestTime2;
    public static float[] bestTime3;
    public static float[] bestTime4;
    public static float[] bestTime5;
    public enum Level { O, Tw, Th, Fo, Fi };
    public static Level lvl;
    public static float[] thisTime;
    public static float[] curBest;

    // Start is called before the first frame update
    void Start()
    {
        offset = Time.time;
        bestTime1 = new float[] { 99f, 59f };
        bestTime2 = new float[] { 99f, 59f };
        bestTime3 = new float[] { 99f, 59f };
        bestTime4 = new float[] { 99f, 59f };
        bestTime5 = new float[] { 99f, 59f };
        thisTime = new float[] { 99f, 59f };
        curBest = new float[] { 99f, 59f };
        //lvl = Level.O;
    }

    public static void reset()
    {
        offset += Time.time;
        seconds = 0f;
        minutes = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        minutes = (int)((Time.time - offset) / 60f);
        seconds = (int)((Time.time - offset) % 60f);
        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }


    public static float[] getBest(int level)
    {
        switch (level)
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
                float[] tmp = { 99f, 59f };
                return tmp;

        }
    }


    public static void setBest(float[] newTime)
    {


        switch (lvl)
        {
            case Level.O:
                bestTime1[0] = newTime[0];
                bestTime1[1] = newTime[1];
                break;

            case Level.Tw:
                bestTime2[0] = newTime[0];
                bestTime2[1] = newTime[1];
                break;

            case Level.Th:
                bestTime3[0] = newTime[0];
                bestTime3[1] = newTime[1];
                break;

            case Level.Fo:
                bestTime4[0] = newTime[0];
                bestTime4[1] = newTime[1];
                break;

            case Level.Fi:
                bestTime5[0] = newTime[0];
                bestTime5[1] = newTime[1];
                break;
        
        }
    }

    public static string getBestString(int level)
    {
        switch (level)
        {
            case 1:
                return ((int)bestTime1[0]).ToString("00") + ":" + ((int)bestTime1[1]).ToString("00");
                break;

            case 2:
                return ((int)bestTime2[0]).ToString("00") + ":" + ((int)bestTime2[1]).ToString("00");
                break;

            case 3:
                return ((int)bestTime3[0]).ToString("00") + ":" + ((int)bestTime3[1]).ToString("00");
                break;

            case 4:
                return ((int)bestTime4[0]).ToString("00") + ":" + ((int)bestTime4[1]).ToString("00");
                break;

            case 5:
                return ((int)bestTime5[0]).ToString("00") + ":" + ((int)bestTime5[1]).ToString("00");
                break;

            default:
                return "99:59";
                break;
        }
    }
}
