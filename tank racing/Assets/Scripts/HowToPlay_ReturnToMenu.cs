using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay_ReturnToMenu : MonoBehaviour
{

    public GUISkin skin;
    private string obHow;
    public GameObject board;
    public Material Inst;
    public Material Obj;
    private bool page;

    void OnGUI()
    {
        GUI.skin = skin;
        if (GUI.Button(new Rect(20, 20, 200, 100), "Menu"))
        {
            Application.LoadLevel(0);
        }
        if (GUI.Button(new Rect(Screen.width - 220, Screen.height - 120, 200, 100), obHow))
        {
            if (!page)
            {
                board.GetComponent<MeshRenderer>().material = Obj;
                obHow = "Instructions";
                page = true;
            }

            else
            {
                board.GetComponent<MeshRenderer>().material = Inst;
                obHow = "Objective";
                page = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        page = false;
        obHow = "Objective";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}