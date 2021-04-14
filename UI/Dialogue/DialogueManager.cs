using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //all references to things concerning dialogue
    public static DialogueManager instance;
    public GameObject dialogueTrigger;
    public GameObject cursor;
    public GameObject dialogueManager;
    public Text dialogueText;
    private Queue<string> sentences;

    //initialize the Queue
    void Awake()
    {
        instance = this;
     sentences = new Queue<string>();   
    }

    //method that takes a dialogue parameter. For each line, or sentence, put them in the Queue, then call DisplayNextSentence()
    public void StartDialogue(Dialogue dialogue) {

        //  sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }


    //if sentences are remaining, calls the TypeSentence Coroutine and dequeues the 'sentence'. If not, calls method EndDialogue
    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        FindObjectOfType<AudioManager>().Play("Talk");
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Sets the text to cycle through every character in the sentence and display them one by one every frame 
    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }


    //reenables things that were disabled during dialogue, destroys the trigger, and sets the text and UI to be visible to false 
    public void EndDialogue() {
        dialogueManager.SetActive(false);
        DialogueTrigger.inDialogue = false;
        FindObjectOfType<AudioManager>().Stop("Talk");
       Cursor.lockState = CursorLockMode.Locked;
       Time.timeScale = 1f;
       Destroy(dialogueTrigger);
       cursor.gameObject.SetActive(true);
       PlayerPickUp.Instance.isHolding = false;
    }

    
}
