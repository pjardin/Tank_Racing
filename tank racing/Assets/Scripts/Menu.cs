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
    	if(GUI.Button(new Rect(origin_x, origin_y + buttonHeight * -3 + 60, buttonWidth,buttonHeight), "LEVEL 1")) {
            shareDataClass.offset = 1;
            Application.LoadLevel(1);
    	}

        if (GUI.Button(new Rect(origin_x, origin_y + buttonHeight * -2 + 60, buttonWidth, buttonHeight), "LEVEL 2"))
        {
            shareDataClass.offset = 5;
            Application.LoadLevel(1);
        }

        if (GUI.Button(new Rect(origin_x, origin_y + buttonHeight * -1 + 60, buttonWidth, buttonHeight), "LEVEL 3"))
        {
            shareDataClass.offset = 10;
            Application.LoadLevel(1);
        }

        if (GUI.Button(new Rect(origin_x, origin_y + buttonHeight * 0 + 60, buttonWidth, buttonHeight), "LEVEL 4"))
        {
            shareDataClass.offset = 15;
            Application.LoadLevel(1);
        }

        if (GUI.Button(new Rect(origin_x, origin_y + buttonHeight * 1 + 60, buttonWidth, buttonHeight), "LEVEL 5"))
        {
            shareDataClass.offset = 20;
            Application.LoadLevel(1);
        }


        if (GUI.Button(new Rect(origin_x, origin_y + buttonHeight * 2 + 60, buttonWidth, buttonHeight), "How to Play"))
        {
            Application.LoadLevel(2);
        }

            if (GUI.Button(new Rect(origin_x,origin_y+buttonHeight*3+60,buttonWidth,buttonHeight), "Quit")) {
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




