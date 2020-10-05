/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 02/2020
//************************************************************/


using System.Collections.Generic;
using UnityEngine;
using GameEventListener;


/// <summary>
/// [pt-BR] - Evento scriptavel de proposito geral.
/// </summary>
[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Event System/Game Event")]
public class GameEvent : ScriptableObject
{
    [TextArea(3, 5)] [SerializeField] private string description = "";

    [SerializeField] private bool debugLog = false;

    // Obejetos que escutam a variavel e reagem quando o evento é acionado.
    private List<Listener> listeners = new List<Listener>();


    /// <summary>
    /// [pt-BR] - Informa aos listeners do evento que ele foi ativado.
    /// </summary>
    public void Raise()
    {
        if(debugLog) Debug.Log("[en-US] - Raise event: " + this.name);

        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRised();
        }
    }


    /// <summary>
    /// [pt-BR] - Registra um listener para ouvir o evento.
    /// </summary>
    /// <param name="listener">Listener a ser adicionado.</param>
    public void RegisterListener(Listener listener)
    {
        listeners.Add(listener);
    }


    /// <summary>
    /// [pt-BR] - Remove um listener da lista.
    /// </summary>
    /// <param name="listener">Listener a ser adicionado.</param>
    public void UnregisterListener(Listener listener)
    {
        listeners.Remove(listener);
    }
}
