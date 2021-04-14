using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public static DialogueTrigger instance { get; private set; }

    //references to game objects that should be tweaked during/after dialogue
    public GameObject door;
   public Dialogue dialogue;
   public GameObject dialogueMenu;
   public GameObject cursor;

   public GameObject dialogueTrigger;

   public Light sun;
   public GameObject sphereLight;
   public static bool inDialogue;


private void Awake() {
    instance = this;
}
private void Start() {
    door.GetComponent<Animator>().enabled = false;
    sun.gameObject.SetActive(false);
}
   public void TriggerDialogue() {
       FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
   }

    //when Player enters the trigger zone, start dialogue
   private void OnTriggerEnter(Collider other) {
       if (other.CompareTag("Player")) {
           inDialogue = true;
           Cursor.visible = true;
        dialogueMenu.SetActive(true);
        cursor.SetActive(false);
        PlayerPickUp.Instance.isHolding = true;
       Cursor.lockState = CursorLockMode.Confined;
       Time.timeScale = 0f;
       TriggerDialogue();
       }
       
   }


    //since time is frozen during dialogue, and the trigger is destroyed, the OnTriggerStay method is needed to do stuff after dialogue
   private void OnTriggerStay(Collider other) {
       if (other.CompareTag("Player")) {
           door.GetComponent<Animator>().enabled = true;
           sun.gameObject.SetActive(true);
           Destroy(sphereLight);
           FunctionTimer.Create(PlayDoorSound, 0.8f);
       }

   }

   private void PlayDoorSound() {
           FindObjectOfType<AudioManager>().Play("DoorSound");
   }
}
