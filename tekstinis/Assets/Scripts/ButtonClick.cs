using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour {
    public GameManager gm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onClickChoice1()
    {
        gm.choiceOPtion = 1;
    }
    public void onClickChoice2()
    {
        gm.choiceOPtion = 2;
    }
    public void onClickChoice3()
    {
        gm.choiceOPtion = 3;
    }
    public void onClickChoice4()
    {
        gm.choiceOPtion = 4;
    }
    public void onClickChoice5()
    {
        gm.choiceOPtion = 5;
    }
    public void onClickChoice6()
    {
        gm.choiceOPtion = 6;
    }
}
