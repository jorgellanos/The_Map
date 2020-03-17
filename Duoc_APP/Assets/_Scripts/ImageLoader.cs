using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public Image[] array;
    public string folderPath;
    public int imgNum;

    // Start is called before the first frame update
    void Start()
    {
        imgNum = Directory.GetFiles(folderPath).Length;
        array = new Image[imgNum];
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.LogError("File not exists! " + filePath);
            rec = new Rect(0, 0, 0, 0);
        }

        return Sprite.Create(tex, rec, new Vector2(0.5f, 0.5f), 100);
    }

    public void LoadImageToArray()
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = GameObject.Find("Image (" + i + ")").GetComponent<Image>();
            array[i].sprite = ImportImageToSprite("");
        }
    }
}
