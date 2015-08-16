using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Temp_HUD : MonoBehaviour {
    Target Personagem;
    public Image X_Vida, X_Amanus;
	// Use this for initialization
	void Start () {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
        {
            print("OBJ: " + (obj.name));
            Personagem = (Target)obj.GetComponent(typeof(Target));
            print("Personagem: " + (Personagem != null));
        }
	}
	
	// Update is called once per frame
	
    void FixedUpdate()
    {
        if (Personagem != null)
        {

            float _Vida = (float)Personagem.Vida / (float)Personagem.VidaTotal;
            float _Amanus = (float)Personagem.Amanus / (float)Personagem.AmanusTotal;
            _Amanus = Mathf.Clamp(_Amanus, 0, 1);
            _Vida= Mathf.Clamp(_Vida, 0, 1);
            X_Vida.fillAmount = _Vida;
            X_Amanus.fillAmount = _Amanus;
        }
    }
}
