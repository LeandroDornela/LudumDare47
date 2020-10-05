/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 07/01/2020
//************************************************************/


using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "NewInt", menuName = "Variables/Int")]
public class VariableInt : Variable
{
    [Header("Values")]

    [Tooltip("[pt-BR] - Valor atual da variável.")]
    [SerializeField] private int value;

    [Tooltip("[pt-BR] - Valor padrão da variável.")]
    [SerializeField] private int defaultValue;

    [Tooltip("[pt-BR] - Utilizar um valor máximo para a variável.")]
    [SerializeField] private bool useMaxValue = false;

    [Tooltip("[pt-BR] - Valor máximo para a variável.")]
    [ConditionalProperty("useMaxValue")]
    [SerializeField] private int maxValue = 0;

    [Tooltip("[pt-BR] - Utilizar um valor minimo para a variável.")]
    [SerializeField] private bool useMimValue = false;

    [Tooltip("[pt-BR] - Valor minimo para a variável.")]
    [ConditionalProperty("useMimValue")]
    [SerializeField] private int mimValue = 0;


    [Header("Security")]

    [Tooltip("[pt-BR] - Proteger a variável para gravação.")]
    [SerializeField] private bool protectRecord = false;

    [Tooltip("[pt-BR] - Proteger a variável para leitura.")]
    [SerializeField] private bool protectRead = false;

    [Tooltip("[pt-BR] - Modificar o valor da variável quando lida. Caso os valores minimos e maximos tenham" +
        "sido setados eles serão usados como limites, caso não estejam os valores limite serão os maiores" +
        "possiveis.")]
    [SerializeField] private bool randomizeOnRead = false;


    [Header("Events")]

    [Tooltip("[pt-BR] - Utilizar os eventos para mudança no valor da variável.")]
    [SerializeField] private bool useEvents = false;
    [ConditionalProperty("useEvents")]
    [SerializeField] private ValueChangeEvents valueChangeEvents;


    [Header("Debug")]

    [Tooltip("[pt-BR] - Utilizar mensagens de log.")]
    [SerializeField] private bool debugLog = false;
    [Space]
    [InspectorMethod("Increase", buttonName = "Increase", useValue = true)]
    public int increaseMethodButton = 0;
    [InspectorMethod("Decrease", buttonName = "Decrease", useValue = true)]
    public int decreaseMethodButton = 0;
    [InspectorMethod("SetValue", buttonName = "Set Value", useValue = true)]
    public int setValueMethodButton = 0;
    [Space]
    [InspectorMethod("Reset", buttonName = "Reset", useValue = false)]
    public bool resetMethodButton;


    /// <summary>
    /// [pt-BR] - Retorna o valor atual da variável.
    /// </summary>
    /// <returns></returns>
    public int Value()
    {
        int presentValue = value;

        if (protectRead)
        {
            if (debugLog) Debug.LogWarning("[en-US] - " + name + " is protected to read.");
            return 0;
        }

        if (randomizeOnRead)
        {
            int min = int.MinValue;
            int max = int.MaxValue;

            if (useMimValue)
            {
                min = mimValue;
            }

            if (useMaxValue)
            {
                max = maxValue + 1;
            }

            value = Random.Range(min, max);
        }

        return presentValue;
    }


    /// <summary>
    /// [pt-BR] - Seta um novo valor da variável levando em conta as possiveis limitações de valor.
    /// </summary>
    /// <param name="val">Novo valor da variavel.</param>
    /// <returns></returns>
    public void SetValue(int val)
    {
        if (protectRecord)
        {
            if (debugLog) Debug.Log("[en-US] - " + name + " is protected to record.");
            return;
        }

        if (useMaxValue && val > maxValue)
        {
            if (debugLog) Debug.Log("[en-US] - " + name + " have the max value: " + maxValue);
            return;
        }

        if (useEvents) valueChangeEvents.VerifyEvents(value, val);

        value = val;

        if (debugLog) Debug.Log("[en-US] - " + name + " value chaged.");
    }


    /// <summary>
    /// [pt-BR] - Incrementa o valor da variável pelo paramentro respeitando as limitaçãoes possiveis.
    /// </summary>
    /// <param name="val">Valor para ser somado à variavel.</param>
    /// <returns></returns>
    public void Increase(int val)
    {
        if (val == 0 || val < 0)
        {
            if (debugLog) Debug.Log("[en-US] - The value passed is not greater than 0.");
            return;
        }

        if (protectRecord)
        {
            if (debugLog) Debug.Log("[en-US] - " + name + " is protected to record.");
            return;
        }

        if (useMaxValue)
        {
            if (value == maxValue)
            {
                if (debugLog) Debug.Log("[en-US] - " + name + " have the max value.");
                return;
            }

            if (debugLog) Debug.Log("[en-US] - " + name + " value increased.");
            value += val;

            if (value > maxValue) value = maxValue;

            if (useEvents) valueChangeEvents.onValueIncrease.Invoke();
            return;
        }

        if (debugLog) Debug.Log("[en-US] - " + name + " value increased in " + val);
        value += val;
        if (useEvents) valueChangeEvents.onValueIncrease.Invoke();
    }


    /// <summary>
    /// [pt-BR] - Decrementa o valor da variável pelo paramentro respeitando as limitaçãoes possiveis.
    /// </summary>
    /// <param name="val">Valor para ser subitraido da variavel.</param>
    /// <returns></returns>
    public void Decrease(int val)
    {
        if (val == 0 || val < 0)
        {
            if (debugLog) Debug.Log("[en-US] - The value passed is not greater than 0.");
            return;
        }

        if (protectRecord)
        {
            if (debugLog) Debug.Log("[en-US] - " + name + " is protected to record.");
            return;
        }

        if (debugLog) Debug.Log("[en-US] - " + name + " value decreased.");
        value -= val;

        if (useMimValue && (value < mimValue))
        {
            value = mimValue;
        }

        if (useEvents) valueChangeEvents.onValueDecrease.Invoke();
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
    public void SetDefaultValue(int val)
    {
        defaultValue = val;
    }


    /// <summary>
    /// [pt-BR] - Seta o valor máximo que a variável pode ter.
    /// </summary>
    /// <param name="val">Novo valor máximo.</param>
    public void SetMaxValue(int val)
    {
        maxValue = val;
    }


    /// <summary>
    /// [pt-BR] - Seta o valor minimo que a variável pode ter.
    /// </summary>
    /// <param name="val">Novo valor minimo.</param>
    public void SetMimValue(int val)
    {
        mimValue = val;
    }


    /// <summary>
    /// [pt-BR] - Reseta a variavel para o valor maximo.
    /// </summary>
    public void ResetToMax()
    {
        value = maxValue;
    }


    /// <summary>
    /// [pt-BR] - Reseta a variavel para o valor minimo.
    /// </summary>
    public void ResetToMin()
    {
        value = mimValue;
    }


    /// <summary>
    /// [pt-BR] - Reseta a variavel para o valor padão.
    /// </summary>
    public void Reset()
    {
        value = defaultValue;
    }
}
