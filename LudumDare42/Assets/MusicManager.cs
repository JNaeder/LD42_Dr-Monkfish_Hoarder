using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MusicManager : MonoBehaviour {

    [FMODUnity.EventRef]
    public string musicEvent;

    FMOD.Studio.EventInstance musicInst;


    public static MusicManager musicManager;

    GameManager gM;

    public float musicLevelNum;


    private void Awake()
    {
        if (musicManager != null)
        {
            Destroy(gameObject);
        }
        else {
            musicManager = this;

        }
        
    }

    // Use this for initialization
    void Start () {
        gM = FindObjectOfType<GameManager>();



        musicInst = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        musicInst.start();

        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
        

	}


    public void ChangeMusicLevel(int levelNum) {
        musicInst.setParameterValue("Level", levelNum);
        musicLevelNum = levelNum;
    }
}
