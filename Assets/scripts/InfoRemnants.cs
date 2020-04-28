using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoRemnants : MonoBehaviour {
    [SerializeField] private Text remnants;
    [SerializeField] private Text part;

    public float Remnants {
        set { remnants.text = value.ToString(); }
    }

    public string Part{
        set { part.text = value; }
    }
}
