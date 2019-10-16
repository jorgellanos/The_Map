using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSlider : MonoBehaviour
{
    public Texture2D[] slides = new Texture2D[9];
    public float changeTime = 10.0f;
    private int currentSlide = 0;
    private float timeSinceLast = 1.0f;

    void Start()
    {
        transform.position = new Vector3(0.5f, 0.5f, 0.0f);
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GetComponent<GUITexture>().texture = slides[currentSlide];
        GetComponent<GUITexture>().pixelInset = new Rect(-slides[currentSlide].width / 2.0f, -slides[currentSlide].height / 2.0f, slides[currentSlide].width, slides[currentSlide].height);
        currentSlide++;
    }

    void Update()
    {
        Debug.Log(currentSlide);

        if (currentSlide == 3)
        {
            currentSlide = 0;
        }

        if (timeSinceLast > changeTime)
        {
            GetComponent<GUITexture>().texture = slides[currentSlide];
            GetComponent<GUITexture>().pixelInset = new Rect(-slides[currentSlide].width / 2.0f, -slides[currentSlide].height / 2.0f, slides[currentSlide].width, slides[currentSlide].height);
            timeSinceLast = 0.0f;
            currentSlide++;
        }
        timeSinceLast += Time.deltaTime;


    }
}
