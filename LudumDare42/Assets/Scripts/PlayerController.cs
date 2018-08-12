using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    public float jumpStrength = 5f;
    public float throwForce = 2f;
    public Vector2 throwDirection;
    public float jumpMult, fallMult;
    public float objectAreaSize;

    public LayerMask mask, floorMask;

    public Transform objectHoldPos, groundCheck;

    bool isFacingRight;
    public  bool isGrounded;
    int jumpNum;

    [FMODUnity.EventRef]
    public string jumpSound, pickUpSound, throwSound, rotateSound;
    


    Transform currentObject, currentObjectParent;
    Rigidbody2D objectRB;

    float h;


    bool isHoldingObject;

    Rigidbody2D rB;
    Animator anim;
    PauseManager pM;

	// Use this for initialization
	void Start () {
        rB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pM = FindObjectOfType<PauseManager>();
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        CarryObject();
        CheckIfGrounded();
        SetAnimStuff();
	}


    void SetAnimStuff() {

        anim.SetBool("isHolding", isHoldingObject);
        anim.SetFloat("h", Mathf.Abs(h));


    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Object") {
            if (Input.GetAxis("PickUp") > 0 || Input.GetButtonDown("PickUpButton")) {
                if (currentObjectParent == null)
                {
                    Debug.Log("Pick Up Object");
                    isHoldingObject = true;
                    currentObject = collision.gameObject.transform;
                    currentObjectParent = currentObject.parent;




                    objectRB = currentObjectParent.gameObject.GetComponent<Rigidbody2D>();
                    currentObjectParent.rotation = Quaternion.Euler(Vector3.zero);
                    objectRB.constraints = RigidbodyConstraints2D.FreezeRotation;
                    objectRB.simulated = false;
                    currentObjectParent.position = objectHoldPos.position + Vector3.up;

                    FMODUnity.RuntimeManager.PlayOneShot(pickUpSound);
                }
            }

        }
    }

    void CarryObject() {
        if (isHoldingObject) {
            currentObjectParent.position = objectHoldPos.position + Vector3.up;
            
            if (Input.GetAxis("PickUp") == 0 && !Input.GetButton("PickUpButton"))
            {
                ThrowObject();

            }
            else if (Input.GetButtonDown("Rotate")) {
                currentObjectParent.Rotate(new Vector3(0, 0, 90));
                FMODUnity.RuntimeManager.PlayOneShot(rotateSound);
            }

        }

    }

    void ThrowObject() {
       // Debug.Log("Throw Object");
        isHoldingObject = false;
        currentObjectParent = null;


             objectRB.constraints = RigidbodyConstraints2D.None;
            objectRB.simulated = true;
        if (isFacingRight)
        {
            objectRB.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        }
        else {
            objectRB.AddForce(new Vector2(-throwDirection.x,throwDirection.y) * throwForce, ForceMode2D.Impulse);
        }

        FMODUnity.RuntimeManager.PlayOneShot(throwSound);

    }
    


    void Movement() {
        h = Input.GetAxis("Horizontal");

        Vector3 transScale = transform.localScale;
        if (h < 0)
        {
            transScale.x = -1;
            isFacingRight = false;
        }
        else if(h > 0) {
            transScale.x = 1;
            isFacingRight = true;
        }

        transform.localScale = transScale;


        transform.position += new Vector3(h * speed * Time.deltaTime, 0, 0);

        if (rB.velocity.y < 0)
        {
            rB.velocity += Vector2.up * Physics2D.gravity * jumpMult * Time.deltaTime;
        }
        else if (rB.velocity.y > 0 && !Input.GetButton("Jump")) {
            rB.velocity += Vector2.up * Physics2D.gravity * fallMult * Time.deltaTime;
        }




        if (Input.GetButtonDown("Jump") && !pM.isPaused) {
            if (jumpNum < 2)
            {
                rB.AddForce(Vector2.up * 100 * jumpStrength);
                jumpNum++;
                FMODUnity.RuntimeManager.PlayOneShot(jumpSound);
            }


        }

    }


    void CheckIfGrounded() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, floorMask);
        if (isGrounded) {
            jumpNum = 0;
        }

    }
}
