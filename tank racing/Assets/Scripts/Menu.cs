using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public int buttonWidth;
	public int buttonHeight;
	private int origin_x;
	private int origin_y;
	public GUISkin mySkin;
    //public Script GTimer;
    public static string time1;
    public static string time2;
    public static string time3;
    public static string time4;
    public static string time5;
    public float[] tmpTime;

    // Start is called before the first frame update
    void Start()
    {
        buttonWidth = 200;
        buttonHeight = 60;
        origin_x = Screen.width/2 - buttonWidth/2;
        origin_y = Screen.height/2 - buttonHeight*2;
        
    }

    void OnGUI() {
    	GUI.skin = mySkin;
        if (GUI.Button(new Rect(origin_x - (int)(buttonWidth * .5) - 5, origin_y + (int)(buttonHeight * -1.7) + 60, buttonWidth, buttonHeight), "LEVEL 1\nBest Time: " + time1))
        {
            GTimer.reset();
            GTimer.lvl = GTimer.Level.O;
            shareDataClass.offset = 1;
            Application.LoadLevel(1);
    	}

        if (GUI.Button(new Rect(origin_x + (int)(buttonWidth * .5) + 5, origin_y + (int)(buttonHeight * -1.7) + 60, buttonWidth, buttonHeight), "LEVEL 2\nBest Time: " + time2))
        {
            GTimer.reset();
            GTimer.lvl = GTimer.Level.Tw;
            shareDataClass.offset = 5;
            Application.LoadLevel(1);
        }

        if (GUI.Button(new Rect(origin_x - (int)(buttonWidth * .5) - 5, origin_y + (int)(buttonHeight * -0.5) + 60, buttonWidth, buttonHeight), "LEVEL 3\nBest Time: " + time3))
        {
            GTimer.reset();
            GTimer.lvl = GTimer.Level.Th;
            shareDataClass.offset = 10;
            Application.LoadLevel(1);
        }

        if (GUI.Button(new Rect(origin_x + (int)(buttonWidth * .5) + 5, origin_y + (int)(buttonHeight * -0.5) + 60, buttonWidth, buttonHeight), "LEVEL 4\nBest Time: " + time4))
        {
            GTimer.reset();
            GTimer.lvl = GTimer.Level.Fo;
            shareDataClass.offset = 15;
            Application.LoadLevel(1);
        }

        if (GUI.Button(new Rect(origin_x, origin_y + (int)(buttonHeight * .7) + 60, buttonWidth, buttonHeight), "LEVEL 5\nBest Time: " + time5))
        {
            GTimer.reset();
            GTimer.lvl = GTimer.Level.Fi;
            shareDataClass.offset = 20;
            Application.LoadLevel(1);
        }


        if (GUI.Button(new Rect(origin_x, origin_y + (int)(buttonHeight * 2.4) + 60, buttonWidth, buttonHeight), "How to Play"))
        {
            Application.LoadLevel(2);
        }

        if (GUI.Button(new Rect(origin_x, origin_y + (int)(buttonHeight * 3.6) + 60, buttonWidth, buttonHeight), "Quit"))
        {
            #if UNITY_EDITOR
    			UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
    		#endif
    	}
    }
}



//https://stackoverflow.com/questions/42393259/load-scene-with-param-variable-unity
public static class shareDataClass
{
    public static int offset { get; set; }
}




