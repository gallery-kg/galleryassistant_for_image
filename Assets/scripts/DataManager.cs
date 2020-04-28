using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {
    private static string path = "";

    private string qrResult;
    
    public string QrResult {
        get { return qrResult; }
        set {
            qrResult = value; 
        }
    }

    private string vendor;
    public string Vendor {
        get { return vendor; }
        set {
            vendor = value; 
        }
    }

    private string searchResult;
    public string SearchResult
    {
        get { return searchResult; }
        set
        {
            searchResult = value;
        }
    }


    private string userName;

    public string UserName
    {
        get { return PlayerPrefs.GetString("userName", ""); }
        set{
            PlayerPrefs.SetString("userName",value);
        }
    }

    public string UserID
    {
        get { return PlayerPrefs.GetString("userID", ""); }
        set
        {
            PlayerPrefs.SetString("userID", value);
        }
    }

    public string StoreName {
        get { return PlayerPrefs.GetString("storeName",""); }
        set{
            PlayerPrefs.SetString("storeName", value);
        }
    }


    public bool LoggedIn {
        get { return PlayerPrefs.GetInt("loggedIn", 0) > 0; }
    }


    public static DataManager instance;

    void Awake () {
		DontDestroyOnLoad(this);
        instance = this;
        Screen.fullScreen = false;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //Необходимая мера без этого телефон не запрашивает разрешения на использование камеры
        WebCamTexture tex = new WebCamTexture();
        //----------------------------------------------

    }

}
