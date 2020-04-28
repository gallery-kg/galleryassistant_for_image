using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public static GameObject InstantiateUiObject(Vector3 position, GameObject o, Transform parrent)
    {
        GameObject obj = Instantiate(o);
        obj.transform.SetParent(parrent);
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<RectTransform>().anchoredPosition3D = position;
        return obj;
    }

    public static GameObject Instantiate3dObject(Vector3 position, GameObject o, Transform parrent)
    {
        GameObject obj = Instantiate(o);
        obj.transform.SetParent(parrent);
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<Transform>().position = position;
        return obj;
    }
    public static string GetHtmlFromUri(string resource)
    {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess)
                {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                    {
                        //We are limiting the array to 80 so we don't have
                        //to parse the entire html document feel free to 
                        //adjust (probably stay under 300)
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs)
                        {
                            html += ch;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
        return html;
    }

    public static bool CheckConnectNetworking()
    {
        string HtmlText = Helper.GetHtmlFromUri("http://google.com");
        if (HtmlText == "" || !HtmlText.Contains("schema.org/WebPage"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Возвращает слова в падеже, зависимом от заданного числа 
    /// </summary>
    /// <param name="number">Число от которого зависит выбранное слово</param>
    /// <param name="nominativ">Именительный падеж слова. Например "день"</param>
    /// <param name="genetiv">Родительный падеж слова. Например "дня"</param>
    /// <param name="plural">Множественное число слова. Например "дней"</param>
    /// <returns></returns>
    public static string GetDeclension(int number, string nominativ, string genetiv, string plural)
    {
        number = number % 100;
        if (number >= 11 && number <= 19)
        {
            return plural;
        }

        var i = number % 10;
        switch (i)
        {
            case 1:
                return nominativ;
            case 2:
            case 3:
            case 4:
                return genetiv;
            default:
                return plural;
        }
    }

    // Число последнего запуска прилы

    public static DateTime LastDateOpenApp
    {
        get { return DateTime.Parse(PlayerPrefs.GetString("lastDateOpenApp", DateTime.MinValue.ToString("d"))); }
        set { PlayerPrefs.SetString("lastDateOpenApp", value.ToString("d")); }
    }

}
