using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTeleportation : MonoBehaviour
{
    //variables and references handling placing "markers"
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerMarker;
    public float rangePlaceMarker = 8.5f;
    public GameObject placeMarkerRange;
    
    //variables and references handling teleporting object to "markers"  
    public Transform teleportPoint;
    public float teleportRange = 50f;
    public Transform particlesHere;

    //reference to the Teleport Particle System
    public ParticleSystem teleportParticles;
    
    void Update()
    {
        if(Input.anyKeyDown) {
            placeMarkerRange.GetComponent<MeshRenderer>().enabled = false;
        }
        
        MakePlayerMarker();
        TeleportObject();
    }

    /* when "F" key is pressed, target is in range, and target doesnt have the "Interactable" or "PlayerMarker" tag, set a "marker" on that spot
    also play sound */
    private void MakePlayerMarker() {
        if (Input.GetKeyDown(KeyCode.F)) {
            placeMarkerRange.GetComponent<MeshRenderer>().enabled = true;
           if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit rayHit, rangePlaceMarker)) {
               GameObject objHit = rayHit.transform.gameObject;
               if (!objHit.CompareTag("Interactable") && !objHit.CompareTag("PlayerMarker") && !objHit.CompareTag("Player") && !objHit.CompareTag("Win")) {
                   playerMarker.position = rayHit.point;
                   AudioManager.instance.Play("PlaceMarker");
               } 
           }
        }
    }

    /* when "E" key is pressed, cast a ray that teleports an object hit- if it has "Interactable" tag- to slightly above the marker position
    if teleport is succseeful, also use Audio Manager to play sound and create particle effects */
    private void TeleportObject() {
        if(Input.GetKeyDown(KeyCode.E)) {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit raycastHit, teleportRange)) {
                Transform objHit = raycastHit.transform;
                
             if (objHit.gameObject.CompareTag("Interactable") && PlayerPickUp.Instance.isHolding == false) {
                 AudioManager.instance.Play("TeleportSound");
                 Instantiate(teleportParticles, particlesHere.position, Quaternion.identity);
                 objHit.position = teleportPoint.position;
                 
             }
            }
        }
    }

    
}
