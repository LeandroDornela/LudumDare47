using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VariableFloatToText : MonoBehaviour
{
    [SerializeField] private VariableFloat variable;

    [SerializeField] private Text text;


    // Update is called once per frame
    void Update()
    {
        text.text = variable.Value().ToString();
    }
}
