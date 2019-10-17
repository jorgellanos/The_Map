using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject[] Canvases;

    // Start is called before the first frame update
    void Start()
    {
        Canvases[0].gameObject.SetActive(true);
        Canvases[1].gameObject.SetActive(false);
        Canvases[2].gameObject.SetActive(false);
        Canvases[3].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
