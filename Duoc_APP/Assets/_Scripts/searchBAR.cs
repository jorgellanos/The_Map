using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class searchBAR : MonoBehaviour {

    // reference
    public InputField field;
    public GameObject cam;
    public GameObject object_ed;
    public GameObject pref;
    public Text edif;
    public Text piso;

    // variables/parametros
    private string CharName;
    public string dataValue;
    private GameObject p;
    private int x, z, y;
    
    // Valores por defecto
    void Start () {
        field.text = string.Empty;
        edif.text = "Edificio: \n Antonio Varas";
        piso.text = "Piso: \n 1°";

        // Camara
        x = -85;
        y = -500;
        z = -150;

        // Mostrar resolucion pantalla actual
        Debug.Log(Screen.currentResolution);
    }
    
    public void onSubmit()
    {
        // input text
        CharName = field.text.ToUpper();

        // Buscador 
        foreach (Transform child in object_ed.transform)
        {
            if (CharName != child.name)
            {
                foreach (Transform grandChild in child)
                {
                    if(CharName != grandChild.name)
                    {
                        foreach (Transform greatGrandChild in grandChild)
                        {
                            if (CharName == greatGrandChild.name)
                            {
                                found(greatGrandChild.transform.position, CharName);
                                floorCam(grandChild.transform.position);
                                field.text = string.Empty;
                                GetBuilding(child.name);
                                GetFloor(grandChild.name);
                            }
                        }
                    }
                }
            }
        }
    }

    // Crear punto de referencia arriba de la ubicacion solicitada
    public void found(Vector3 f, string names)
    {
        Destroy(p);
        p = Instantiate(pref, new Vector3(f.x, f.y + 1f, f.z), Quaternion.Euler(90, 0, 0));
    }

    // Set HUD eidificio y piso.
    public void GetFloor(string numPiso)
    {
        piso.text = "Piso: \n" + numPiso;
    }

    public void GetBuilding(string edificio)
    {
        edif.text = "Edificio: \n" + edificio;
    }

    //change camera position
    public void floorCam(Vector3 cord)
    {
        cam.transform.position = new Vector3(cord.x, cord.y + 80, cord.z);
    }
}