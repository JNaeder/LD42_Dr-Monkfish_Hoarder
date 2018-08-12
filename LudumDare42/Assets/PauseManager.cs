using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class PauseManager : MonoBehaviour {

    public GameObject pauseMenu, controlsScreen;
    public GameObject pauseMenuResumeButton, controlsBackButton;

    EventSystem eS;


    public bool isPaused;

    [FMODUnity.EventRef]
    public string menuButtonSound;

	// Use this for initialization
	void Start () {
        eS = FindObjectOfType<EventSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckForButtonPress();	
	}

    void CheckForButtonPress() {


        if (Input.GetButtonDown("Pause")) {
            isPaused = !isPaused;
            if (isPaused) {
                PauseMenu();
            } else
            {
                UnPauseMenu();

            }

        }

    }



    public void PauseMenu() {
        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.SetActive(true);
        eS.SetSelectedGameObject(null);
        eS.SetSelectedGameObject(pauseMenuResumeButton);
    }

    public void UnPauseMenu() {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
        eS.SetSelectedGameObject(null);

    }
    public void ShowControlsScreen()
    {
        pauseMenu.SetActive(false);
        controlsScreen.SetActive(true);
        eS.SetSelectedGameObject(null);
        eS.SetSelectedGameObject(controlsBackButton);


    }


    public void BackToMainMenu()
    {
        pauseMenu.SetActive(true);
        controlsScreen.SetActive(false);
        eS.SetSelectedGameObject(null);
        eS.SetSelectedGameObject(pauseMenuResumeButton);


    }


    public void PlayMenuButtonSound() {
        FMODUnity.RuntimeManager.PlayOneShot(menuButtonSound);

    }

}
