using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloading : MonoBehaviour {

    private IEnumerator Start() {
        yield return new WaitForEndOfFrame();
        if (DataManager.instance.LoggedIn) {
            SceneManager.LoadScene("qr_reader");
        }
        else {
            SceneManager.LoadScene("login");
        }
        
    }

}
