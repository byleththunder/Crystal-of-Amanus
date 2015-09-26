#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class MenuItensBuilds : EditorWindow
{

    GameObject InScene, NewModule, temp;
    public GameObject[] Prefabs;
    int indice, conectIndice, conectIndice2;
    string[] nomes, conectoresNomes, conectoresNomes2;
    ModuleConnector escolhido, escolhidoNew;
    [MenuItem("Gerar/Conectar Objetos em cena &-0")]
    static void AbrirJanela()
    {
        GetWindow<MenuItensBuilds>("Conector");
    }

    void OnGUI()
    {
        Prefabs = Resources.LoadAll<GameObject>("Montagem/");
        nomes = new string[Prefabs.Length];
        for (int i = 0; i < nomes.Length; i++)
        {
            nomes[i] = Prefabs[i].ToString();
        }
        EditorGUILayout.LabelField("Ambos objetos devem ter o ModuloConnector como filho.");
        InScene = EditorGUILayout.ObjectField("Objeto 01", InScene, typeof(GameObject), true) as GameObject;
        EditorGUILayout.Space();
        if (InScene != null)
        {
            EditorGUILayout.LabelField("Conectores:");
            var _connect = InScene.GetComponentsInChildren<ModuleConnector>();
            conectoresNomes = new string[_connect.Length];
            for (int i = 0; i < conectoresNomes.Length; i++)
            {
                conectoresNomes[i] = _connect[i].ToString();
            }
            EditorGUILayout.Space();
            conectIndice = EditorGUILayout.Popup(conectIndice, conectoresNomes);
            escolhido = _connect[conectIndice];
        }
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Prefabs:");
        indice = EditorGUILayout.Popup(indice, nomes);
        EditorGUILayout.Space();
        try
        {
            NewModule = Prefabs[indice];
        }
        catch { }
        if (GUILayout.Button("Instanciar"))
        {
            Debug.Log("Criando");
            temp = Instantiate(NewModule);
            
        }
        try
        {
            if (temp != null)
            {
                EditorGUILayout.LabelField("Conectores:");
                var _connect = temp.GetComponentsInChildren<ModuleConnector>();
                conectoresNomes2 = new string[_connect.Length];
                for (int i = 0; i < conectoresNomes2.Length; i++)
                {
                    conectoresNomes2[i] = _connect[i].ToString();
                }
                EditorGUILayout.Space();
                conectIndice2 = EditorGUILayout.Popup(conectIndice2, conectoresNomes2);
                escolhidoNew = _connect[conectIndice2];

            }
        }catch
        {
            Debug.LogError("Não conseguiu acessar o filho");
        }
        EditorGUILayout.Space();
        if (temp != null)
        {
            if (GUILayout.Button("Conectar"))
            {
                if (InScene != null && temp != null)
                {
                    try
                    {


                        ModuleConnector _oldE = escolhido;
                        ModuleConnector _newE = escolhidoNew;
                        MatchExits(_oldE, _newE);
                        _oldE.gameObject.SetActive(false);
                        _newE.gameObject.SetActive(false);
                        InScene = temp;
                        NewModule = null;
                        temp = null;
                    }
                    catch
                    {
                        Debug.LogError("Não é possivel conectar objetos que estão fora de cena.");
                    }

                }
            }
        }
    }
    void MatchExits(ModuleConnector oldExit, ModuleConnector newExit)
    {
        var newModule = newExit.transform.parent;
        var forwardVectorToMatch = -oldExit.transform.forward;
        var correctiveRotation = Azimuth(forwardVectorToMatch) - Azimuth(newExit.transform.forward);
        newModule.RotateAround(newExit.transform.position, Vector3.up, correctiveRotation);
        var correctiveTranslation = oldExit.transform.position - newExit.transform.position;
        newModule.transform.position += correctiveTranslation;
    }
    static float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }

}
#endif