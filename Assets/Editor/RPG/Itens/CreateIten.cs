#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateIten : EditorWindow
{
    static Item _iten;
    [MenuItem("RPG/Iten/Create Iten")]
    static void OpenWindow()
    {
        GetWindow<CreateIten>();
        _iten = ScriptableObject.CreateInstance<Item>();
    }
    
    void OnGUI()
    {
       
        try
        {
            //Imagem
            _iten.Img = EditorGUILayout.ObjectField("Ico", _iten.Img, typeof(Sprite), false) as Sprite;
            //Nome
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Name", GUILayout.MaxWidth(100));
            _iten.Nome = EditorGUILayout.TextArea(_iten.Nome);
            EditorGUILayout.EndHorizontal();
            //Descricao
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Description", GUILayout.MaxWidth(100));
            _iten.Descricao = EditorGUILayout.TextArea(_iten.Descricao);
            EditorGUILayout.EndHorizontal();
            _iten.Tipo = (Item.TipoDeItem)EditorGUILayout.EnumPopup(_iten.Tipo);
            #region Type
            if (_iten.Tipo == Item.TipoDeItem.Potion)
            {
                _iten.NameImg = EditorGUILayout.ObjectField("Name Img", _iten.NameImg, typeof(Sprite), false) as Sprite;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Life", GUILayout.MaxWidth(100));
                _iten.Vida = EditorGUILayout.IntField(_iten.Vida);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Amanus", GUILayout.MaxWidth(100));
                _iten.Amanus = EditorGUILayout.IntField(_iten.Amanus);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Price", GUILayout.MaxWidth(100));
                _iten.Preco = EditorGUILayout.IntField(_iten.Preco);
                EditorGUILayout.EndHorizontal();
            }
            else if (_iten.Tipo == Item.TipoDeItem.Equipamento)
            {
                _iten.TipoDeEquipamentos = (Item.EquipmentTypes)EditorGUILayout.EnumPopup(_iten.TipoDeEquipamentos);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Attack", GUILayout.MaxWidth(100));
                _iten.Ataque = EditorGUILayout.IntField(_iten.Ataque);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Defense", GUILayout.MaxWidth(100));
                _iten.Defesa = EditorGUILayout.IntField(_iten.Defesa);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Life", GUILayout.MaxWidth(100));
                _iten.Vida = EditorGUILayout.IntField(_iten.Vida);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Amanus", GUILayout.MaxWidth(100));
                _iten.Amanus = EditorGUILayout.IntField(_iten.Amanus);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Price", GUILayout.MaxWidth(100));
                _iten.Preco = EditorGUILayout.IntField(_iten.Preco);
                EditorGUILayout.EndHorizontal();
            }
            else if (_iten.Tipo == Item.TipoDeItem.NaoConsumivel)
            {
                //Nao consumivel
            }
            else if (_iten.Tipo == Item.TipoDeItem.Trigger)
            {
                //Nao consumivel
            }
            #endregion
        }
        catch
        {
            Debug.Log(_iten == null);
        }
        if (GUILayout.Button("Create"))
        {
            if (!string.IsNullOrEmpty(_iten.Nome))
            {
                _iten.SetItem();
                AssetDatabase.CreateAsset(_iten, "Assets/Resources/Itens/" + _iten.Nome + ".asset");
                AssetDatabase.SaveAssets();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = _iten;
                Close();
            }
            else
            {
                Debug.LogError("The Name field is empty");
            }
        }

    }
}
#endif
