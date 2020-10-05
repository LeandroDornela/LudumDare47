using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public NPC[] npcs;
    public PlayerController player;
    public int maxDayActions = 3;
    public PlayableDirector fadeSequence;
    public ClockControl clock;

    [Header("Callbacks")]
    public UnityEvent onStart;
    public UnityEvent nextDay;
    public UnityEvent gameOver;

    private int totalActions = 0;
    private bool inGame = true;

    // Start is called before the first frame update
    void Start()
    {
        onStart.Invoke();
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void HalfFadeReaction()
    {
        if(inGame)
        {
            SetNextDay();
        }
    }


    public void NPCInteraction()
    {
        totalActions++;

        if(VerifyVictoryCondition())
        {
            GameOver();
        }

        if(HaveIntereaction())
        {
            AdvanceTime();
        }
        else
        {
            StartNextDayAnimation();
        }
    }


    void StartNextDayAnimation()
    {
        fadeSequence.Play();
    }


    void AdvanceTime()
    {
        clock.SetTime(totalActions * (99/maxDayActions));
    }


    void SetNextDay()
    {
        clock.SetTime(0);
        player.ResetPosition();
        totalActions = 0;

        for (int i = 0; i < npcs.Length; i++)
        {
            npcs[i].SetOptionsFromLvl();
        }
    }


    void GameOver()
    {
        Debug.Log("Fim de jogo. Vitoria.");
        inGame = false;
    }


    bool VerifyVictoryCondition()
    {
        for(int i = 0; i < npcs.Length; i++)
        {
            if(npcs[i].level < npcs[i].optionPerLevel.Length)
            {
                return false;
            }
        }

        return true;
    }

    bool HaveIntereaction()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            if (npcs[i].CanInteract) return true;
        }

        return false;
    }
}
