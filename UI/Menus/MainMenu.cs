using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) {
            return;
        } else {
            Destroy(player);
        }
        
    }
    private void Start() {
        Screen.fullScreen = true;
        FindObjectOfType<AudioManager>().Play("TitleTheme");
        Cursor.visible = true;
    }

   public void LoadLevel1() {
       SceneManager.LoadScene("Level1", LoadSceneMode.Single);
       Cursor.visible = false;

   }

   public void LoadLevel2() {
       SceneManager.LoadScene("Level2", LoadSceneMode.Single);
   }

   public void LoadLevel3() {
       SceneManager.LoadScene("Level3", LoadSceneMode.Single);
   }

   public void QuitGame() {
       Application.Quit();
   }
}
