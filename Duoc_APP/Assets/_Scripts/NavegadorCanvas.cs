using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavegadorCanvas : MonoBehaviour
{
    public GameObject MenuInicio; //principal
    public GameObject PanelMapas; //mapas
    


    private Animator menuAnim;//referencia MenuInicio
    private Animator panelMapasAnim; //referencia PanelMapas



    private void Awake()
    {
        menuAnim = MenuInicio.GetComponent<Animator>();
        panelMapasAnim = PanelMapas.GetComponent<Animator>();
        //PanelMapas.SetActive(false); ya no hace falta
        
    }

    public void Mapas()
    {
        PanelMapas.SetActive(true);  //se avilita
        //sacar menu principal -- al hacer click en mapas
        menuAnim.SetBool("Close", true);
        panelMapasAnim.SetBool("Open", true);


        //traer emnu de mapas al hacer click mapas y en mapas un boton volver para volver
        //
    }

    public void Principal()
    {
        MenuInicio.SetActive(true);
        panelMapasAnim.SetBool("Close", true);
        menuAnim.SetBool("Open", true);
    }

}
