using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AraCanvasYonlendirme : MonoBehaviour
{
    string IconNo;
    int age;
    // Start is called before the first frame update
    void Start()
    {
       IconNo = PlayerPrefs.GetString("tagBilgi");
        age = PlayerPrefs.GetInt("Age");
    }

    public void Yonlendirme()
    {
        switch (age)
        {
            case 4:
                switch (IconNo)
                {
                    case "Icon1":
                        Debug.Log("4-1");
                        break;
                    case "Icon2":
                        Debug.Log("4-2");
                        break;
                    case "Icon3":
                        Debug.Log("4-3");
                        break;
                    case "Icon4":
                        Debug.Log("4-4");
                        break;
                }
                break;
            case 5:
                switch (IconNo)
                {
                    case "Icon1":
                        Debug.Log("5-1");
                        break;
                    case "Icon2":
                        Debug.Log("5-2");
                        break;
                    case "Icon3":
                        Debug.Log("5-3");
                        break;
                    case "Icon4":
                        Debug.Log("5-4");
                        break;
                }
                break;
            case 6:
                switch (IconNo)
                {
                    case "Icon1":
                        Debug.Log("6-1");
                        break;
                    case "Icon2":
                        Debug.Log("6-2");
                        break;
                    case "Icon3":
                        Debug.Log("6-3");
                        break;
                    case "Icon4":
                        Debug.Log("6-4");
                        break;
                }
                break;
            case 7:
                switch (IconNo)
                {
                    case "Icon1":
                        Debug.Log("7-1");
                        break;
                    case "Icon2":
                        Debug.Log("7-2");
                        break;
                    case "Icon3":
                        Debug.Log("7-3");
                        break;
                    case "Icon4":
                        Debug.Log("7-4");
                        break;
                }
                break;
            case 8:
                switch (IconNo)
                {
                    case "Icon1":
                        Debug.Log("8-1");
                        break;
                    case "Icon2":
                        Debug.Log("8-2");
                        break;
                    case "Icon3":
                        Debug.Log("8-3");
                        break;
                    case "Icon4":
                        Debug.Log("8-4");
                        break;
                }
                break;
            case 9:
                switch (IconNo)
                {
                    case "Icon1":
                        Debug.Log("9-1");
                        break;
                    case "Icon2":
                        Debug.Log("9-2");
                        break;
                    case "Icon3":
                        Debug.Log("9-3");
                        break;
                    case "Icon4":
                        Debug.Log("9-4");
                        break;
                }
                break;


        }
    }

    
}
