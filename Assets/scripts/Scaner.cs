﻿using System;
using System.Collections;
using System.Collections.Generic;
using BarcodeScanner;
using BarcodeScanner.Scanner;
using UnityEngine;
using UnityEngine.UI;
using Wizcorp.Utils.Logger;

public class Scaner : MonoBehaviour
{
    private IScanner BarcodeScanner;
    public Text TextHeader;
    public RawImage Image;
    public AudioSource Audio;
    public static Scaner instance;
    // Disable Screen Rotation on that screen
    void Awake()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        instance = this;

    }

    void Start()
    {
        // Create a basic scanner
        BarcodeScanner = new Scanner();
        BarcodeScanner.Camera.Play();

        // Display the camera texture through a RawImage
        BarcodeScanner.OnReady += (sender, arg) =>
        {
            // Set Orientation & Texture
            Image.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
            Image.transform.localScale = BarcodeScanner.Camera.GetScale();
            Image.texture = BarcodeScanner.Camera.Texture;

            // Keep Image Aspect Ratio
            var rect = Image.GetComponent<RectTransform>();
            var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);
        };

        // Track status of the scanner
        BarcodeScanner.StatusChanged += (sender, arg) =>
        {
            TextHeader.text = "Status: " + BarcodeScanner.Status;
        };
    }

    /// <summary>
    /// The Update method from unity need to be propagated to the scanner
    /// </summary>
    void Update()
    {
        if (BarcodeScanner == null)
        {
            return;
        }
        BarcodeScanner.Update();
    }

    #region UI Buttons

    public void ClickStart()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Start");
            return;
        }

        // Start Scanning
        BarcodeScanner.Scan((barCodeType, barCodeValue) =>
        {
            //BarcodeScanner.Stop();
            TextHeader.text = "Found: " + barCodeType + " / " + barCodeValue;

            if (Audio != null)
            {
                // Feedback
                Audio.Play();
            }


#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
            DataManager.instance.QrResult = barCodeValue;
            ClickStop();
            MainManager.instance.OpenPanel(Enums.Panels.RESULT);
        });
    }

    public void ClickStop()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No valid camera - Click Stop");
            return;
        }

        // Stop Scanning
        BarcodeScanner.Stop();

    }

    public void ClickBack()
    {
        // Try to stop the camera before loading another scene
        StartCoroutine(StopCamera(() =>
        {
            Application.Quit();
        }));
    }

    /// <summary>
    /// This coroutine is used because of a bug with unity (http://forum.unity3d.com/threads/closing-scene-with-active-webcamtexture-crashes-on-android-solved.363566/)
    /// Trying to stop the camera in OnDestroy provoke random crash on Android
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator StopCamera(Action callback)
    {
        // Stop Scanning
        Image = null;
        BarcodeScanner.Destroy();
        BarcodeScanner = null;

        // Wait a bit
        yield return new WaitForSeconds(0.1f);

        callback.Invoke();
    }

    #endregion
}
