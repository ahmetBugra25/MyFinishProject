using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class InternetControl : MonoBehaviour
{
     // �nternet durumunu g�stermek i�in bir UI Text ��esi
    public bool isChecking = false;

    public void KontrolYap()
    {
        // Coroutine ba�lat
        StartCoroutine(CheckInternetRoutine());
    }

    IEnumerator CheckInternetRoutine()
    {
        while (true)
        {
            // �nternet ba�lant�s�n� kontrol et
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
            // Burada kullan�c�ya uyar� verebilir veya ba�ka bir i�lem yapabilirsin
        }
    }
}
