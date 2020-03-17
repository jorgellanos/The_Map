using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teclado : MonoBehaviour {

    //variables
    string word = string.Empty;
    public InputField input = null;

    void Update()
    {
        if (input.text == "")
        {
            word = "";
        }
    }

    public void funcion (string letra)
    {
        //agregar letra al inputfield
        word = word + letra;
        input.text = word.ToUpper();
	}

    public void backSpace()
    {
        if(input.text.Length > 0)
        {
            //eliminar/remover ultimo caracter del inputfield
            input.text = input.text.Remove(input.text.Length - 1, 1);
            word = input.text;
        }
    }

    public void Clear()
    {
        input.text = "";
    }
}
