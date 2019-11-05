using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject[] Canvases;
    public string filePath, backgroundPath;
    public bool loading;

    // Start is called before the first frame update
    void Start()
    {
        loading = true;
        ReadJson();
        Canvases[0].gameObject.SetActive(true);
        Canvases[1].gameObject.SetActive(false);
        Canvases[2].gameObject.SetActive(false);
        Canvases[3].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (loading)
        {
            LoadBackground();
        }
    }

    public void LoadBackground()
    {
        for (int i = 0; i < Canvases.Length; i++)
        {
            if (i >= Canvases.Length)
            {
                loading = false;
            }
            else
            {
                Canvases[i].gameObject.transform.Find("Fondos").GetComponent<Image>().sprite
                    = ImportImageToSprite(backgroundPath + i + ".jpg");
            }
        }
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

    public Sprite ImportImageToSprite(string filePath)
    {
        Texture2D tex = null;
        Rect rec;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            Debug.Log("File Exists!");
            rec = new Rect(0, 0, tex.width, tex.height);
        }
        else
        {
            Debug.LogError("File not exists!");
            rec = new Rect(0, 0, 0, 0);
        }

        return Sprite.Create(tex, rec, new Vector2(0.5f, 0.5f), 100);
    }
}

[System.Serializable]
public class Persona
{
    public string[] Layers;
}
