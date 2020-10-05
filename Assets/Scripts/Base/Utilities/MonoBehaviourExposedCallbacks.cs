/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 02/2020
//************************************************************/


using UnityEngine;
using UnityEngine.Events;


public class MonoBehaviourExposedCallbacks : MonoBehaviour
{
    public UnityEvent onAwake;
    public UnityEvent onLevelWasLoaded;
    public UnityEvent onStart;
    public UnityEvent onUpdate;
    public UnityEvent onLateUpdate;
    public UnityEvent onFixedUpdate;
    public UnityEvent onApplicationFocusIn;
    public UnityEvent onApplicationFocusOut;


    private void Awake()
    {
        onAwake.Invoke();
    }


    private void OnLevelWasLoaded(int level)
    {
        onLevelWasLoaded.Invoke();
    }


    void Start()
    {
        onStart.Invoke();
    }


    void Update()
    {
        onUpdate.Invoke();
    }


    private void LateUpdate()
    {
        onLateUpdate.Invoke();
    }


    private void FixedUpdate()
    {
        onFixedUpdate.Invoke();
    }


    private void OnApplicationFocus(bool focus)
    {
        if(focus)
        {
            onApplicationFocusIn.Invoke();
        }
        else
        {
            onApplicationFocusOut.Invoke();
        }
    }
}
