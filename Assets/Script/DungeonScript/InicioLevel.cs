using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("Scripts/Dungeon Scripts/Inicio/Inicio Level")]
public class InicioLevel : DungeonScript
{

    /// <summary>
    /// É aqui que todo evento do inicio do jogo vai acontecer.
    /// Quando a cena começar, o Fade(Painel no canvas) vai ter o alfa aumentado e em seguida vai começar a conversa entre
    /// o Eran e o Damoh. Quando a conversa terminar, eu aumento o alfa do fade no máximo e troco de cena.
    /// </summary>

    public Image painel;
    Color Fade;
    float timer = 0;
    bool comecou = false;
    GameObject obj;
    // Use this for initialization
    void Start()
    {
        //Calendar.IncreaseStage(1);
        MessageBox.PrefabPath = "CaixaDeTexto";
        //Conversa();
        Fade = new Color(painel.color.r, painel.color.g, painel.color.b,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (comecou)
        {
            if (!obj.activeInHierarchy)
            {
                if (Fade.a < 1)
                {
                    timer += Time.deltaTime;
                    if (timer > 0.1f)
                    {
                        timer = 0;
                        Fade.a += 0.05f;
                    }
                    painel.color = Fade;
                }
                else
                {
                    LoadingScreen.NextLevelName = "Lv_Ruinas";
                    Application.LoadLevel("LoadingScene");
                }
            }
        }else
        {
            Mundanca();
        }
    }
    void Conversa()
    {
        
        MessageBox.Instance.WriteMessage("Ainda faltam alguns dias para chegarmos ao Reino de Cernuno.", "Mercante");
        MessageBox.Instance.WriteMessage("Essa não é uma rota comercial muito conhecida – por que você está fazendo esse caminho?", "Mercante");
        MessageBox.Instance.WriteMessage("Ouvi histórias sobre um tesouro perdido do rei. Uma diligência real levava fortunas em dois baús, quando foi atacada por monstros.", "Guerreiro");
        MessageBox.Instance.WriteMessage("Os soldados fugiram e os monstros levaram tudo da carruagem...", "Guerreiro");
        MessageBox.Instance.WriteMessage("Deixa-me adivinhar: o tesouro está perto de Cernuno e você quer encontra-lo? Para fazer riqueza rápida?", "Mercante");
        MessageBox.Instance.WriteMessage("Não só pela riqueza. Também quero conquistar uma parte do mundo para mim, ouvir os bardos cantarem sobre meu nome em aventuras.", "Guerreiro");
        MessageBox.Instance.WriteMessage("Hahaha! Você diz coisas engraçadas para alguém que anda apenas com uma espada. Por qual nome devo atender vossa alteza?", "Mercante");
        MessageBox.Instance.WriteMessage("Meu nome é Eran.", "Eran");
        MessageBox.Instance.WriteMessage("Vou me lembrar do seu nome, mercenário. Talvez eu também consiga aproveitar um pouco da sua fortuna! Sou Damoh, o mercador viajante.", "Damoh");
        obj = MessageBox.Instance.gameObject;
        comecou = true;
    }
    void Mundanca()
    {
        
        if (Fade.a < 0.2f)
        {
            timer += Time.deltaTime;
            if (timer > 0.1f)
            {
                timer = 0;
                Fade.a += 0.05f;
            }
            painel.color = Fade;
        }else
        {
            Conversa();

        }
    }
}
