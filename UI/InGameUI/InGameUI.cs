using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public static InGameUI instance;

    //references and variables handling pausing the game
    public static bool isPaused = false;
    public GameObject pauseMenu;

    public AudioMixer audioMixer;

    //reference to the crosshair
    public GameObject crossHair;
    
    private void Awake() {
        if( instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
    }
    private void Start() {
        Cursor.visible = false;
        crossHair.SetActive(true);
    }
    void Update()
    {
        //  && WinMenu.Instance.inWinMenu == false
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (DialogueTrigger.inDialogue == false || DialogueTrigger.instance == null) {
                    if (isPaused) {
                Resume();
             } else {
                Pause();
                }
                }
                
        }

    }


    public void Resume() {
        Cursor.visible = false;
        PlayerPickUp.Instance.isHolding = true;
        pauseMenu.SetActive(false);
        crossHair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;
        FunctionTimer.Create(HoldTimer, 0.1f);
    }

    void Pause() {
        Cursor.visible = true;
        PlayerPickUp.Instance.isHolding = true;
        pauseMenu.SetActive(true);
        crossHair.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        isPaused = true;
    }

    void HoldTimer() {
        PlayerPickUp.Instance.isHolding = false;
    }

    public void SetGameVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetSensitivity(float sensitivity) {
        CameraController.Instance.mouseSensitivity = sensitivity;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
