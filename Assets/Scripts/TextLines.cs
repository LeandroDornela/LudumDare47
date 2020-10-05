/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 03/2020
//************************************************************/


using UnityEngine;


/// <summary>
/// [pt-BR] - Linhas de texto para serem exibidas nas caixas de dialogo.
/// Um objeto excriptavel TextLine pode ser passado como parametro para
/// que os textos presentes sejam exibidos na caixa de diálogo.
/// </summary>
[CreateAssetMenu(fileName = "NewTextLines", menuName = "Text Box/Text Lines")]
public class TextLines : ScriptableObject
{
    [Tooltip("[pt-BR] - Borda da câmera onde a caixa de dialogo será exibida quando estas linhas" +
        "estiverem ativas.")]
    [SerializeField] private TextBox.ScreenPosition screenPosition;
    
    [Space]
    [Tooltip("[pt-BR] - Linhas de diálogo.")]
    [TextArea(3, 10)] [SerializeField] private string[] lines;


    /// <summary>
    /// [pt-BR] - Retorna o array de linhas de diálogo.
    /// </summary>
    /// <returns></returns>
    public string[] GetLines()
    {
        return lines;
    }


    /// <summary>
    /// [pt-BR] - Retorna a posição onde a caixa de diálogo deve ser exibida.
    /// </summary>
    /// <returns></returns>
    public TextBox.ScreenPosition GetScreenPosition()
    {
        return screenPosition;
    }
}
