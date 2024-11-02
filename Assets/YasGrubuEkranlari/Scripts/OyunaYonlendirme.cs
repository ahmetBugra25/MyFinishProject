using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OyunaYonlendirme : MonoBehaviour,IPointerClickHandler
{
    string tagInfo; // Image'in etiket bilgisini saklamak için bir deðiþken

    [System.Serializable]
    public class Tag
    {
        public string tag;

        public Tag(string _tag)
        {
            tag = _tag;
        }
    }
    // Ýmaj týklandýðýnda çaðrýlacak fonksiyon
    public void OnPointerClick(PointerEventData eventData)
    {
        tagInfo = gameObject.tag;
        string tagKayit = tagInfo;
        Tag tagBilgi = new Tag(tagKayit);
        Debug.Log("Tag Bilgisi: " + tagInfo); // Etiket bilgisini konsola yazdýr
        PlayerPrefs.SetString("tagBilgi", tagBilgi.tag);
    }
}
