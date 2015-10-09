using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResolutionContent : MonoBehaviour
{
    public Text Nome;
    public int Indice = 0;
    public Image sprt;
    public string ScriptObjectName = string.Empty;
    // Use this for initialization
    void Start()
    {
        sprt = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Click()
    {
        GameObject.Find(ScriptObjectName).GetComponent<OptionMenu>().SelectResolution(Indice);
    }
   
}
