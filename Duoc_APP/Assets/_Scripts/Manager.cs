using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject[] Canvases;
    public string filePath;

    // Start is called before the first frame update
    void Start()
    {
        ReadJson();
        Canvases[0].gameObject.SetActive(true);
        Canvases[1].gameObject.SetActive(false);
        Canvases[2].gameObject.SetActive(false);
        Canvases[3].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadJson()
    {
        using (StreamReader r = new StreamReader(filePath))
        {
            string json = r.ReadToEnd();
            Persona p = JsonUtility.FromJson<Persona>(json);
            Debug.Log(json);
        }
    }
}

[System.Serializable]
public class Persona
{
    public string[] Layers;
}
