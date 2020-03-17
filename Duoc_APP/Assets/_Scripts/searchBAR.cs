using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class searchBAR : MonoBehaviour {

    // reference
    public InputField field;
    public GameObject pref;

    // variables/parametros
    private string CharName;
    public string dataValue;
    private GameObject p;

    // ALERT
    public GameObject alert;

    // Referencias
    public GameObject matriz, panel, Varas, Froebel, Torre, Casona, buscador, ui1, ui2, ui3;

    public GameObject[] Varaspisos, FroPisos, TorPisos, CasPisos;
    // Valores por defecto
    void Start () {
        field.text = string.Empty;
    }

    public void onSubmit()
    {
        // input text
        CharName = field.text.ToUpper();
        if (CharName.Length == 4)
        {
            SetMap(CharName);
        }
        else
        {
            alert.SetActive(true);
        }
        
    }

    // Crear punto de referencia arriba de la ubicacion solicitada
    public void Found(GameObject sala, float size)
    {
        RectTransform salaT = sala.GetComponent<RectTransform>();
        Text salaX = sala.GetComponentInChildren<Text>();
        Destroy(p);
        p = Instantiate(pref, sala.transform.position, Quaternion.Euler(0, 0, 0));
        p.transform.SetParent(sala.transform);
        p.transform.position = sala.transform.position;
        p.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        p.GetComponentInChildren<Text>().text = sala.GetComponent<Text>().text;
        p.GetComponentInChildren<Text>().fontSize = sala.GetComponent<Text>().fontSize;
        p.GetComponentInChildren<RectTransform>().sizeDelta = sala.GetComponent<RectTransform>().sizeDelta;
    }

    public void SetMap(string name)
    {
        string ed = name.Substring(0, 1);
        float p = float.Parse(name.Substring(1,1));
        float w;
        bool result = float.TryParse(ed, out w);
        Debug.Log("resultado: " + result);
        if (!result)
        {
            switch (ed)
            {
                case "V":
                    Varas.SetActive(true);
                    Froebel.SetActive(false);
                    Torre.SetActive(false);
                    Casona.SetActive(false);
                    alert.SetActive(false);
                    ui1.SetActive(true);
                    ui2.SetActive(true);
                    ui3.SetActive(true);
                    panel.SetActive(true);
                    matriz.SetActive(false);
                    if (p < 5 && p > -1)
                    {
                        Order(Varaspisos, p, 90, name);
                    }
                    else
                    {
                        Order(Varaspisos, 1, 0, name);
                    }
                    break;

                case "T":
                    Varas.SetActive(false);
                    Froebel.SetActive(false);
                    Torre.SetActive(true);
                    Casona.SetActive(false);
                    alert.SetActive(false);
                    ui1.SetActive(true);
                    ui2.SetActive(true);
                    ui3.SetActive(true);
                    panel.SetActive(true);
                    matriz.SetActive(false);
                    if (p < 6 && p > 2)
                    {
                        Order(TorPisos, p, 90, name);
                    }
                    else
                    {
                        Order(TorPisos, 5, 0, name);
                    }
                    break;

                case "C":
                    Varas.SetActive(false);
                    Froebel.SetActive(false);
                    Torre.SetActive(false);
                    Casona.SetActive(true);
                    alert.SetActive(false);
                    ui1.SetActive(true);
                    ui2.SetActive(true);
                    ui3.SetActive(true);
                    panel.SetActive(true);
                    matriz.SetActive(false);
                    if (p == 1)
                    {
                        Order(CasPisos, p, 180, name);
                    }
                    else
                    {
                        Order(CasPisos, 1, 0, name);
                    }
                    break;

                case "F":
                    Varas.SetActive(false);
                    Froebel.SetActive(true);
                    Torre.SetActive(false);
                    Casona.SetActive(false);
                    alert.SetActive(false);
                    ui1.SetActive(true);
                    ui2.SetActive(true);
                    ui3.SetActive(true);
                    panel.SetActive(true);
                    matriz.SetActive(false);
                    if (p < 3 && p > 0)
                    {
                        Order(FroPisos, p, 90, name);
                    }
                    else
                    {
                        Order(FroPisos, 1, 0, name);
                    }
                    break;

                default:
                    alert.SetActive(true);
                    break;
            }
        }
        else
        {
            alert.SetActive(true);
        }    
    }

    public void Order(GameObject[] list, float piso, float siz, string nombre)
    {
        foreach (GameObject p in list)
        {
            p.SetActive(false);
            if (float.Parse(p.name.Substring(0 ,1)) == piso)
            {
                p.SetActive(true);
                buscador.SetActive(false);
                GameObject sala = GameObject.Find(nombre);
                Found(sala, siz);
                field.text = "";
            }
        }
    }
}