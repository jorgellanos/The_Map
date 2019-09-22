using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AccessAPI : MonoBehaviour
{
    // Array que guarda strings
    public string[] items;
    // url de la base de datos MySql, Principal
    public string url = "http://localhost:8081/VarasAPP/VarasMapSalas.php";

    // Singleton
    public static AccessAPI Instance { get; private set; }

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
            byte[] results = www.downloadHandler.data;
        }
        //Crear string que guarde los elementos de la pagina
        string datacollector = www.downloadHandler.text;
        //meter los elementos en el Array separados por un ;
        items = datacollector.Split(';');
    }

    // Sacar parametros separados de los elementos
    public string getDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }
}
