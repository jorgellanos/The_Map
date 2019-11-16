using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject[] Canvases, fond;
    public string filePath, backgroundPath;
    public bool loading;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("http://localhost:3000/afiches"));
        loading = true;
        ReadJson();
        Canvases[0].gameObject.SetActive(true);
        Canvases[1].gameObject.SetActive(true);
        Canvases[2].gameObject.SetActive(true);
        Canvases[3].gameObject.SetActive(true);
        fond = GameObject.FindGameObjectsWithTag("fondo");
    }

    // Update is called once per frame
    void Update()
    {
        if (loading)
        {
            LoadBackground();
        }
        else
        {
            return;
        }
    }

    public void LoadBackground()
    {
        for (int i = 0; i < Canvases.Length + 1; i++)
        {
            if (i >= Canvases.Length)
            {
                loading = false;
                Canvases[0].gameObject.SetActive(true);
            }
            else
            {
                fond[i].gameObject.GetComponentInChildren<Image>().sprite
                    = ImportImageToSprite(backgroundPath + i + ".jpg");
                Canvases[i].gameObject.SetActive(false);
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

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}

[System.Serializable]
public class Persona
{
    public string[] Layers;
}
