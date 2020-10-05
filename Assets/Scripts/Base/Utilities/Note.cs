/**************************************************************
* Programador: Leandro Dornela Ribeiro
* Contato: leandrodornela@ice.ufjf.br
* Data de criação: 04/2020
//************************************************************/


using UnityEngine;


[System.Serializable]
public class Note : MonoBehaviour
{
    [TextArea(5, 15)]
    public string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
        "Integer suscipit tempus mollis. Curabitur est est, malesuada eu velit vel, volutpat fermentum quam. " +
        "In non dolor et magna tincidunt vulputate non ullamcorper mauris. Nunc et risus diam. Suspendisse ac " +
        "arcu dolor. Aliquam non arcu et elit ullamcorper commodo. Nulla tincidunt blandit pulvinar. Aliquam " +
        "laoreet sapien et sagittis blandit. Vestibulum at nisl lacinia, imperdiet enim sit amet, feugiat ligula. " +
        "Donec sed dui sed ante tempus fermentum quis nec ligula.";
}
