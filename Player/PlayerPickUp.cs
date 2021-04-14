using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{

    // allows other classes to reference methods in this script and call them 
    public static PlayerPickUp Instance { get; private set; }
   
    //variables and references handling the mechanic
    public float rangePickUp = 5f;
    public float moveForce = 100f;
    public Transform holdParent;
    public GameObject objHeld; 
    [SerializeField] private Camera cam;
    public bool isHolding;

    private void Awake() {
        Instance = this;
    }
    

    void Update()
    {
        //if mouse down, and not holding an object, call PickUpObject when in "range".
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (objHeld == null) {
                if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out RaycastHit rayHit, rangePickUp)) {
                
                PickUpObject(rayHit.transform.gameObject);
                }
            } 
            else {
                DropObject();
            }
            
        }

        //if holding object, call MoveHelddObject method
        if (objHeld != null) {
            MoveHeldObject();
        }

        //method called to move an object while holding it
        void MoveHeldObject() {
            if(Vector3.Distance(objHeld.transform.position, holdParent.position) > 0.1f) {
                Vector3 objectMoveDir = (holdParent.position - objHeld.transform.position);
                objHeld.GetComponent<Rigidbody>().AddForce(objectMoveDir * moveForce);
            }
        }

    }

    //method that moves desired object, if it has Rigidbody and Interactable tag, to desired point and disables gravity
    void PickUpObject(GameObject selectObj) {
        if(selectObj.GetComponent<Rigidbody>() && selectObj.CompareTag("Interactable") && isHolding == false) {
            
            isHolding = true;

            //tweaks variables with the objects Rigidbody
            Rigidbody objRig = selectObj.GetComponent<Rigidbody>(); 
            objRig.useGravity = false;
            objRig.drag = 10;

            //makes the object semi-transparent
            MeshRenderer objRenderer = selectObj.GetComponent<MeshRenderer>();
            Color objColor = objRenderer.material.GetColor("_Color");
            objColor.a = 0.6f;
            objRenderer.material.SetColor("_Color", objColor);

            //sets the object's parent to be holdParent, and makes the object within this class set to be the "class-wide" object
            objRig.transform.parent = holdParent;
            objHeld = selectObj;
        }
    }

    

    //does the inverse of PickUpObject. 
    void DropObject() {
       Rigidbody objHeldRigidibody = objHeld.GetComponent<Rigidbody>();

       isHolding = false;

        objHeldRigidibody.useGravity = true;
        objHeldRigidibody.drag = 1;

        MeshRenderer objRenderer = objHeld.GetComponent<MeshRenderer>();
        Color objColor = objRenderer.material.GetColor("_Color");
        objColor.a = 1f;
        objRenderer.material.SetColor("_Color", objColor);

        objHeld.transform.parent = null;
        objHeld = null;
    }

}
