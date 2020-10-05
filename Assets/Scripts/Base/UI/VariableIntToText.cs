using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VariableIntToText : MonoBehaviour
{
    [SerializeField] private VariableInt variable;

    [SerializeField] private Text text;


    // Update is called once per frame
    void Update()
    {
        text.text = variable.Value().ToString();
    }
}
