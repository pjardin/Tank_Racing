using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay_ReturnToMenu : MonoBehaviour
{

    public GUISkin skin;
    private string obHow;
    public GameObject board;
    public Texture env;
    public Texture inst;
    public Texture obj;
    private enum State { ENV, HOW, OBJ };
    private bool pass;

    private State state;

    void OnGUI()
    {
        GUI.skin = skin;
        if (GUI.Button(new Rect(20, 20, 200, 100), "Menu"))
        {
            Application.LoadLevel(0);
        }
        if (GUI.Button(new Rect(Screen.width - 220, 20, 200, 100), obHow))
        {
         
            switch (state)
            {
                case State.ENV:
                    state = State.OBJ;
                    board.GetComponent<RawImage>().texture = obj;
                    obHow = "Instructions";
                    break;

                case State.OBJ:
                    state = State.HOW;
                    board.GetComponent<RawImage>().texture = inst;
                    obHow = "Environment";
                    break;

                case State.HOW:
                    state = State.ENV;
                    board.GetComponent<RawImage>().texture = env;
                    obHow = "Objective";
                    break;

            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        board.GetComponent<RawImage>().texture = env;
        state = State.ENV;
        obHow = "Objective";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}