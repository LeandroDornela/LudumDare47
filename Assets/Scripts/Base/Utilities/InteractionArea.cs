/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 02/2020
//************************************************************/


using UnityEngine;
using UnityEngine.Events;


namespace InteractionArea
{
    public enum DetectionMode
    {
        Trigger,
        TriggerAndInput
    }


    /// <summary>
    /// [pt-BR] - Área para interação do jogador. Basicamente um trigger com
    /// um disparador de eventos.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class InteractionArea : MonoBehaviour
    {
        [Tooltip("[pt-BR] - Mode de detecção de interação com a área.")]
        [SerializeField] private DetectionMode detectionMode;

        [Tooltip("[pt-BR] - Input para interação com a área.")]
        [NaughtyAttributes.InputAxis] [SerializeField] private string interactionInput;

        [SerializeField] private VariableBool interactionCondition;

        [Tooltip("[pt-BR] - Disparar interação enquando permanecer na área.")]
        [SerializeField] private bool objectInteractWhileInArea;

        [Tooltip("[pt-BR] - Desabilitar o script de interação apos interagir.")]
        [SerializeField] private bool disableComponentAfterInteraction;

        [Tooltip("[pt-BR] - Destruir o gameobject após a interação.")]
        [SerializeField] private bool destroyAfterInteraction;

        [Space]
        [Tooltip("[pt-BR] - Callbacks para o momento da interação.")]
        [SerializeField] private UnityEvent onInteraction;

        [SerializeField] private UnityEvent onEnterArea;

        [SerializeField] private UnityEvent onExitArea;
        

        private bool objectInArea = false;
        private bool interacted = false;

        private Collider _collider;


        private void Update()
        {
            if(!objectInArea)
            {
                return;
            }

            if (!objectInteractWhileInArea && interacted)
            {
                return;
            }

            if(interactionCondition != null)
            {
                if(!interactionCondition.Value())
                {
                    return;
                }
            }

            switch (detectionMode)
            {
                case DetectionMode.Trigger:
                    Interact();
                    break;
                case DetectionMode.TriggerAndInput:
                    if (Input.GetButtonDown(interactionInput))
                    {
                        Interact();
                    }
                    break;
            }
        }


        /// <summary>
        /// [pt-BR] - Lançar a interação.
        /// </summary>
        void Interact()
        {
            onInteraction.Invoke();
            interacted = true;

            if (destroyAfterInteraction)
            {
                Destroy(gameObject);
                return;
            }

            if (disableComponentAfterInteraction)
            {
                this.enabled = false;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            objectInArea = true;

            onEnterArea.Invoke();
        }


        private void OnTriggerExit(Collider other)
        {
            objectInArea = false;
            interacted = false;

            onExitArea.Invoke();
        }


        private void OnDrawGizmos()
        {
            if (_collider == null)
            {
                _collider = GetComponent<Collider>();
            }

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
        }
    }
}
