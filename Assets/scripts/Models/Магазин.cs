using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Магазин {

    public string Наименование;
    public string БуквенныйКод;

}
[System.Serializable]
public class _СписокМагазинов {
    public int КодОтвета;
    public Магазин[] СписокМагазинов;
}
