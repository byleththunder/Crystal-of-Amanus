using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(Inventario))]
public class InventarioEditor : Editor {

    Inventario alvo;
    string[] Nomes;
    
    SerializedProperty Indice;
    void OnEnable()
    {
        alvo = (Inventario)target;
        Indice = serializedObject.FindProperty("indice");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("Itens: ");

        Nomes = alvo.ItensNames();
        
        EditorGUILayout.Popup(Indice.intValue, Nomes);
            serializedObject.ApplyModifiedProperties();
    }
}
