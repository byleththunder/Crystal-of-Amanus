using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SelectConteiner : MonoBehaviour {
    public string OpcaoNome;
    public Image Painel;
    public TipoOpcoes tipo;
    public float scale;
	// Use this for initialization
	void Start () {
       
        Painel = gameObject.GetComponent<Image>();
        Text nome = GetComponentInChildren<Text>();
        nome.text = OpcaoNome;
        RectTransform rc = GetComponent<RectTransform>();
        scale = rc.rect.bottom*2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ChangeColor(bool select)
    {
        if (select)
        {
            Painel.color = new Color(Color.red.r, Color.red.g, Color.red.b, 100.0f);
        }
        else
        {
            Painel.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0f);
        }
    }
}
