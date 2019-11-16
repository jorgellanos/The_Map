using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    public Image[] slides;
    public float changeTime, time;
    public int imgNum, count, currentSlide = 0;
    private float timeSinceLast = 3.0f;
    public bool transition, pass, begin, loading;
    public string filePath;

    void Start()
    {
        begin = true;
        transition = true;
        imgNum = Directory.GetFiles("C:/Im").Length;
        slides = new Image[imgNum];
        loading = true;
    }

    void Update()
    {
        if (loading)
        {
            for (int i = 0; i < imgNum + 1; i++)
            {
                if (i >= imgNum)
                {
                    slides[0].gameObject.SetActive(true);
                    transition = false;
                    loading = false;
                }
                else
                {
                    slides[i] = GameObject.Find("Image (" + i + ")").GetComponent<Image>();
                    slides[i].sprite = ImportImageToSprite(filePath + i + ".jpg");
                    slides[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (currentSlide >= imgNum)
            {
                currentSlide = 0;
            }

            if (!transition)
            {
                Timer();
            }
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
            Debug.LogError("File not exists! " + filePath);
            rec = new Rect(0,0,0,0);
        }
        
        return Sprite.Create(tex, rec, new Vector2(0.5f, 0.5f), 100);
    }

    public void FadeOut(int current)
    {
        Color temp = slides[current].color;
        if (temp.a > 0)
        {
            temp.a -= 0.5f * Time.deltaTime;
            slides[current].color = temp;
        }
        else
        {
            slides[current].gameObject.SetActive(false);
        }
    }

    public void FadeIn(int current)
    {
        Color temp = slides[current].color;

        if (begin)
        {
            begin = false;
            temp.a = 0;
        }
        if (!slides[current].gameObject.activeSelf)
        {
            slides[current].gameObject.SetActive(true);
        }
        if (temp.a < 1)
        {
            temp.a += 0.5f * Time.deltaTime;
            slides[current].color = temp;
        }
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
        FadeOut(currentSlide);
        begin = true;
        yield return new WaitForSeconds(timeSinceLast);
        if (pass)
        {
            if (currentSlide < imgNum)
            {
                currentSlide += 1;
            }
            
            pass = false;
        }

        changeTime = time;
        yield return new WaitForSeconds(0.1f);
        FadeIn(currentSlide);
        yield return new WaitForSeconds(1);
        transition = false;
    }
}
