using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class MainMenu : MonoBehaviour {

    public GameObject mainScreen, controlsScreen;
    public GameObject startGameButton, controlsBackButton;

    [FMODUnity.EventRef]
    public string menuButtonSound;


    EventSystem eS;

    // Use this for initialization
    void Start() {
        eS = FindObjectOfType<EventSystem>();

        eS.SetSelectedGameObject(startGameButton);
    }

    // Update is called once per frame
    void Update() {

    }


    public void ShowControlsScreen()
    {
        mainScreen.SetActive(false);
        controlsScreen.SetActive(true);
        eS.SetSelectedGameObject(null);
        eS.SetSelectedGameObject(controlsBackButton);


    }


    public void BackToMainMenu() {
        mainScreen.SetActive(true);
        controlsScreen.SetActive(false);
        eS.SetSelectedGameObject(null);
        eS.SetSelectedGameObject(startGameButton);


    }

    public void PlayMenuButtonSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(menuButtonSound);

    }
}
