using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonSFX : MonoBehaviour
{
    //method that is called when a button is "hovered over" using the mouse cursor
    public void OnButtonHover () {
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
        FindObjectOfType<AudioManager>().ChangeVolume("TitleTheme", 0.07f);
    }


    //method that is called when the mouse cursor leaves a button
    public void OnButtonHoverExit () {
        FindObjectOfType<AudioManager>().ChangeVolume("TitleTheme", 0.35f);
    }
}
