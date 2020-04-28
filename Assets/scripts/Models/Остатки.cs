using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ОстаткиНоменклатуры {

    public int КодОтвета;
    public string Код;
    public string Магазин;
    public string Артикул;
    public string ЕдиницаХраненияОстатков;
    public Остатки[] Остатки;
}

[System.Serializable]
public class ОстаткиМагазина
{

    public int КодОтвета;
    public ОстаткиНоменклатуры[] Остатки;
}

[System.Serializable]
public class Остатки {
    public string Характеристика;
    public float СвободныйОстаток;
}
