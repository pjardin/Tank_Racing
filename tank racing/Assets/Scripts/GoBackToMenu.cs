using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackToMenu : MonoBehaviour
{
	public GUISkin skin;
    public float killCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnGUI() {
    	GUI.skin = skin;
    	if(GUI.Button(new Rect(20,20,200,100),"Menu")) {
    		Application.LoadLevel(0);
    	}

        GUI.TextArea(new Rect(700, 20, 200, 50), "time left: " + killCount.ToString());
    }
}







