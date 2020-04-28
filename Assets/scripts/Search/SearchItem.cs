using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchItem : MonoBehaviour, IPointerClickHandler {

    [SerializeField] private Text артикул;
    public string Артикул {
        get {return артикул.text; }
        set {артикул.text = value; }
    }

    [SerializeField] private Text тон;
    public string Тон {
        get { return тон.text; }
        set { тон.text = value; }
    }
    
    [SerializeField] private Text количество;
    public string Количество {
        get { return количество.text; }
        set { количество.text = value; }
    }

    public void OnPointerClick(PointerEventData eventData) {
        DataManager.instance.SearchResult = Артикул;
        MainManager.instance.OpenPanel(Enums.Panels.RESULT);
    }
}
