using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OyunaYonlendirme : MonoBehaviour,IPointerClickHandler
{
    string tagInfo; // Image'in etiket bilgisini saklamak i�in bir de�i�ken

    [System.Serializable]
    public class Tag
    {
        public string tag;

        public Tag(string _tag)
        {
            tag = _tag;
        }
    }
    // �maj t�kland���nda �a�r�lacak fonksiyon
    public void OnPointerClick(PointerEventData eventData)
    {
        tagInfo = gameObject.tag;
        string tagKayit = tagInfo;
        Tag tagBilgi = new Tag(tagKayit);
        Debug.Log("Tag Bilgisi: " + tagInfo); // Etiket bilgisini konsola yazd�r
        PlayerPrefs.SetString("tagBilgi", tagBilgi.tag);
    }
}
