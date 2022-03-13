using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    
    public static WinMenu Instance { get; private set; }
    public GameObject winMenu;
    public GameObject crossHair;
    public GameObject pasueMenu;
    public bool inWinMenu = false;
    
    
    private void Awake() {
        Instance = this;
    }

    //If the Player collides with the Win Object, stop the player from interacting with the game 
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
        Cursor.visible = true;
        inWinMenu = true;
        PlayerPickUp.Instance.isHolding = true;
        winMenu.SetActive(true);
        crossHair.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
    }
    }
    
    //if next scene exists in build settings, load it. If not, loads the Menu
    public void NextStage() {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            inWinMenu = false;
            winMenu.SetActive(false);
            Cursor.visible = false;
        } else {
            SceneManager.LoadScene("Menu");
            inWinMenu = false;
            winMenu.SetActive(false);
        }
        
    }
    
    // if the Player wants to go back to the Main Menu, load it
    public void LoadMenu() {
        Time.timeScale = 1f;
        inWinMenu = false;
        winMenu.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    

    public void QuitGame() {
        Application.Quit();
    }
    
}
