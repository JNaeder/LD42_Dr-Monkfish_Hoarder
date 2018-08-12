using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ObjectSpawner : MonoBehaviour {

    public Transform[] spawners;
    public GameObject[] furniture;

    public float spawnSpeed;

    [FMODUnity.EventRef]
    public string dropItemSound;

    float tempTime;
    

    // Use this for initialization
    void Start () {

        tempTime = Time.time;
        SpawnPiece();
       
		
	}
	
	// Update is called once per frame
	void Update () {
        SpawnTimer();
	}


    void SpawnPiece() {
        int randSpawnerNum = Random.Range(0, spawners.Length);
        int randomObjectNum = Random.Range(0, furniture.Length);

        GameObject newObject = Instantiate(furniture[randomObjectNum], spawners[randSpawnerNum].position, Quaternion.identity);
        FMODUnity.RuntimeManager.PlayOneShot(dropItemSound);
    }

    


    public void SpawnTimer() {
        if (Time.time > tempTime + spawnSpeed) {
            SpawnPiece();
            tempTime = Time.time;

        }


    }
}
