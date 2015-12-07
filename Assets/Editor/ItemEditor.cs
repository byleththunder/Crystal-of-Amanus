using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor {

    Item alvo;
    SerializedProperty _Nome,_Descricao,_Vida,_Amanus,_Ataque,_Defesa, _Preco,_Ico,_IcoName,_Metodo;
    void OnEnable()
    {
        alvo = (Item)target;
        _Nome = serializedObject.FindProperty("Nome");
        _Descricao = serializedObject.FindProperty("Descricao");
        _Vida = serializedObject.FindProperty("Vida");
        _Amanus = serializedObject.FindProperty("Amanus");
        _Ataque = serializedObject.FindProperty("Ataque");
        _Defesa = serializedObject.FindProperty("Defesa");
        _Preco = serializedObject.FindProperty("Preco");
        _Ico = serializedObject.FindProperty("Img");
        _IcoName = serializedObject.FindProperty("NameImg");
        _Metodo = serializedObject.FindProperty("MetodoItem");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_Ico);
        EditorGUILayout.PropertyField(_Nome);
        EditorGUILayout.PropertyField(_Descricao);
        #region Type
        alvo.Tipo = (Item.TipoDeItem)EditorGUILayout.EnumPopup(alvo.Tipo);
        alvo.SetItem();
        if (alvo.Tipo == Item.TipoDeItem.Equipamento)
        {
            EditorGUILayout.PropertyField(_Vida);
            EditorGUILayout.PropertyField(_Amanus);
            EditorGUILayout.PropertyField(_Ataque);
            EditorGUILayout.PropertyField(_Defesa);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_Preco);
            
        }
        else if (alvo.Tipo == Item.TipoDeItem.NaoConsumivel)
        {
        }
        else if(alvo.Tipo == Item.TipoDeItem.Potion)
        {
            EditorGUILayout.PropertyField(_IcoName);
            EditorGUILayout.PropertyField(_Vida);
            EditorGUILayout.PropertyField(_Amanus);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_Preco);
        }
        else if(alvo.Tipo == Item.TipoDeItem.Trigger)
        {
        }
        #endregion
        serializedObject.ApplyModifiedProperties();
    }
}
