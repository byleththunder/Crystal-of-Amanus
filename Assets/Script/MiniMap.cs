using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

    public Vector3 MaxSize, MinSize;
    public RectTransform Map;
    public Transform Player;
    public Vector2 _TamanhoTerreno;
    public RectTransform PlayerIco;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _TamanhoTerreno = new Vector2(Mathf.Abs(MaxSize.x) + Mathf.Abs(MinSize.x), Mathf.Abs(MaxSize.z) + Mathf.Abs(MinSize.z));
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 playerpos = new Vector2(Mathf.Abs(Mathf.Abs(Player.position.x) + MinSize.x), Mathf.Abs(Mathf.Abs(Player.position.z) + MinSize.z));
        //print(playerpos);
        Vector2 Mapa = new Vector2(-Invert(Map.sizeDelta.x, Relacao(_TamanhoTerreno.x, playerpos.x)), Invert(Map.sizeDelta.y, Relacao(_TamanhoTerreno.y, playerpos.y)));
        Map.anchoredPosition = Mapa;
        if(PlayerIco != null)
        {
            PlayerIco.anchoredPosition = Mapa;
        }
	}
    float Relacao(float Max, float Actual)
    {
        return (100f * Actual) / Max;
    }
    float Invert(float Max, float percent)
    {
        return (Max * percent) / 100;
    }

}
