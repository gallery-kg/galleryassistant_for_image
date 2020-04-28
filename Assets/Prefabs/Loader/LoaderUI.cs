using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoaderUI : MonoBehaviour {

    public static LoaderUI instance;
    [SerializeField] private GameObject loaderPanel;
    [SerializeField] private GameObject bottomPanel;
    private int showCounter = 0;
    private void Awake() {
        instance = this;
        showCounter = 0;
    }

    public void ShowLoader() {
        loaderPanel.SetActive(true);
        showCounter++;
    }

    public void HideLoaderPanel() {
        showCounter--;
        if (showCounter <= 0) {
            loaderPanel.SetActive(false);
        }
    }

    //Для загрузки всех остатков
    public void ShowBottomPanel() {
        bottomPanel.SetActive(true);
    }

    public void HideBottomPanel() {
        bottomPanel.SetActive(false);
    }

}
