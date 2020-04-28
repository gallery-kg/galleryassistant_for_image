using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventsHandler : MonoBehaviour, IPointerClickHandler {
    public delegate void EventsHandlerDelegate();

    public event EventsHandlerDelegate OnPointerClickEvent;

    public void OnPointerClick(PointerEventData eventData) {
        if (OnPointerClickEvent != null) {
            OnPointerClickEvent();
        }
    }
}
