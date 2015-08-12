using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(ChamarComponent))]
public class ChamarComponentEditor : Editor
{
    SerializedProperty ListaScripts, ListaIndexs, Tamanho, ListaObjetos, ListaIndex2, DecisaoAcao;
    bool mostraElementos = false;
    string[] Opcoes = new string[2] { "Script do GameObject", "Script de Outro GameObject" };
    string[] Opcoes2 = new string[3] { "Script do GameObject", "Script de Outro GameObject", "GameObject" };
    ChamarComponent Alvo;
    void OnEnable()
    {
        ListaScripts = serializedObject.FindProperty("Scripts");
        ListaIndexs = serializedObject.FindProperty("index");
        Tamanho = serializedObject.FindProperty("size");
        ListaObjetos = serializedObject.FindProperty("objs");
        ListaIndex2 = serializedObject.FindProperty("Index2");
        DecisaoAcao = serializedObject.FindProperty("decisao");
        
        Alvo = (ChamarComponent)target;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(DecisaoAcao);

        #region Só Scripts
        if (Alvo.decisao == ChamarComponent.TipoDeAcao.SomenteScripts)
        {
            EditorGUILayout.LabelField("Quantos Scripts?");
            EditorGUILayout.PropertyField(Tamanho);
            EditorGUILayout.Space();
            if (Tamanho.intValue < 1)
            {
                Tamanho.intValue = 1;
            }
            if (Tamanho.intValue > 0)
            {
                mostraElementos = true;
            }
            else
            {
                mostraElementos = false;
            }
            #region ReSize
            try
            {
                ResizeList(ListaScripts, Tamanho);
                ResizeList(ListaIndexs, Tamanho);
                ResizeList(ListaObjetos, Tamanho);
                ResizeList(ListaIndex2, Tamanho);
            }
            catch
            {
            }
            #endregion
            #region Arrumar
            if (mostraElementos)
            {
                EditorGUILayout.LabelField("----Inicio----");

                for (int i = 0; i < Tamanho.intValue; i++)
                {

                    ListaIndexs.GetArrayElementAtIndex(i).intValue = EditorGUILayout.Popup(ListaIndexs.GetArrayElementAtIndex(i).intValue, Opcoes);
                    switch (ListaIndexs.GetArrayElementAtIndex(i).intValue)
                    {
                        case 0:
                            EditorGUILayout.PropertyField(ListaScripts.GetArrayElementAtIndex(i));
                            break;
                        case 1:
                            GetScriptFromAnotherGameObject(ListaObjetos.GetArrayElementAtIndex(i), ListaIndex2.GetArrayElementAtIndex(i), i);
                            break;
                    }
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
            #endregion

        }
        #endregion
        #region Scripts e GameObjects
        if (Alvo.decisao == ChamarComponent.TipoDeAcao.ScriptsEGameObject)
        {
            EditorGUILayout.LabelField("Quantos Objetos/Scripts?");
            EditorGUILayout.PropertyField(Tamanho);
            EditorGUILayout.Space();
            if (Tamanho.intValue < 1)
            {
                Tamanho.intValue = 1;
            }
            if (Tamanho.intValue > 0)
            {
                mostraElementos = true;
            }
            else
            {
                mostraElementos = false;
            }
            #region ReSize
            try
            {
                ResizeList(ListaScripts, Tamanho);
                ResizeList(ListaIndexs, Tamanho);
                ResizeList(ListaObjetos, Tamanho);
                ResizeList(ListaIndex2, Tamanho);
            }
            catch
            {
            }
            #endregion
            #region Arrumar
            if (mostraElementos)
            {
                EditorGUILayout.LabelField("----Inicio----");

                for (int i = 0; i < Tamanho.intValue; i++)
                {

                    ListaIndexs.GetArrayElementAtIndex(i).intValue = EditorGUILayout.Popup(ListaIndexs.GetArrayElementAtIndex(i).intValue, Opcoes2);
                    switch (ListaIndexs.GetArrayElementAtIndex(i).intValue)
                    {
                        case 0:
                            EditorGUILayout.PropertyField(ListaScripts.GetArrayElementAtIndex(i));
                            break;
                        case 1:
                            GetScriptFromAnotherGameObject(ListaObjetos.GetArrayElementAtIndex(i), ListaIndex2.GetArrayElementAtIndex(i), i);
                            break;
                        case 2:
                            if (Alvo.Scripts[i] != null)
                            {
                                Alvo.Scripts.RemoveAt(i);
                            }
                            EditorGUILayout.PropertyField(ListaObjetos.GetArrayElementAtIndex(i));
                            break;
                    }
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
            #endregion
        }
        #endregion
        serializedObject.ApplyModifiedProperties();
    }
    void ResizeList(SerializedProperty propt, SerializedProperty Size)
    {
        if (Size.intValue != propt.arraySize)
        {
            while (Size.intValue > propt.arraySize)
            {
                propt.InsertArrayElementAtIndex(propt.arraySize);
            }
            while (Size.intValue < propt.arraySize)
            {
                propt.DeleteArrayElementAtIndex(propt.arraySize - 1);
            }
        }
    }
    void ResizeList(SerializedProperty propt, int Size)
    {
        if (Size != propt.arraySize)
        {
            while (Size > propt.arraySize)
            {
                propt.InsertArrayElementAtIndex(propt.arraySize);
            }
            while (Size < propt.arraySize)
            {
                propt.DeleteArrayElementAtIndex(propt.arraySize - 1);
            }
        }
    }
    void GetScriptFromAnotherGameObject(SerializedProperty GameObject, SerializedProperty Index2, int i)
    {
        EditorGUILayout.PropertyField(GameObject);
        if (Alvo.objs[i] != null)
        {
            string[] opcoes = new string[Alvo.objs[i].GetComponents<MonoBehaviour>().Length];
            for (int j = 0; j < opcoes.Length; j++)
            {
                opcoes[j] = Alvo.objs[i].GetComponents<MonoBehaviour>()[j].ToString();
            }
            Index2.intValue = EditorGUILayout.Popup(Index2.intValue, opcoes);
            Alvo.Scripts[i] = Alvo.objs[i].GetComponents<MonoBehaviour>()[Index2.intValue];
            EditorGUILayout.LabelField("Script Selecionado : " + Alvo.Scripts[i].ToString());
        }
    }
}
