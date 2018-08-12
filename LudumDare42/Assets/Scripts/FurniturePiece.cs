using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePiece : MonoBehaviour {

    ObjectBlock[] blocks;

    int startingLayerNum;
    string startingTag;




	
	void Awake () {
        blocks = GetComponentsInChildren<ObjectBlock>();

        startingLayerNum = blocks[0].gameObject.layer;
        startingTag = blocks[0].gameObject.tag;



        ChangeEachBlock(0, "Untagged");

	}


    public void ChangeToStartingValues() {
        ChangeEachBlock(startingLayerNum, startingTag);
 
    }


    public void ChangeEachBlock(int layerNum, string tagName) {
        //Debug.Log("Testing");
        foreach (ObjectBlock oB in blocks) {
            oB.gameObject.layer = layerNum;
           // Debug.Log("Change String Name to " + tagName);
            oB.gameObject.tag = tagName;

        }


    }
}
