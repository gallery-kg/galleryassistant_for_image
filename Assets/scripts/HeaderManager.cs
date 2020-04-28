using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeaderManager : MonoBehaviour {
    public static HeaderManager instance;

    [SerializeField] private Text info;
    public string Info {
        set { info.text = value; }
    }

    //[SerializeField] private Button backBtn;
    [SerializeField] private Button menuBtn;
    [SerializeField] private string loadScene;
    void Awake () {
	    instance = this;
	}

    void Start() {
        //backBtn.onClick.AddListener(BackClickListener);
        menuBtn.onClick.AddListener(MenuClickListener);
        UpdateInfo(DataManager.instance.UserName);
    }

    private void OnDestroy() {
        menuBtn.onClick.RemoveListener(MenuClickListener);
    }

    private void MenuClickListener() {
        if (MainMenuManager.instance.MenuIsActive()) {
            MainMenuManager.instance.HidePanel();
        }
        else {
            MainMenuManager.instance.ShowPanel();
        }
        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (MainManager.instance.ActivePanel(Enums.Panels.QR_READER)) {
                SimpleDemo.instance.ClickStop();
                MainManager.instance.ClouseAllPanels();
            }
            else {
                MainMenuManager.instance.QrReader();
            }
            
            
        }
    }

    public void UpdateInfo(string userName) {
        Info = userName;
    }

    public void UpdateInfo()
    {
        Info = DataManager.instance.UserName;
    }
}
