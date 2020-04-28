using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainManager : MonoBehaviour {
    [System.Serializable]
    protected class Panel {
        public Enums.Panels name;
        public GameObject panel;
    }
    public static MainManager instance;

    [SerializeField] private Panel[] panels;
    

    private void Awake() {
        instance = this;
    }

    private void Start() {
        
    }

    public void OpenPanel(Enums.Panels name) {
        ClouseAllPanels();

        panels.First(p => p.name == name).panel.SetActive(true);
    }

    public void ClouseAllPanels() {
        foreach (Panel panel in panels.Where(p=>p.panel.activeSelf)) {
            panel.panel.SetActive(false);
        }
        MainMenuManager.instance.HidePanel();
    }

    public bool ActivePanel(Enums.Panels name) {
        return panels.First(p => p.name == name).panel.activeSelf;
    }

}
