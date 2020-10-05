/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 17/03/2020
//************************************************************/


using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "NewIntArray", menuName = "Variables/Int Array")]
public class VariableIntArray : Variable
{
    [Header("Values")]

    [Tooltip("[pt-BR] - Valores atuais das variáveis.")]
    [SerializeField] private int[] values;

    [Tooltip("[pt-BR] - Valor padrão da variável.")]
    [SerializeField] private int[] defaultValues;


    [Header("Security")]

    [Tooltip("[pt-BR] - Proteger a variável para gravação.")]
    [SerializeField] private bool protectRecord = false;

    [Tooltip("[pt-BR] - Proteger a variável para leitura.")]
    [SerializeField] private bool protectRead = false;


    [Header("Debug")]

    [Tooltip("[pt-BR] - Utilizar mensagens de log.")]
    [SerializeField] private bool debugLog = false;
    [Space]
    [InspectorMethod("Reset", buttonName = "Reset", useValue = false)]
    public bool resetMethodButton;


    private void OnValidate()
    {
        if (values.Length != defaultValues.Length)
        {
            Debug.LogError("[en-US] - values.Length != defaultValues.Length in " + name);
        }
    }


    /// <summary>
    /// [pt-BR] - Retorna todos os valores armazenados.
    /// </summary>
    /// <returns></returns>
    public int[] Values()
    {
        if (protectRead)
        {
            if (debugLog) Debug.LogWarning("[en-US] - " + name + " is protected to read.");
            return null;
        }

        return values;
    }


    /// <summary>
    /// [pt-BR] - Retorna o valor armazena em uma posição.
    /// </summary>
    /// <param name="id">Identificador do valor.</param>
    /// <returns></returns>
    public int GetValue(int id)
    {
        if (id >= values.Length)
        {
            if (debugLog) Debug.LogWarning("[en-US] - Invalid ID.");
            return 0;
        }

        return values[id];
    }


    /// <summary>
    /// [pt-BR] - Tamanho do array de valores.
    /// </summary>
    /// <returns></returns>
    public int Length()
    {
        return values.Length;
    }


    /// <summary>
    /// [pt-BR] - Seta o valor de uma variável dado seu identificador.
    /// </summary>
    /// <param name="id">Identificador</param>
    /// <param name="val">Novo valor</param>
    public void SetValue(int id, int val)
    {
        if (protectRecord)
        {
            if (debugLog) Debug.Log("[en-US] - " + name + " is protected to record.");
            return;
        }

        if (id >= values.Length)
        {
            if (debugLog) Debug.LogWarning("[en-US] - Invalid ID.");

            return;
        }

        values[id] = val;
    }


    /// <summary>
    /// [pt-BR] - Seta todos os valores do array de uma vez.
    /// </summary>
    /// <param name="vals"></param>
    public void SetValues(int[] vals)
    {
        if (protectRecord)
        {
            if (debugLog) Debug.Log("[en-US] - " + name + " is protected to record.");
            return;
        }

        if (vals.Length != values.Length)
        {
            if (debugLog) Debug.LogWarning("[en-US] - Invalid array size.");

            return;
        }

        values = vals;
    }


    /// <summary>
    /// [pt-BR] - Adiciona um valor a variável correspondente ao id.
    /// </summary>
    /// <param name="id">Identificador.</param>
    /// <param name="val">Valor a ser adicionado.</param>
    public void IncreaceValue(int id, int val)
    {
        if (protectRecord)
        {
            if (debugLog) Debug.Log("[en-US] - " + name + " is protected to record.");
            return;
        }

        if (id >= values.Length)
        {
            if (debugLog) Debug.LogWarning("[en-US] - Invalid ID.");

            return;
        }

        values[id] += val;
    }


    /// <summary>
    /// [pt-BR] - Subtrai um valor da variável correspondente ao id.
    /// </summary>
    /// <param name="id">Identificador.</param>
    /// <param name="val">Valor a ser adicionado.</param>
    public void DecreaceValue(int id, int val)
    {
        if (protectRecord)
        {
            if (debugLog) Debug.Log("[en-US] - " + name + " is protected to record.");
            return;
        }

        if (id >= values.Length)
        {
            if (debugLog) Debug.LogWarning("[en-US] - Invalid ID.");

            return;
        }

        values[id] -= val;
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
    /// [pt-BR] - Reseta a variavel para o valor padão.
    /// </summary>
    public void Reset()
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = defaultValues[i];
        }
    }
}
