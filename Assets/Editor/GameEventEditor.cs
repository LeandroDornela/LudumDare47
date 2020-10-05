using System.Globalization;
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameEvent))]
class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEvent myScript = (GameEvent)target;
        if (GUILayout.Button("Raise Event"))
        {
            myScript.Raise();
        }
    }
}