using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QrPanel : MonoBehaviour {
    public static QrPanel instance;

    public RawImage Image;
    private float RestartTime;

    // Disable Screen Rotation on that screen
    void Awake()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        instance = this;
    }

    void Start()
    {
        LoaderUI.instance.ShowLoader();
        //Обнуляем переменные с результатом
        DataManager.instance.QrResult = null;
        DataManager.instance.SearchResult = null;
    }



    public void LoadResult(){
        SceneManager.LoadScene("result");
    }

    public void LoadLoginScene() {
        SceneManager.LoadScene("login");
    }


}
