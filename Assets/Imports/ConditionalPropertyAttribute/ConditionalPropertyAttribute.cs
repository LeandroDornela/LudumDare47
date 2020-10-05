using UnityEngine;

//https://gamedev.stackexchange.com/questions/157127/how-to-make-a-property-visible-in-the-inspector-window-if-another-property-is-tr
public class ConditionalPropertyAttribute : PropertyAttribute
{

    public string condition;

    public ConditionalPropertyAttribute(string condition)
    {
        this.condition = condition;
    }
}
