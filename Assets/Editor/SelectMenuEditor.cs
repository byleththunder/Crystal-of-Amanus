using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
[CustomEditor(typeof(SelectMenu))]
public class SelectMenuEditor : Editor
{

    

    SerializedProperty ListaNomes, ListaDestinos, ListaTipos, Objetos, IndexSize, Prefabricavel, Tamanho;
    bool mostrarTipos = false;
    SelectMenu menu;



    void OnEnable()
    {
        ListaNomes = serializedObject.FindProperty("NomeOpcoes");
        ListaDestinos = serializedObject.FindProperty("Destino");
        ListaTipos = serializedObject.FindProperty("TipoDeOpcoes");
        Objetos = serializedObject.FindProperty("obj");
        IndexSize = serializedObject.FindProperty("Sizes");
        Prefabricavel = serializedObject.FindProperty("ConteinerPrefab");
        Tamanho = serializedObject.FindProperty("size");
        menu = (SelectMenu)target;

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Insira o prefab das Opções :");
        EditorGUILayout.PropertyField(Prefabricavel, true);

        EditorGUILayout.LabelField("Insira a quantidade de Opções abaixo.");
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(Tamanho, true);
        if (Tamanho.intValue < 1) { Tamanho.intValue = 1; }

        if (Tamanho.intValue > 0)
        {
            mostrarTipos = true;
        }
        else
        {
            mostrarTipos = false;
        }
        EditorGUILayout.Space();
        #region Resize
        if (Tamanho.intValue != ListaNomes.arraySize)
        {
            while (Tamanho.intValue > ListaNomes.arraySize)
            {
                ListaNomes.InsertArrayElementAtIndex(ListaNomes.arraySize);
            }
            while (Tamanho.intValue < ListaNomes.arraySize)
            {
                ListaNomes.DeleteArrayElementAtIndex(ListaNomes.arraySize - 1);
            }
        }
        if (Tamanho.intValue != ListaDestinos.arraySize)
        {
            while (Tamanho.intValue > ListaDestinos.arraySize)
            {
                ListaDestinos.InsertArrayElementAtIndex(ListaDestinos.arraySize);
            }
            while (Tamanho.intValue < ListaDestinos.arraySize)
            {
                ListaDestinos.DeleteArrayElementAtIndex(ListaDestinos.arraySize - 1);
            }
        }
        if (Tamanho.intValue != ListaTipos.arraySize)
        {
            while (Tamanho.intValue > ListaTipos.arraySize)
            {
                ListaTipos.InsertArrayElementAtIndex(ListaTipos.arraySize);
            }
            while (Tamanho.intValue < ListaTipos.arraySize)
            {
                ListaTipos.DeleteArrayElementAtIndex(ListaTipos.arraySize - 1);
            }
        }
        if (Tamanho.intValue != Objetos.arraySize)
        {
            while (Tamanho.intValue > Objetos.arraySize)
            {
                Objetos.InsertArrayElementAtIndex(Objetos.arraySize);
            }
            while (Tamanho.intValue < Objetos.arraySize)
            {
                Objetos.DeleteArrayElementAtIndex(Objetos.arraySize - 1);
            }
        }
        if (Tamanho.intValue != IndexSize.arraySize)
        {
            while (Tamanho.intValue > IndexSize.arraySize)
            {
                IndexSize.InsertArrayElementAtIndex(IndexSize.arraySize);
            }
            while (Tamanho.intValue < IndexSize.arraySize)
            {
                IndexSize.DeleteArrayElementAtIndex(IndexSize.arraySize - 1);
            }
        }
        #endregion


        #region Arrumar
        EditorGUILayout.Space();
        #region Elementos
        EditorGUILayout.LabelField("----Começo----");
        try
        {

            for (int i = 0; i < Tamanho.intValue; i++)
            {
                EditorGUILayout.LabelField("Escreva o nome: ");
                EditorGUILayout.PropertyField(ListaNomes.GetArrayElementAtIndex(i), mostrarTipos);
                EditorGUILayout.LabelField("Seleciona o tipo: ");
                EditorGUILayout.PropertyField(ListaTipos.GetArrayElementAtIndex(i), mostrarTipos);
                if (menu.TipoDeOpcoes[i] == TipoOpcoes.AtivarGameObject)
                {
                    EditorGUILayout.LabelField("Coloque o GameObject à ser ativado: ");
                    if (menu.Destino[i].GetType() == typeof(MonoBehaviour))
                    {
                        menu.Destino.RemoveAt(i);
                    }
                    EditorGUILayout.PropertyField(ListaDestinos.GetArrayElementAtIndex(i));
                }
                if (menu.TipoDeOpcoes[i] == TipoOpcoes.AtivarScript)
                {
                    EditorGUILayout.LabelField("Coloque o GameObject aonde está o script: ");

                    EditorGUILayout.PropertyField(Objetos.GetArrayElementAtIndex(i));
                    if (menu.obj[i] != null)
                    {

                        string[] opcoes = new string[menu.obj[i].GetComponents<MonoBehaviour>().Length];
                        for (int j = 0; j < opcoes.Length; j++)
                        {
                            opcoes[j] = menu.obj[i].GetComponents<MonoBehaviour>()[j].ToString();
                        }

                        IndexSize.GetArrayElementAtIndex(i).intValue = EditorGUILayout.Popup(IndexSize.GetArrayElementAtIndex(i).intValue, opcoes);
                        menu.Destino[i] = menu.obj[i].GetComponents<MonoBehaviour>()[IndexSize.GetArrayElementAtIndex(i).intValue];
                        EditorGUILayout.LabelField("Script selecionado : " + menu.Destino[i]);
                    }

                }
                if (menu.TipoDeOpcoes[i] == TipoOpcoes.Back)
                {
                    if (menu.Destino[i].GetType() == typeof(MonoBehaviour))
                    {
                        menu.Destino.RemoveAt(i);
                    }
                    EditorGUILayout.LabelField("Coloque o destino para voltar: ");
                    EditorGUILayout.PropertyField(ListaDestinos.GetArrayElementAtIndex(i));
                }
                if (menu.TipoDeOpcoes[i] == TipoOpcoes.Confirm)
                {
                    EditorGUILayout.LabelField("Coloque o GameObject aonde está o script: ");

                    EditorGUILayout.PropertyField(Objetos.GetArrayElementAtIndex(i));
                    if (menu.obj[i] != null)
                    {

                        string[] opcoes = new string[menu.obj[i].GetComponents<MonoBehaviour>().Length];
                        for (int j = 0; j < opcoes.Length; j++)
                        {
                            opcoes[j] = menu.obj[i].GetComponents<MonoBehaviour>()[j].ToString();
                        }

                        IndexSize.GetArrayElementAtIndex(i).intValue = EditorGUILayout.Popup(IndexSize.GetArrayElementAtIndex(i).intValue, opcoes);
                        menu.Destino[i] = menu.obj[i].GetComponents<MonoBehaviour>()[IndexSize.GetArrayElementAtIndex(i).intValue];
                        EditorGUILayout.LabelField("Script selecionado : " + menu.Destino[i]);
                    }
                    if (menu.TipoDeOpcoes[i] == TipoOpcoes.Sair)
                    {
                        if (menu.Destino[i].GetType() == typeof(MonoBehaviour))
                        {
                            menu.Destino.RemoveAt(i);
                        }
                        EditorGUILayout.LabelField("Coloque o destino para sair: ");
                        EditorGUILayout.PropertyField(ListaDestinos.GetArrayElementAtIndex(i));
                    }
                    if (menu.TipoDeOpcoes[i] == TipoOpcoes.SairTotalmente)
                    {
                        if (menu.Destino[i].GetType() == typeof(MonoBehaviour))
                        {
                            menu.Destino.RemoveAt(i);
                        }
                        EditorGUILayout.LabelField("Coloque o destino para ser desativado: ");
                        EditorGUILayout.PropertyField(ListaDestinos.GetArrayElementAtIndex(i));
                    }
                    EditorGUILayout.Space();
                    if (i + 1 == Tamanho.intValue)
                    {
                        EditorGUILayout.LabelField("----Fim----");
                    }
                    else
                    {
                        EditorGUILayout.LabelField("----Outro----");
                    }

                }
            }
        }
        catch 
        {
        }
        #endregion
        #endregion
        serializedObject.ApplyModifiedProperties();
    }


}
