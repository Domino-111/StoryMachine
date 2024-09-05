using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public Text display;

    public string newText;

    public string newTextB;

    public char searchCharacter;

    void Start()
    {
        //Set the text value of the text component to the string we provide
        display.text = newText + " " + newTextB;

        //Check if the string contains our search character
        if (display.text.Contains(searchCharacter))
        {
            display.text += " This does contain the character '" + searchCharacter + "'";
        }

        else
        {
            display.text += " This does NOT contain the character '" + searchCharacter + "'";
        }
    }

    void Update()
    {
        
    }
}
