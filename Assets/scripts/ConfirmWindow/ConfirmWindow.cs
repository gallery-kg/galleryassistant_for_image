using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmWindow : MonoBehaviour {

    public delegate void ConfirmWindowDelegate();

    public event ConfirmWindowDelegate ConfirmEvent;
    public event ConfirmWindowDelegate RejectEvent;
    [SerializeField] private Text message;
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Button rejectBtn;

    private void OnEnable()
    {
        confirmBtn.onClick.AddListener(SubmitClickListener);
        rejectBtn.onClick.AddListener(RejectClickListener);
    }

    private void OnDisable()
    {
        confirmBtn.onClick.RemoveListener(SubmitClickListener);
        rejectBtn.onClick.RemoveListener(RejectClickListener);
    }

    private void RejectClickListener() {
        if (RejectEvent != null) {
            RejectEvent();
        }
    }

    private void SubmitClickListener()
    {
        if (ConfirmEvent != null)
        {
            ConfirmEvent();
        }
    }

    public void SetMessage(string m)
    {
        message.text = m;
    }
}
