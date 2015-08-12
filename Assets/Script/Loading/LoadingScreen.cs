using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static string NextLevelName;
    public Image Barra;
    AsyncOperation Op;
    public Image Circle;
    float girar;
    public float TimeDelay;
    // Use this for initialization
    void Start()
    {
        Op = Application.LoadLevelAsync(NextLevelName);
        //Op = Application.LoadLevelAdditiveAsync(NextLevelName);
        Op.allowSceneActivation = false;

    }

    // Update is called once per frame
    void Update()
    {
        Barra.fillAmount = Op.progress;
        girar = ((girar + 1) % 100);
        Circle.fillAmount = girar / 100;
        if (Op.progress >=0.9f)
        {
            //Op.allowSceneActivation = true;
            if (!IsInvoking("ChangeStage"))
            {
                Invoke("ChangeStage", 5);
            }
        }
    }
    void ChangeStage()
    {
        Op.allowSceneActivation = true;
        Destroy(gameObject);
    }
    float CalcularPos(float percent, float PosMax)
    {

        float x = ((PosMax * percent)) - PosMax;
        return x;
    }
}
