using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class ResultManager : MonoBehaviour {

    [SerializeField] private GameObject infoRemnantsObj; 

    [SerializeField] private Button matchBtn;
    [SerializeField] private Button doesNotMatchBtn;
    [SerializeField] private Button poorQuality;
    [SerializeField] private Button detail;

    [SerializeField] private Text vendor;

    [SerializeField] private Text notFoundMessage;

    [SerializeField] private RawImage rawImage;

    private void OnEnable() {
        vendor.text = "";
        rawImage.texture = null;
        notFoundMessage.gameObject.SetActive(false);
        rawImage.gameObject.SetActive(true);
        if (DataManager.instance != null) {
            if (!string.IsNullOrEmpty(DataManager.instance.QrResult)) {
                ParseQRData(HttpUtility.UrlDecode(new UriBuilder(DataManager.instance.QrResult).Path));
            }
        }
        matchBtn.onClick.AddListener(MatchClickListener);
        doesNotMatchBtn.onClick.AddListener(DoesNotMatchClickListener);
        poorQuality.onClick.AddListener(PoorQualityClickListener);
        detail.onClick.AddListener(DetailClickListener);
        
        //ParseQRData("https://www.gallery.kg/p3/kg/kgb/8557-08kr");
    }

    private void OnDisable() {
        if (DataManager.instance != null) {
            DataManager.instance.QrResult = null;
            DataManager.instance.SearchResult = null;
            DataManager.instance.Vendor = null;
            DataManager.instance.StoreName = null;
        }
        
        Resources.UnloadUnusedAssets();

        matchBtn.onClick.RemoveListener(MatchClickListener);
        doesNotMatchBtn.onClick.RemoveListener(DoesNotMatchClickListener);
        poorQuality.onClick.RemoveListener(PoorQualityClickListener);
        detail.onClick.RemoveListener(DetailClickListener);
    }

    
    private void MatchClickListener() {
        StartCoroutine(Upload("Соответствует", () => MainMenuManager.instance.QrReader()));
    }

    private void DetailClickListener()
    {
        string url = "";
        if (!string.IsNullOrEmpty(DataManager.instance.QrResult))
        {
            url = DataManager.instance.QrResult.Trim() + "?utm_source=sdk";
        }

        Application.OpenURL(url);
    }

    private void PoorQualityClickListener() {
        StartCoroutine(Upload("Плохое Качество", () => MainMenuManager.instance.QrReader()));
    }

    private void DoesNotMatchClickListener()
    {
        StartCoroutine(Upload("Не Соответствует", () => MainMenuManager.instance.QrReader()));
    }

    private void ParseQRData(string value) {
        vendor.text = value;
        //http://www.gallery.kg/kg/p2/kgopt1/ГОУ00919069
        Regex _regex = new Regex(@"\/([\S]*)\/([\S]*)\/([\S]*)\/([\S\s]*)\/?$");
        var match = _regex.Match(value.Trim());

        if (match.Success){
            string code = match.Groups[4].ToString();
            string store = match.Groups[3].ToString();

            DataManager.instance.StoreName = store;
           
            StartCoroutine(GetImageIEnumerator(code, store));
        }
        else{
            //Ничего не найдено
            NotFoundMessage();
        }

    }


    //Получить остатки
    private IEnumerator GetImageIEnumerator(string code,string store)
    {
        //code = WWW.EscapeURL(code);
        string url = string.Format("https://www.gallery.kg/api/assistant/изображение?магазин={0}&номенклатура={1}", store,code);
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.timeout = 0;
        LoaderUI.instance.ShowLoader();
        ДанныеИзображения r = null;
        yield return www.Send();
        LoaderUI.instance.HideLoaderPanel();
        if (www.isNetworkError){
            MessageManager.instance.StartWaitWindow("Проверьте подключение к сети и повторите попытку.");
        }
        else{
            try
            {
                r = JsonUtility.FromJson<ДанныеИзображения>(www.downloadHandler.text);
                vendor.text = r.Артикул;
                DataManager.instance.Vendor = r.Артикул;
                
            }
            catch (Exception e){
                //Артикул не найден
                Debug.Log("error "+ e.Message);
                NotFoundMessage();
                yield break;
            }
            //Загрузка изображения
            if (r != null) {
                WWW _image = new WWW(r.UrlИзображения);
                yield return _image;
                if (string.IsNullOrEmpty(_image.error)) {
                    if (!string.IsNullOrEmpty(r.Изображение) && r.Изображение != "00000000-0000-0000-0000-000000000000") {
                        rawImage.texture = _image.texture;
                    }
                    else {
                        rawImage.texture = Resources.Load<Texture>("Art/no-image");
                    }
                }
                else {
                    Debug.Log("Не удалось загрузить изображение!");
                    MessageManager.instance.StartWaitWindow("Не удалось загрузить изображение! Ошибка: "+_image.error);
                }
                
            }

        }
    }

    private void NotFoundMessage() {
        notFoundMessage.text = "В данном магазине, запрашиваемого артикула не найдено.\nДля более подробной информации нажмите на кнопку \"Подробно\".";
        notFoundMessage.gameObject.SetActive(true);
        rawImage.gameObject.SetActive(false);
        //MessageManager.instance.StartWaitWindow("В данном магазине, запрашиваемого артикула не найдено.");
    }

    //POST 

    delegate void UploadSuccess();
    IEnumerator Upload(string data,UploadSuccess Success=null)
    {
        LoaderUI.instance.ShowLoader();

        UnityWebRequest www = UnityWebRequest.Get(string.Format("https://www.gallery.kg/api/assistant/проверка-изображения?магазин={0}&номенклатура={1}&результат={2}&userid={3}",
            DataManager.instance.StoreName,
            DataManager.instance.Vendor,
            data,
            DataManager.instance.UserID));
        yield return www.SendWebRequest();
        LoaderUI.instance.HideLoaderPanel();
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            MessageManager.instance.StartWaitWindow("Не удалось отправить данные на сервер.\n "+ www.error);
            yield break;
        }
        else
        {
            if (Success != null) {
                Success();
            }
        }
        
    }
}
