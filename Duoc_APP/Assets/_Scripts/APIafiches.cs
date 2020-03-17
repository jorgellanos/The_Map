using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIafiches : MonoBehaviour
{
    public GameObject slider;
    public bool load;
    public int saveNum = 0;
    // Array que guarda strings
    public string[] items;
    public string[] paths;
    // url de la base de datos
    public string url = "http://localhost:3000/afiches";

    // Singleton
    public static APIafiches Instance { get; private set; }

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

    private void Update()
    {
        if (load)
        {
            StartSlider();
        }
        else
        {
            return;
        }
    }

    // Sacar parametros separados de los elementos
    public string getDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(","))
        {
            value = value.Remove(value.IndexOf(","));
        }
        return value;
    }

    public void getPaths()
    {
        for (int i = 1; i < items.Length; i++)
        {
            string path = getDataValue(items[i], "img_afiche");
            paths[saveNum] = path;
            saveNum++;
            if (saveNum == items.Length - 1)
            {
                load = true;
                Debug.Log("BEGIN!!!!");
            }
        }
    }

    public void StartSlider()
    {
        slider.SetActive(true);
        load = false;
    }
}
