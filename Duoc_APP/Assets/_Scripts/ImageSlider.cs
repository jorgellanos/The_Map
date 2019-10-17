using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    public Image[] slides = new Image[3];
    public float changeTime, time;
    public int currentSlide = 0;
    private float timeSinceLast = 3.0f;
    public bool transition, pass;

    void Start()
    {
        transition = false;
        slides[0].gameObject.SetActive(true);
        slides[1].gameObject.SetActive(false);
        slides[2].gameObject.SetActive(false);
        slides[3].gameObject.SetActive(false);
    }

    void Update()
    {
        if (!transition)
        {
            Timer();
        }
    }

    public void FadeOut(int current)
    {
        Color temp = slides[current].color;
        if (temp.a > 0)
        {
            temp.a -= 2f * Time.deltaTime;
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
        temp.a = 0;

        if (!slides[current].gameObject.activeSelf)
        {
            slides[current].gameObject.SetActive(true);
        }
        
        if (temp.a < 1)
        {
            temp.a += 1f * Time.deltaTime;
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
        yield return new WaitForSeconds(timeSinceLast);
        if (pass)
        {
            currentSlide += 1;
            pass = false;
        }

        changeTime = time;
        
        
        if (currentSlide > slides.Length)
        {
            currentSlide = 0;
        }
        yield return new WaitForSeconds(0.1f);
        FadeIn(currentSlide);
        yield return new WaitForSeconds(1);
        transition = false;
    }
}
