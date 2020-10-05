/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 13/01/2020
//************************************************************/


using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "NewVector3", menuName = "Variables/Vector3")]
public class VariableVector3 : Variable
{
    [Header("Values")]

    [Tooltip("[pt-BR] - Valor atual da variável.")]
    [SerializeField] private Vector3 value = Vector3.zero;

    [Tooltip("[pt-BR] - Valor padrão da variável.")]
    [SerializeField] private Vector3 defaultValue = Vector3.zero;


    [Header("Security")]

    [Tooltip("[pt-BR] - Proteger a variável para gravação.")]
    [SerializeField] private bool protectRecord = false;

    [Tooltip("[pt-BR] - Proteger a variável para leitura.")]
    [SerializeField] private bool protectRead = false;


    [Header("Debug")]

    [Tooltip("[pt-BR] - Utilizar mensagens de log.")]
    [SerializeField] private bool debugLog = false;
    [Space]
    [InspectorMethod("SetValue", buttonName = "Set Value", useValue = true)]
    public Vector3 setValueMethodButton = Vector3.zero;
    [Space]
    [InspectorMethod("Reset", buttonName = "Reset", useValue = false)]
    public bool resetMethodButton;


    /// <summary>
    /// [pt-BR] - Retorna o valor atual da variável.
    /// </summary>
    /// <returns></returns>
    public Vector3 Value()
    {
        if (protectRead)
        {
            if (debugLog) Debug.LogWarning("[en-US] - " + name + " is protected to read.");
            return Vector3.zero;
        }

        return value;
    }


    /// <summary>
    /// [pt-BR] - Seta um novo valor da variável levando em conta as possiveis limitações de valor.
    /// </summary>
    /// <param name="val">Novo valor da variavel.</param>
    /// <returns></returns>
    public void SetValue(Vector3 val)
    {
        if (protectRecord)
        {
            if (debugLog) Debug.Log("[en-US] - " + name + " is protected to record.");
            return;
        }

        value = val;
    }


    /// <summary>
    /// [pt-BR] - Bloqueia a variável para gravação.
    /// </summary>
    public void LockRecord()
    {
        protectRecord = true;
    }


    /// <summary>
    /// [pt-BR] - Desbloqueia a variável para gravação.
    /// </summary>
    public void UnlockRecord()
    {
        protectRecord = false;
    }


    /// <summary>
    /// [pt-BR] - Bloqueia a variável para leitura.
    /// </summary>
    public void LockRead()
    {
        protectRead = true;
    }


    /// <summary>
    /// [pt-BR] - Desbloqueia a variável para leitura.
    /// </summary>
    public void UnlockRead()
    {
        protectRead = false;
    }


    /// <summary>
    /// [pt-BR] - Seta o valor padrão da variável.
    /// </summary>
    /// <param name="val">Novo valor padrão.</param>
    public void SetDefaultValue(Vector3 val)
    {
        defaultValue = val;
    }


    /// <summary>
    /// [pt-BR] - Reseta a variavel para o valor padão.
    /// </summary>
    public void Reset()
    {
        value = defaultValue;
    }
}