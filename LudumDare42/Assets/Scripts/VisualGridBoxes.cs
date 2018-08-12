using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualGridBoxes : MonoBehaviour {
    Transform[,] grid;
    SpriteRenderer[] boxes;
    public LayerMask mask;
    public float boxSize;

    public Color startColor, objectInItColor;

    GameManager gM;

	// Use this for initialization
	void Start () {
        gM = FindObjectOfType<GameManager>();
        //Debug.Log(gM.gridBoxSize);
        boxes = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sP in boxes) {
            sP.color = startColor;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        foreach (SpriteRenderer sP in boxes) {
            Vector3 boxPos = sP.gameObject.transform.position;
            if (Physics2D.OverlapBox(boxPos, new Vector2(boxSize, boxSize), mask))
            {
                sP.color = objectInItColor;
               // Debug.Log(Physics2D.OverlapBox(boxPos, Vector2.one * 0.65f, mask).gameObject.name);
                if ((Physics2D.OverlapBox(boxPos, new Vector2(boxSize, boxSize), mask).gameObject.name == "Player")){
                    sP.color = startColor;
                }
            }
            else {
                sP.color = startColor;
            }

        }

		
	}
}
