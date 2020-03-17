using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    public Image[] slides;
    public float changeTime, time;
    public int imgNum, currentSlide = 0;
    private float timeSinceLast = 5.0f;
    public bool transition, pass, begin, loading;
    public string filePath, folderPath;
    public AccessAPI api;

    void Awake()
    {
        imgNum = api.paths.Length;
        begin = true;
        transition = true;
        loading = true;
    }

    void Update()
    {
        if (loading)
        {
            for (int i = 1; i < imgNum + 1; i++)
            {
                if (i == imgNum)
                {
                    slides[1].gameObject.SetActive(true);
                    transition = false;
                    loading = false;
                }
                else
                {
                    filePath = api.paths[i].Substring(2).Replace("\"","").Trim();
                    slides[i] = GameObject.Find("FotoCarrusel (" + i + ")").GetComponent<Image>();
                    slides[i].sprite = ImportImageToSprite(filePath);
                    slides[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (currentSlide > imgNum)
            {
                currentSlide = 0;
            }

            if (currentSlide < 0)
            {
                currentSlide = imgNum;
            }

            if (!transition)
            {
                Timer();
            }
        }

        slides[imgNum].gameObject.SetActive(true);

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
        return Sprite.Create(tex, rec, new Vector2(1f, 1f), 100);
    }

    public void ChangeImage(int next)
    {
        slides[currentSlide].gameObject.SetActive(false);
        currentSlide += next;
        if (currentSlide == imgNum)
        {
            currentSlide = 1;
        }

        if (currentSlide <= 0)
        {
            currentSlide = imgNum - 1;
        }
        slides[currentSlide].gameObject.SetActive(true);
        changeTime = time;
    }

    public void Timer()
    {
        changeTime -= 1 * Time.deltaTime;
        if (changeTime <= 0)
        {
            transition = false;
            pass = true;
            changeTime = 0;

            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading()
    {
        begin = true;
        yield return new WaitForSeconds(timeSinceLast);
        slides[currentSlide].gameObject.SetActive(false);
        if (pass)
        {
            if (currentSlide < imgNum)
            {
                currentSlide += 1;
                Debug.Log(currentSlide + " of " + imgNum);
            }

            if (currentSlide == imgNum)
            {
                currentSlide = 1;
            }

            if (currentSlide <= 0)
            {
                currentSlide = imgNum - 1;
            }
            pass = false;
        }
        slides[currentSlide].gameObject.SetActive(true);
        changeTime = time;
        transition = false;
    }
}
