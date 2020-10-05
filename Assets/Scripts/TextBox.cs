/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 03/2020
//************************************************************/


using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary>
/// [pt-BR] - Caixa de texto com supote a multiplas linhas de dialogo e diferentes
/// formas de avanço de texto.
/// </summary>
public class TextBox : MonoBehaviour
{
    public enum ScreenPosition
    {
        top,
        botton
    }

    [Header("References")]

    [Tooltip("[pt-BR] - Componente de texto.")]
    [SerializeField] private TMP_Text text;

    [Tooltip("[pt-BR] - Janela de texto.")]
    [SerializeField] private RectTransform textWindow;

    [Tooltip("[pt-BR] - Icone indicador para pular o texto.")]
    [SerializeField] private GameObject skipTextIcon;


    [Header("Aniamtion")]

    [Tooltip("[pt-BR] - Distância da borda da câmera.")]
    [SerializeField] private float borderDistance = 0;

    [Tooltip("[pt-BR] - Velocidade da animação de movimento da caixa.")]
    [SerializeField] private float animationSpeed = 5;


    [Header("Input")]

    [Tooltip("[pt-BR] - Input para pular o texto.")]
    [NaughtyAttributes.InputAxis] [SerializeField] private string skipTextInput;

    [Tooltip("[pt-BR] - Caso ativado o jogador pode pular o texto a qualquer momento.")]
    [SerializeField] private bool useInputToSkip = true;

    [Tooltip("[pt-BR] - Caso ativado o proximo texto só será exibido quando o jogador pressionar o botão.")]
    [SerializeField] private bool waitInputToAdvance = false;


    [Header("Time")]

    [Tooltip("[pt-BR] - Tempo minimo que o texto permanece visivel.")]
    [SerializeField] private float textTime = 10;

    [Tooltip("[pt-BR] - Caso ativado o tempo será parado com deltatime = 0.")]
    [SerializeField] private bool stopTimeIfOpen = true;


    [Header("Events")]

    [SerializeField] private UnityEvent onOpen;
    [SerializeField] private UnityEvent onClose;


    // Lista de textos a serem exibidos.
    private List<string> texts;

    // Contador de tempo de exibição dos textos.
    private float timer = 0;

    // Quando verdadeiro a caixa ficará visivel.
    private bool displayText = false;

    // Animação de abertura ativa.
    private bool inOpenAnimation = false;

    // Animação de fechamento ativa.
    private bool inCloseAnimation = false;

    // Posição destino para a caixa de dialogo.
    private Vector2 targetPosition = Vector2.zero;

    // Borda da tela de referência para a animação.
    private ScreenPosition screenPosition;

    // Referência ao canvas scaler.
    private CanvasScaler canvasScaler;

    // Posição original da caixa de diálogo.
    private Vector3 defaultPosition = Vector2.zero;



    void Awake()
    {
        texts = new List<string>();
        canvasScaler = GetComponentInParent<CanvasScaler>();

        defaultPosition.y = canvasScaler.referenceResolution.y / 2 + textWindow.sizeDelta.y / 2;

        textWindow.localPosition = defaultPosition;
    }


    void Update()
    {
        if(inOpenAnimation || inCloseAnimation)
        {
            MoveTextBox();
            return;
        }

        if (!displayText)
        {
            return;
        }


        if (useInputToSkip && Input.GetButtonDown(skipTextInput))
        {
            // Zera contador, verifica ultima linha e exibe a prixima.
            timer = 0;
            if (texts.Count == 0)
            {
                CloseBox();
                return;
            }
            NextLine();

            return;
        }


        timer += Time.unscaledDeltaTime;


        if (timer >= textTime)
        {
            if(waitInputToAdvance)
            {
                if (!useInputToSkip && !skipTextIcon.activeSelf)
                {
                    skipTextIcon.SetActive(true);
                }

                if (Input.GetButtonDown(skipTextInput))
                {
                    // Zera contador, verifica ultima linha e exibe a prixima.
                    timer = 0;
                    if (texts.Count == 0)
                    {
                        CloseBox();
                        return;
                    }
                    NextLine();
                }
            }
            else
            {
                // Zera contador, verifica ultima linha e exibe a prixima.
                timer = 0;
                if (texts.Count == 0)
                {
                    CloseBox();
                    return;
                }
                NextLine();
            }
        }
    }


