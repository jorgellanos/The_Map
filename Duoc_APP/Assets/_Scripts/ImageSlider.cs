using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    public Image[] slides = new Image[3];
    public float changeTime, time;
    public int currentSlide = 0;
    private float timeSinceLast = 3.0f;
    public bool transition, pass, begin, loading;
    public string filePath;

    void Start()
    {
        begin = true;
        transition = true;
        loading = true;
        slides[0].gameObject.SetActive(true);
        slides[1].gameObject.SetActive(false);
        slides[2].gameObject.SetActive(false);
        slides[3].gameObject.SetActive(false);
        
    }

    void Update()
    {
        if (loading)
        {
            StartCoroutine(LoadImage());
        }

        if (!transition)
        {
            Timer();
        }

        if (currentSlide == slides.Length)
        {
            currentSlide = 0;
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
    
    IEnumerator LoadImage()
    {
        slides[0].sprite = ImportImageToSprite(filePath);
        yield return slides[0].sprite;
        loading = false;
        transition = false;
    }
    
    IEnumerator Fading()
    {
        FadeOut(currentSlide);
        begin = true;
        yield return new WaitForSeconds(timeSinceLast);
        if (pass)
        {
            currentSlide += 1;
            pass = false;
        }

        changeTime = time;
        yield return new WaitForSeconds(0.1f);
        FadeIn(currentSlide);
        yield return new WaitForSeconds(1);
        transition = false;
    }
}
