using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void LoadScene(int sceneNum) {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNum);
        
    }


    public void QuitGame() {
        Application.Quit();

    }
}
