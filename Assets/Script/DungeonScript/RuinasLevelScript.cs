using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[AddComponentMenu("Scripts/Dungeon Scripts/Ruinas/Ruinas Level")]
public class RuinasLevelScript : DungeonScript
{
    /// <summary>
    /// Esse script só vai controlar a iluminação da cena e salvar o estado dos baus, se estão abertos ou não.
    /// A string Ruinas é aonde será salvo o estado dos baus.
    /// </summary>
    public static string Ruinas = string.Empty;
    public GameObject CenaIni;
    public GameObject Personagem;
    public GameObject Camera;
    Animator animIni;
    public bool wait = true;
    // Use this for initialization
    void Start()
    {
        //Invoke("FimEspera", 1f);
        animIni = CenaIni.GetComponent<Animator>();
        Camera.GetComponent<CameraRendering>().enabled = false;
        Personagem.GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.DontMove;
       
        GameObject[] _obj = GameObject.FindGameObjectsWithTag("Chest");
        for (int i = 0; i < _obj.Length; i++)
        {
            Baus.Add(_obj[i].GetComponent<Chest>());
        }

        try
        {
            if (!string.IsNullOrEmpty(Game.current.RuinaDungeon))
            {
                Ruinas = Game.current.RuinaDungeon;
                Load();
            }
        }
        catch
        {
            Debug.LogWarning("Retirar essa linha após implementar uma janela de Save e Load.");
        }
        //ChangeColorByStage();

    }
    [ContextMenu("Achar todos os Baús da Cena")]
    void GetAllChestOnScene()
    {
        if (Baus.Count > 0) return;

        GameObject[] _obj = GameObject.FindGameObjectsWithTag("Chest");
        for (int i = 0; i < _obj.Length; i++)
        {
            Baus.Add(_obj[i].GetComponent<Chest>());
        }

    }
    [ContextMenu("Achar Directional Light na Scena")]
    void FindDirectinalLight()
    {
        if (LuzPrincipal == null) return;
        LuzPrincipal = System.Array.Find<Light>(GameObject.FindObjectsOfType<Light>(), x => x.type == LightType.Directional);

    }
    // Update is called once per frame
    void FimEspera()
    {
        wait = false;
    }
    void Update()
    {
        if (CenaIni != null)
        {
            if (!wait && CenaIni.active)
            {
                
                    CenaIni.transform.DetachChildren();
                    CenaIni.SetActive(false);
                    Camera.GetComponent<CameraRendering>().enabled = true;
                    Personagem.GetComponent<Character>().EstadoDoJogador = GameStates.CharacterState.Playing;
                    Personagem.GetComponent<Character>().visao = TargetVision.Back;
                
            }
        }
        Save();
    }
    public override void Save()
    {
        string _temp = string.Empty;
        List<bool> ChestOpen = new List<bool>();
        for (int i = 0; i < Baus.Count; i++)
        {
            ChestOpen.Add(Baus[i].Open);
        }
        _temp += SaveGameState.SalvarLista<bool>(ChestOpen, "ChestOpen");
        Ruinas = _temp;
    }
    public override void Load()
    {
        List<bool> ChestOpen = LoadGameState.LoadBooleanList(Ruinas, "ChestOpen");
        for (int i = 0; i < Baus.Count; i++)
        {
            Baus[i].Open = ChestOpen[i];
        }
    }
}
