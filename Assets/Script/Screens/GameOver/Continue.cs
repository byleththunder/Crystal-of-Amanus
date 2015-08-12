using UnityEngine;
using System.Collections;

public class Continue : MonoBehaviour {
    public static bool VoltarJogo = false;
    void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            ContinuarJogo();
        }
    }
    public void ContinuarJogo()
    {
        LoadingScreen.NextLevelName = "CenaTestes";
        Application.LoadLevel("LoadingScene");
    }
}
