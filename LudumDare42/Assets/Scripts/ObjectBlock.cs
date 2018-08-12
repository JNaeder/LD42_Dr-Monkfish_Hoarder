using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBlock : MonoBehaviour {

    

    public LayerMask mask;

    bool hasTouchedStuff;
    bool isTouchingStuff;

    private void Awake()
    {


    }


    private void Update()
    {
        

        if (!hasTouchedStuff) {
            isTouchingStuff = Physics2D.OverlapBox(transform.position, transform.localScale, 0, mask);
            if (isTouchingStuff) {
                hasTouchedStuff = true;
                //Debug.Log("Hit! Change Layers");
                FurniturePiece fP = GetComponentInParent<FurniturePiece>();
                fP.ChangeToStartingValues();
            }


        }



    }
}
