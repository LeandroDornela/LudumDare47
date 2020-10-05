/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 04/2020
//************************************************************/


using UnityEngine;


/// <summary>
/// [pt-BR] - Classe base para singletons.
/// </summary>
/// <typeparam name="T">Tipo da classe que deve ser um singleton.</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if(instance == null)
                {
                    Debug.LogError("[en-US] - No instance of singleton of type " + typeof(T));
                }
            }

            return instance;
        }
    }

    protected void Awake()
    {
        if (instance == null)
        {
            instance = (T)FindObjectOfType(typeof(T));

            DontDestroyOnLoad(gameObject);

            if (instance == null)
            {
                Debug.LogError("[en-US] - No instance of singleton of type " + typeof(T));
            }
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
