using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainWindow : MonoBehaviour
{

    Target Personagem;
    //UI = User Interface
    public Text _Nome;
    public Image Painel_HP, Painel_Amanus;
    
    float XTotal_HP = 0;
    float XTotal_Amanus = 0;
	bool change = false;
    //Awake
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameObject _pers = GameObject.FindGameObjectWithTag("Player");
        if (_pers != null)
            Personagem = (Target)_pers.GetComponent(typeof(Target));
    }
    void Start()
    {
        
    }
	void Update()
	{
		if (gameObject.activeInHierarchy && !change) {
			if (Personagem != null)
			{
				_Nome.text = Personagem.Nome;

                float VidaX = (float)Personagem.Vida / (float)Personagem.VidaTotal;
                float AmanusX = (float)Personagem.Amanus / (float)Personagem.AmanusTotal;
                VidaX = Mathf.Clamp(VidaX, 0, 1f);
                AmanusX = Mathf.Clamp(AmanusX, 0, 1f);
                Painel_HP.fillAmount = VidaX;
                Painel_Amanus.fillAmount = AmanusX;
				change = true;
			}
		}

	}
    void OnEnable()
    {
       
        if (Personagem != null)
        {
            _Nome.text = Personagem.Nome;
            RectTransform _Painel_HP = Painel_HP.GetComponent<RectTransform>();
            RectTransform _Painel_Amanus = Painel_Amanus.GetComponent<RectTransform>();
            _Painel_HP.anchoredPosition = new Vector2(XTotal_HP + CalcularPos(Personagem.Vida, Personagem.VidaTotal, _Painel_HP.sizeDelta.x), 0);
            _Painel_Amanus.anchoredPosition = new Vector2(XTotal_Amanus + CalcularPos(Personagem.Amanus, Personagem.AmanusTotal, _Painel_Amanus.sizeDelta.x), 0);
        }
    }
    float CalcularPos(float Stats, float StatsMax, float PosMax)
    {
        float y = (Stats * 100) / StatsMax;
        float x = ((PosMax * y) / 100) - PosMax;
        return x;
    }
}
