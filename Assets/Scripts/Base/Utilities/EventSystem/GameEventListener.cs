/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 02/2020
//************************************************************/


using UnityEngine;
using UnityEngine.Events;


namespace GameEventListener
{
    // TODO: Adicionar uma função para registrar o listener no evento diretamente.
    [System.Serializable]
    public struct Listener
    {
        public GameEvent Event;
        public UnityEvent Response;

        public void OnEventRised()
        {
            Response.Invoke();
        }
    }

    [System.Serializable]
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private Listener[] listeners;

        private void OnEnable()
        {
            for (int i = 0; i < listeners.Length; i++)
            {
                if(listeners[i].Event != null) listeners[i].Event.RegisterListener(listeners[i]);
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < listeners.Length; i++)
            {
                if (listeners[i].Event != null) listeners[i].Event.UnregisterListener(listeners[i]);
            }
        }
    }
}
