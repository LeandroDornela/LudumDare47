using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Options
{
    public GameObject positiveOptionTrigger;
    public TextLines positiveOption;
    public GameObject negativeOptionTrigger;
    public TextLines negativeOption;

    public void SetOptionsState(bool value)
    {
        if (positiveOptionTrigger) positiveOptionTrigger.SetActive(value);
        if (negativeOptionTrigger) negativeOptionTrigger.SetActive(value);
    }
}


public class NPC : MonoBehaviour
{
    public string name;
    public int initialLevel = 0;
    public int level = 0;
    public Options[] optionPerLevel;

    [Header("Callbacks")]
    public UnityEvent onInteraction;

    private TextBox textBox;
    public bool CanInteract { get; set; }

    private void Awake()
    {
        textBox = FindObjectOfType<TextBox>();

        SetOptionsFromLvl();
    }


    public void SetOptionsFromLvl()
    {
        for (int i = 0; i < optionPerLevel.Length; i++)
        {
            optionPerLevel[i].SetOptionsState(false);
        }

        if (level < optionPerLevel.Length)
        {
            optionPerLevel[level].SetOptionsState(true);
            CanInteract = true;
        }
    }


    public void PositiveInteraction()
    {
        optionPerLevel[level].SetOptionsState(false);

        textBox.DisplayTexts(optionPerLevel[level].positiveOption);

        level++;

        CanInteract = false;

        onInteraction.Invoke();
    }


    public void NegativeInteraction()
    {
        optionPerLevel[level].SetOptionsState(false);

        textBox.DisplayTexts(optionPerLevel[level].negativeOption);

        level--;

        CanInteract = false;

        onInteraction.Invoke();
    }
}
