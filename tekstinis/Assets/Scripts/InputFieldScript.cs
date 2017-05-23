using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldScript : MonoBehaviour {

    public string answer;
	// Use this for initialization
	void Start () {
        var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(SubmitAnswer);
        input.onEndEdit = se;
        //input.onEndEdit.AddListener(SubmitAnswer);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void SubmitAnswer(string arg0)
    {
        Debug.Log(arg0);
    }
}
