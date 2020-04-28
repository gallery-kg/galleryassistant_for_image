using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public static MainMenuManager instance;

    [SerializeField] private GameObject panel;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        panel.GetComponent<EventsHandler>().OnPointerClickEvent += ClickOnPanel;
    }

    private void ClickOnPanel() {
        HidePanel();
    }

    public void ShowPanel() {
        panel.SetActive(true);
    }

    public void HidePanel() {
        panel.SetActive(false);
    }

    public bool MenuIsActive() {
        return panel.activeSelf;
    }

    public void Logout() {
        SceneManager.LoadScene("login");
    }

    public void AppQuit() {
        //Application.Quit();
        SimpleDemo.instance.ClickBack();
    }

    public void Settings() {
        MainManager.instance.OpenPanel(Enums.Panels.SETTINGS);
    }

    public void Search()
    {
        MainManager.instance.OpenPanel(Enums.Panels.SEARCH);
    }

    public void QrReader() {
        SimpleDemo.instance.ClickStart();
        //EasyCodeScanner.launchScanner(false, "Scanning...", 64, false);
        MainManager.instance.OpenPanel(Enums.Panels.QR_READER);
    }
}
