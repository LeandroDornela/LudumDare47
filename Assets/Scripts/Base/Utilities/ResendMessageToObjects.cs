/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 06/2020
//************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Messages
{
    public bool TakeDamage;
    public bool ResetObject;
    public bool Kill;
}


public class ResendMessageToObjects : MonoBehaviour
{
    public GameObject[] objectsToReceiveMessages;

    public Messages messages;

    public SendMessageOptions messageOptions;


    public void TakeDamage(float value)
    {
        if(messages.TakeDamage)
        {
            for(int i = 0; i < objectsToReceiveMessages.Length; i++)
            {
                objectsToReceiveMessages[i].SendMessage("TakeDamage", value, messageOptions);
            }
        }
    }


    public void ResetObject(bool value)
    {
        if (messages.ResetObject)
        {
            for (int i = 0; i < objectsToReceiveMessages.Length; i++)
            {
                objectsToReceiveMessages[i].SendMessage("ResetObject", value, messageOptions);
            }
        }
    }


    public void Kill(bool value)
    {
        if (messages.Kill)
        {
            for (int i = 0; i < objectsToReceiveMessages.Length; i++)
            {
                objectsToReceiveMessages[i].SendMessage("Kill", value, messageOptions);
            }
        }
    }
}
