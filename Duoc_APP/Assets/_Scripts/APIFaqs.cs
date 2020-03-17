using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIFaqs : MonoBehaviour
{
    public GameObject slider;

    // Array que guarda strings
    public string[] items;
    public string[] paths;
    // url de la base de datos MySql, Principal
    public string url = "http://localhost:3000/faqs";

    // Singleton
    public static APIFaqs Instance { get; private set; }

    // Use this for initialization
    private void Awake()
    {
        //Destruir cualquier objeto que sea copia de este.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private IEnumerator Start()
    {
        // conectar con la base de datos y sacar la informacion de la pagina
        UnityWebRequest www = UnityWebRequest.Get(url);

        //Esperar respuesta 
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }
        //Crear string que guarde los elementos de la pagina
        string datacollector = www.downloadHandler.text;
        //meter los elementos en el Array separados por un ;
        items = datacollector.Split('{');
        //sacar las rutas de las imagenes y agregarlas a un array
        yield return items;
        paths = new string[items.Length];
        getPaths();

    }

    // Sacar parametros separados de los elementos
    public string getDataValue(string data, string index)
    {
        Debug.Log("lengths: " + data.Length + " " + data.IndexOf(index) + " " + index.Length);
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(","))
        {
            value = value.Remove(value.IndexOf(","));
        }
        Debug.Log("Result: " + value);
        return value;
    }

    public void getPaths()
    {
        for (int i = 0; i < items.Length; i++)
        {
            string path = getDataValue(items[i + 1], "pregunta");
            string path2 = getDataValue(items[i + 1], "respuesta");
            paths[i] = path;
            paths[i + 1] = path2;
            Debug.Log(i + "/" + items.Length);
            if (i >= items.Length)
            {
                slider.SetActive(true);
            }
        }
    }
}
