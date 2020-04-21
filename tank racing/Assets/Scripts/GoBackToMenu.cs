using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackToMenu : MonoBehaviour
{
	public GUISkin skin;
    public float killCount;
    bool finished = false;    
    public void setFinished()
    {
        finished = true;
    }
    void OnGUI() {
    	GUI.skin = skin;
    	if(GUI.Button(new Rect(20,20,200,100),"Menu") || finished==true ) {
    		Application.LoadLevel(0);
            finished = false;
    	}

        GUI.TextArea(new Rect(Screen.width/2-100, 20, 200, 50), "time left: " + killCount.ToString());
    }
}







