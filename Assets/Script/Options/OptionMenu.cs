using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionMenu : MonoBehaviour {

    bool FullScreen = false;
    public GameObject bt_Prefab;
    public GameObject Painel;
    public Text QualityText;
    public Toggle[] TG = new Toggle[2];
    public Slider[] SD = new Slider[2];
    
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("FullScreen")) FullScreen = (PlayerPrefs.GetString("FullScreen") == "true"?true:false);
        if (PlayerPrefs.HasKey("ScreenWidth") && PlayerPrefs.HasKey("ScreenHeight")) { Screen.SetResolution(PlayerPrefs.GetInt("ScreenWidth"), PlayerPrefs.GetInt("ScreenHeight"), FullScreen); }
        else { Screen.SetResolution(1024, 768, FullScreen); }
        if (PlayerPrefs.HasKey("QualityLevel")) { QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel")); }
        QualityText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
        if (PlayerPrefs.HasKey("VolumeBGM")) { SD[0].value = PlayerPrefs.GetInt("VolumeBGM"); }
        if (PlayerPrefs.HasKey("VolumeSE")) { SD[1].value = PlayerPrefs.GetInt("VolumeSE"); }
        TG[0].isOn = FullScreen;
        TG[1].isOn = !FullScreen;
        CreateBt();

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void CreateBt()
    {
        Painel.GetComponent<RectTransform>().sizeDelta = new Vector2(Painel.GetComponent<RectTransform>().sizeDelta.x, bt_Prefab.GetComponent<RectTransform>().sizeDelta.y * (Screen.resolutions.Length - 1));
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
           GameObject temp = (GameObject) Instantiate(bt_Prefab);
           temp.transform.SetParent(Painel.transform, false);
           RectTransform rct = temp.GetComponent<RectTransform>();
           rct.anchoredPosition -= new Vector2(0, rct.sizeDelta.y * (i-1));
           ResolutionContent reso = temp.GetComponent<ResolutionContent>();
           reso.Indice = i;
           reso.Nome.text = Screen.resolutions[i].width + " X " + Screen.resolutions[i].height;
        }
    }
    public void IsFullScreen(bool swtch)
    {
        print(swtch);
        if (!Application.isPlaying) return;
        PlayerPrefs.SetString("FullScreen", swtch.ToString());
        FullScreen = swtch;
        Screen.fullScreen = swtch;
    }
    public void SelectResolution(int i)
    {
        if (!Application.isPlaying) return;
        PlayerPrefs.SetInt("ScreenWidth", Screen.resolutions[i].width);
        PlayerPrefs.SetInt("ScreenHeight", Screen.resolutions[i].height);
        Screen.SetResolution(Screen.resolutions[i].width, Screen.resolutions[i].height, FullScreen);
    }
    public void IncreseQuality()
    {
        if (!Application.isPlaying) return;
        QualitySettings.IncreaseLevel();
        QualityText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
        PlayerPrefs.SetInt("QualityLevel", QualitySettings.GetQualityLevel());
    }
    public void VolumeBGM(int i)
    {
        if (!Application.isPlaying) return;
        PlayerPrefs.SetInt("VolumeBGM", i);
    }
    public void VolumeSE(int i)
    {
        if (!Application.isPlaying) return;
        PlayerPrefs.SetInt("VolumeSE", i);
    }
    public void DecreaseQuality()
    {
        if (!Application.isPlaying) return;
        QualitySettings.DecreaseLevel();
        QualityText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
        PlayerPrefs.SetInt("QualityLevel", QualitySettings.GetQualityLevel());
    }
}
