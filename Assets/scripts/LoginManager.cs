using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{

    [SerializeField] private InputField login;

    [SerializeField] private InputField password;

    [SerializeField] private Button loginBtn;

    void Start() {
        PlayerPrefs.SetInt("loggedIn", 0);

        if (PlayerPrefs.HasKey("password") && PlayerPrefs.HasKey("login")){
            login.text = PlayerPrefs.GetString("login");
            password.text = PlayerPrefs.GetString("password");
        }

        loginBtn.onClick.AddListener(LoginListener);
    }

    private void LoginListener()
    {
        if (login.text != string.Empty && password.text != string.Empty )
        {
            StartCoroutine(LoginIEnumerator());
        }
        else
        {
            MessageManager.instance.StartWaitWindow("Одно или несколько полей заполнены не верно");
        }
    }

    private IEnumerator LoginIEnumerator()
    {

        WWWForm form = new WWWForm();
        form.AddField("логин", login.text);
        form.AddField("пароль", password.text);

        UnityWebRequest www = UnityWebRequest.Post("http://www.gallery.kg/api/assistant/вход", form);
        LoaderUI.instance.ShowLoader();
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
            LoaderUI.instance.HideLoaderPanel();
            MessageManager.instance.StartWaitWindow("Проверьте подключение к сети и повторите попытку.");
        }
        else
        {

            try
            {
                Пользователль пользователь = JsonUtility.FromJson<Пользователль>(www.downloadHandler.text);

                DataManager.instance.UserName = пользователь.Имя;
                DataManager.instance.UserID = пользователь.Код;
                PlayerPrefs.SetString("login", login.text);
                PlayerPrefs.SetString("password", password.text);
                PlayerPrefs.SetInt("loggedIn", 1);
                
                LoaderUI.instance.HideLoaderPanel();
                SceneManager.LoadScene("qr_reader");
            }
            catch (Exception e)
            {
                Debug.Log("error " + e.Message);
                LoaderUI.instance.HideLoaderPanel();
                MessageManager.instance.StartWaitWindow("Неверный логин или пароль.");
            }
        }

    }
}
