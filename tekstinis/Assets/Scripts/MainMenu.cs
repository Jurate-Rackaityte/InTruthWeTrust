﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void AboutGame()
    {
        SceneManager.LoadScene(2);
    }
}
