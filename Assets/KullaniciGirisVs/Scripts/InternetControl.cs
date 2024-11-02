using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class InternetControl : MonoBehaviour
{
     // Ýnternet durumunu göstermek için bir UI Text öðesi
    public bool isChecking = false;

    public void KontrolYap()
    {
        // Coroutine baþlat
        StartCoroutine(CheckInternetRoutine());
    }

    IEnumerator CheckInternetRoutine()
    {
        while (true)
        {
            // Ýnternet baðlantýsýný kontrol et
            yield return StartCoroutine(CheckInternetConnection());

        }
    }

    IEnumerator CheckInternetConnection()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://www.google.com");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            isChecking = true;
        }
        else
        {
            isChecking = false;
            // Burada kullanýcýya uyarý verebilir veya baþka bir iþlem yapabilirsin
        }
    }
}
