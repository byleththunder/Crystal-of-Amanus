using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("Scripts/Loading/Loading Screen")]
public class LoadingScreen : MonoBehaviour
{
    public static string NextLevelName;
    public Text Dica;
    AsyncOperation Op;
   
    // Use this for initialization
    void Start()
    {
        try
        {
            if (Game.current != null)
            {
                Game.current.LastPlace = NextLevelName;
            }
            SaveLoad.Save();
        }catch
        {

        }
        Op = Application.LoadLevelAsync(NextLevelName);
        //Op = Application.LoadLevelAdditiveAsync(NextLevelName);
        Op.allowSceneActivation = true;
        if (NextLevelName == "DungeonAleatoria")
        {
            Dica.text = "Dica: Quando quiser sair da Masmorra Aleatória, basta ir em diração a uma chama azul.";
        }else
        {
            Dica.text = "Dica: Aperte F1 para acessar a janela de comandos.";
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Op.isDone)
        {
            Destroy(gameObject);
        }
    }
    
    
}
