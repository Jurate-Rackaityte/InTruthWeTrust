using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Class;
using UnityEngine.UI;

public class ScoresScript : MonoBehaviour {
    private GameManager gm;
    public Text PhText;
    public Text PmText;
    public Text JoaH;
    public Text JoaM;
    public Text JosH;
    public Text JosM;
    public Text aggresiveness;
	// Use this for initialization
	void Start () {
        //pasiimti is pagr. zaidimo zaidejo surinktus "taskus"
        gm = GameObject.FindObjectOfType<GameManager>();
        PhText.text = gm.PH.ToString();
        PmText.text = gm.PM.ToString();
        JoaH.text = gm.JoaH.ToString();
        JoaM.text = gm.JoaM.ToString();
        JosH.text = gm.JosH.ToString();
        JosM.text = gm.JosM.ToString();
        aggresiveness.text = gm.aggresiveness.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void getBack()
    {
        SceneManager.LoadScene(0);
    }
}