    /// <summary>
    /// [pt-BR] - Modifica a posição da caixa de diálogo até a posição destino.
    /// </summary>
    void MoveTextBox()
    {
        if(Vector2.Distance(textWindow.localPosition, targetPosition) >= 1)
        {
            textWindow.localPosition = Vector2.Lerp(textWindow.localPosition, targetPosition, animationSpeed * Time.unscaledDeltaTime);

            if(Vector2.Distance(textWindow.localPosition, targetPosition) < 1)
            {
                textWindow.localPosition = targetPosition;

                if(inOpenAnimation)
                {
                    EndOfOpenAnimation();
                }
                else if(inCloseAnimation)
                {
                    EndOfCloseAnimation();
                }
            }
        }
    }


    /// <summary>
    /// [pt-BR] - Seta a posição alvo da caixa de acordo com a borda da tela de referência.
    /// </summary>
    void SetBoxTargetPosition()
    {
        Vector2 pos;

        switch (screenPosition)
        {
            case ScreenPosition.top:
                defaultPosition.y = canvasScaler.referenceResolution.y / 2 + textWindow.sizeDelta.y / 2;
                textWindow.localPosition = defaultPosition;

                pos.x = 0;
                pos.y = canvasScaler.referenceResolution.y/2 - textWindow.sizeDelta.y / 2 - borderDistance;
                targetPosition = pos;

                break;
            case ScreenPosition.botton:
                defaultPosition.y = - canvasScaler.referenceResolution.y / 2 - textWindow.sizeDelta.y / 2;
                textWindow.localPosition = defaultPosition;

                pos.x = 0;
                pos.y = - canvasScaler.referenceResolution.y / 2 + textWindow.sizeDelta.y / 2 + borderDistance;
                targetPosition = pos;

                break;
        }
    }


    /// <summary>
    /// [pt-BR] - Exibe a próxima linha de texto.
    /// </summary>
    void NextLine()
    {
        text.text = texts[0];
        texts.RemoveAt(0);

        if (!useInputToSkip) skipTextIcon.SetActive(false);
    }


    /// <summary>
    /// [pt-BR] - Inicia a abertura da caixa de diálogo.
    /// </summary>
    void OpenBox()
    {
        textWindow.gameObject.SetActive(true);
        displayText = true;
        inOpenAnimation = true;


        SetBoxTargetPosition();


        if (stopTimeIfOpen) Time.timeScale = 0;
        if (useInputToSkip) skipTextIcon.SetActive(true);
        else skipTextIcon.SetActive(false);

        onOpen.Invoke();
    }


    /// <summary>
    /// [pt-BR] - Inicia o fechamento da caixa de diálogo.
    /// </summary>
    public void CloseBox()
    {
        displayText = false;
        inCloseAnimation = true;
        targetPosition = defaultPosition;

        onClose.Invoke();
    }


    /// <summary>
    /// [pt-BR] - Solicita a abertura da caixa de diálogo para exibição das linhas passadas como parâmetro.
    /// </summary>
    /// <param name="_texts">Linhas de textos a serem exibidas.</param>
    public void DisplayTexts(string[] _texts)
    {
        if(!displayText)
        {
            OpenBox();
            timer = 0;
        }

        for(int i = 0; i < _texts.Length; i++)
        {
            texts.Add(_texts[i]);
        }

        NextLine();
    }


    /// <summary>
    /// [pt-BR] - Solicita a abertura da caixa de diálogo para exibição das linhas passadas como parâmetro.
    /// </summary>
    /// <param name="lines">Linhas de textos a serem exibidas.</param>
    public void DisplayTexts(TextLines lines)
    {
        screenPosition = lines.GetScreenPosition();

        DisplayTexts(lines.GetLines());
    }


    /// <summary>
    /// [pt-BR] - Exibe uma linha de texto.
    /// </summary>
    /// <param name="_text">Linha de texto a ser exibida.</param>
    void DisplayText(string _text)
    {
        if (!displayText)
        {
            OpenBox();
            timer = 0;
        }

        texts.Add(_text);

        NextLine();
    }


    /// <summary>
    /// [pt-BR] - Executa as ações no fim da animação de abertura.
    /// </summary>
    void EndOfOpenAnimation()
    {
        inOpenAnimation = false;
    }


    /// <summary>
    /// [pt-BR] - Executa as ações no fim da animação de fechamento.
    /// </summary>
    void EndOfCloseAnimation()
    {
        inCloseAnimation = false;
        textWindow.gameObject.SetActive(false);
        if (stopTimeIfOpen) Time.timeScale = 1;
    }
}
