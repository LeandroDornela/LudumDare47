/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 07/01/2020
//************************************************************/


using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// [pt-BR] - Struct para os eventos de uma variável.
/// </summary>
[System.Serializable]
public struct ValueChangeEvents
{
    [Space]
    [Tooltip("[pt-BR] - Evento a ser disparado quando o valor da variável for modificado.")]
    public GameEvent onValueChangeEvent;
    [Space]
    [Tooltip("[pt-BR] - Callbacks para mudança do valor da variável.")]
    public UnityEvent onValueChange;

    [Space]
    [Tooltip("[pt-BR] - Evento a ser disparado quando o valor da variável for reduzido.")]
    public GameEvent onValueIncreaseEvent;
    [Space]
    [Tooltip("[pt-BR] - Callbacks para redução do valor da variável.")]
    public UnityEvent onValueIncrease;

    [Space]
    [Tooltip("[pt-BR] - Evento a ser disparado quando o valor da variável for aumentado.")]
    public GameEvent onValueDecreaseEvent;
    [Space]
    [Tooltip("[pt-BR] - Callbacks para aumento do valor da variável.")]
    public UnityEvent onValueDecrease;


    /// <summary>
    /// [pt-BR] - Verifica o valor atual da variavel e o seu novo valor disparando os eventos
    /// correspondentes a mudança de valor caso ela ocorra.
    /// </summary>
    /// <param name="presentValue">Valor atual.</param>
    /// <param name="newValue">Novo valor.</param>
    public void VerifyEvents(float presentValue, float newValue)
    {
        if (presentValue < newValue)
        {
            if(onValueIncreaseEvent != null) onValueIncreaseEvent.Raise();
            onValueIncrease.Invoke();

            if (onValueChangeEvent != null) onValueChangeEvent.Raise();
            onValueChange.Invoke();
        }
        else if (presentValue > newValue)
        {
            if (onValueDecreaseEvent != null) onValueDecreaseEvent.Raise();
            onValueDecrease.Invoke();

            if (onValueChangeEvent != null) onValueChangeEvent.Raise();
            onValueChange.Invoke();
        }
    }


    /// <summary>
    /// [pt-BR] - Verifica o valor atual da variavel e o seu novo valor disparando os eventos
    /// correspondentes a mudança de valor caso ela ocorra.
    /// </summary>
    /// <param name="presentValue">Valor atual.</param>
    /// <param name="newValue">Novo valor.</param>
    public void VerifyEvents(int presentValue, int newValue)
    {
        if (presentValue < newValue)
        {
            if (onValueIncreaseEvent != null) onValueIncreaseEvent.Raise();
            onValueIncrease.Invoke();

            if (onValueChangeEvent != null) onValueChangeEvent.Raise();
            onValueChange.Invoke();
        }
        else if (presentValue > newValue)
        {
            if (onValueDecreaseEvent != null) onValueDecreaseEvent.Raise();
            onValueDecrease.Invoke();

            if (onValueChangeEvent != null) onValueChangeEvent.Raise();
            onValueChange.Invoke();
        }
    }


    /// <summary>
    /// [pt-BR] - Verifica o valor atual da variavel e o seu novo valor disparando os eventos
    /// correspondentes a mudança de valor caso ela ocorra.
    /// </summary>
    /// <param name="presentValue">Valor atual.</param>
    /// <param name="newValue">Novo valor.</param>
    public void VerifyEvents(string presentValue, string newValue)
    {
        if (presentValue != newValue)
        {
            if (onValueChangeEvent != null) onValueChangeEvent.Raise();
            onValueChange.Invoke();
        }
    }


    /// <summary>
    /// [pt-BR] - Verifica o valor atual da variavel e o seu novo valor disparando os eventos
    /// correspondentes a mudança de valor caso ela ocorra.
    /// </summary>
    /// <param name="presentValue">Valor atual.</param>
    /// <param name="newValue">Novo valor.</param>
    public void VerifyEvents(bool presentValue, bool newValue)
    {
        if (presentValue != newValue)
        {
            if (onValueChangeEvent != null) onValueChangeEvent.Raise();
            onValueChange.Invoke();
        }
    }
}


/// <summary>
/// [pt-BR] - Classe base para criação de variáveis scriptaveis.
/// </summary>
[System.Serializable]
public abstract class Variable : ScriptableObject
{
    [TextArea(3,5)] [SerializeField] private string description = "[pt-BR] - Aqui você pode adicionar uma descrição da variavel.";
}
