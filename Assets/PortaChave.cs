using UnityEngine;
using System.Collections;

public class PortaChave : MonoBehaviour
{

    public MonoBehaviour ScriptPorta;
    public string NomeItem;
    // Use this for initialization
    void Start()
    {
        if (ScriptPorta != null)
        {
            ScriptPorta.enabled = false;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            int _indice = col.gameObject.GetComponent<Inventario>().ProcurarItem(NomeItem);
            if(_indice >-1)
            {
                col.gameObject.GetComponent<Inventario>().DiscartItem(_indice);
                ScriptPorta.enabled = true;
                this.enabled = false;
            }
        }
    }
    
}
