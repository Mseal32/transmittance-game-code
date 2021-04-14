using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    //singleton so other 
    public static WinMenu Instance { get; private set; }
    public GameObject winMenu;
    public GameObject crossHair;
    public GameObject pasueMenu;
    public bool inWinMenu = false;

    private void Awake() {
        Instance = this;
    }

    //should have this in a different script and call it here
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

    public void LoadMenu() {
        Time.timeScale = 1f;
        inWinMenu = false;
        winMenu.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    //if next scene exists in build settings, load it. If not, load Menu
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

    public void QuitGame() {
        Application.Quit();
    }
    
}
