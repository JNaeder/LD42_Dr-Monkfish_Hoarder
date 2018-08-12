using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class DeathLine : MonoBehaviour {

    GameManager gM;

    public LayerMask mask;
    public float damageMult = 1;

    bool isTakingDamage;
    bool isGameOver;

    public Animator canvasAnim;

    [FMODUnity.EventRef]
    public string takingDamageSound;

    FMOD.Studio.EventInstance takingDamageInst;


    MusicManager mM;



    bool objectIsTouching;

	// Use this for initialization
	void Start () {
        gM = FindObjectOfType<GameManager>();
        mM = FindObjectOfType<MusicManager>();

        takingDamageInst = FMODUnity.RuntimeManager.CreateInstance(takingDamageSound);
        takingDamageInst.start();

        takingDamageInst.setParameterValue("GettingHit", 0);
    }
	
	// Update is called once per frame
	void Update () {
        CheckIfObjectIsTouching();
        canvasAnim.SetBool("isGettingHurt", isTakingDamage);

	}


    void RemoveHealth() {
        gM.health -= Time.deltaTime * damageMult;
        if (gM.health <= 0) {
            if (!isGameOver)
            {
                takingDamageInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                takingDamageInst.release();
                gM.GameOver();
                mM.ChangeMusicLevel(0);
                isGameOver = true;
            }
        }
    }


    void CheckIfObjectIsTouching() {
        objectIsTouching = Physics2D.OverlapBox(transform.position, transform.localScale, 0, mask);

        if (objectIsTouching)
        {
            RemoveHealth();
            isTakingDamage = true;
            takingDamageInst.setParameterValue("GettingHit", 1);
        }
        else {
            isTakingDamage = false;
            takingDamageInst.setParameterValue("GettingHit", 0);
        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
