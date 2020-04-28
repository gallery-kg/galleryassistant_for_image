using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitWindow : MonoBehaviour {
    public delegate void WaitWindowDelegate();

    public event WaitWindowDelegate ConfirmEvent;
    [SerializeField] private Text message;
    [SerializeField] private Button submit;

    private void OnEnable(){
        submit.onClick.AddListener(SubmitClickListener);
    }

    private void OnDisable(){
        submit.onClick.RemoveListener(SubmitClickListener);
    }

    private void SubmitClickListener() {
        if (ConfirmEvent != null) {
            ConfirmEvent();
        }
    }

    public void SetMessage(string m) {
        message.text = m;
    }
}
