using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {
    public delegate void MessageManagerDelegate();

    public static MessageManager instance;
    [SerializeField] private WaitWindow waitWindow;
    [SerializeField] private ConfirmWindow confirmWindow;

    private bool wait;
    private void Awake() {
        instance = this;
        DontDestroyOnLoad(this);
        waitWindow.ConfirmEvent += ConfirmEventOnWaitWindowListener;
        confirmWindow.ConfirmEvent += ConfirmEvenOnConfirmWndow;
        confirmWindow.RejectEvent += RejectEvenOnConfirmWndow;

    }

    private void ConfirmEventOnWaitWindowListener() {
        wait = false;
    }

    public void StartWaitWindow(string m, MessageManagerDelegate CallBack = null) {
        StartCoroutine(StartWaitWindowIEnumerator(m, CallBack));
    }

    private IEnumerator StartWaitWindowIEnumerator(string m, MessageManagerDelegate CallBack = null) {
        waitWindow.SetMessage(m);
        waitWindow.gameObject.SetActive(true);
        wait = true;
        while (wait) {
            yield return new WaitForEndOfFrame();
        }
        waitWindow.gameObject.SetActive(false);
        if (CallBack != null) {
            CallBack();
        }
    }

    /* Confirm window*/
    private bool confirm;
    private void RejectEvenOnConfirmWndow() {
        
        confirm = false;
        wait = false;
    }

    private void ConfirmEvenOnConfirmWndow() {
        
        confirm = true;
        wait = false;
    }

    public void StartConfirmWindow(string m, MessageManagerDelegate CallBack = null) {
        StartCoroutine(StartWaitConfirmWindowIEnumerator(m, CallBack));
    }

    private IEnumerator StartWaitConfirmWindowIEnumerator(string m, MessageManagerDelegate CallBack = null) {
        confirmWindow.SetMessage(m);
        confirmWindow.gameObject.SetActive(true);
        wait = true;
        while (wait) {
            yield return new WaitForEndOfFrame();
        }
        confirmWindow.gameObject.SetActive(false);
        if (confirm) {
            if (CallBack != null) {
                CallBack();
            }
        }
    }
}
