using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerMovement))]
public class PlayerMovimentEditor : Editor {

    
    PlayerMovement alvo;
    SerializedProperty _nome, _ataque, _hp,_mp, _Animator, _Direcao, _Escudo;
    void OnEnable()
    {
        alvo = (PlayerMovement)target;
        _nome = serializedObject.FindProperty("_Nome");
        _ataque = serializedObject.FindProperty("_Ataque");
        _hp = serializedObject.FindProperty("VidaMax");
        _mp = serializedObject.FindProperty("AmanusMax");
        _Animator = serializedObject.FindProperty("anim");
        _Direcao = serializedObject.FindProperty("Direcao");
        _Escudo = serializedObject.FindProperty("Escudo");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("Propriedades do Personagem:");
        EditorGUILayout.PropertyField(_nome);
        EditorGUILayout.PropertyField(_hp);
        EditorGUILayout.PropertyField(_mp);
        EditorGUILayout.PropertyField(_ataque);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Animator: ");
        EditorGUILayout.PropertyField(_Animator);
        EditorGUILayout.LabelField("Direção que o personagem está olhando: ");
        EditorGUILayout.PropertyField(_Direcao);
        EditorGUILayout.LabelField("Referencia do escudo: ");
        EditorGUILayout.PropertyField(_Escudo);
        serializedObject.ApplyModifiedProperties();
    }
}
