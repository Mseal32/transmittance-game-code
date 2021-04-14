using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarker : MonoBehaviour
{
    //this script is to be put on the PlayerMarker game object, and handles its interactions with other objects
    [SerializeField] private GameObject cube;

    //when an object that is tagged "Interactable" touches the PlayerMarker, disable the marker renderer    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Interactable")) {
            cube.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    //when an object that is tagged "Interactable" leaves the marker's collider, reenable the marker's renderer
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Interactable")) {
            cube.GetComponent<MeshRenderer>().enabled = true;
        }
    } 
}
