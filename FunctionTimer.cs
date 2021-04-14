using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer
{
    //references that allow a List to be made, and a game object created
    private static List<FunctionTimer> activeTimerList;
    private static GameObject initGameObject;

    //method to start new list and creates game object if none exist
    private static void InitIfNeeded() {
        if (initGameObject == null) {
            initGameObject = new GameObject("FunctionTimer_InitGameObject");
            activeTimerList = new List<FunctionTimer>();
        }
    }

    //allows other classes to use the timer
    public static FunctionTimer Create(Action action, float timer, string timerName = null) {
        InitIfNeeded();

        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
        FunctionTimer functionTimer = new FunctionTimer(action, timer, timerName, gameObject);
        
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;

        activeTimerList.Add(functionTimer);

        return functionTimer;
    }


    //method called to remove timers
    private static void RemoveTimer(FunctionTimer functionTimer) {
        InitIfNeeded();
        activeTimerList.Remove(functionTimer);
    }

    //method allowing multiple timers and calls DestroySelf() function on any timer that is "done", removing them from the list
    private static void StopTimer(string timerName) {
        for (int i = 0; i < activeTimerList.Count; i++) {
            if (activeTimerList[i].timerName == timerName) {
                activeTimerList[i].DestroySelf();
                i--;
            }
        }
    }

    //allows the timer to hook onto monobehavior properties
    private class MonoBehaviourHook : MonoBehaviour {
        public Action onUpdate;
        private void Update() {
            if (onUpdate != null) onUpdate();
        }

    }
    
    //variables and references handling the FunctionTimer() method
    private Action action;
    private float timer;
    private string timerName;
    private GameObject gameObject;
    private bool isDestroyed;

    //defines variables to be used when method is called
    private FunctionTimer(Action action, float timer, string timerName, GameObject gameObject) {
        this.action = action;
        this.timer = timer;
        this.timerName = timerName;
        this.gameObject = gameObject;
        isDestroyed = false;
    }

    //counts down all timers and deletes them when duration is up
    public void Update() {
        if(!isDestroyed) {
            timer -= Time.deltaTime;
            if (timer < 0) {
                action();
                DestroySelf();
            }
        }
    }

    //method to remove timer and associated game object
    private void DestroySelf() {
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
        RemoveTimer(this);
    }
}
