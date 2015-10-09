using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Scripts/VisualScripts/Quests")]
public class VQuest : MonoBehaviour {

    public VCQuest Prefab;
    List<VCQuest> Lista = new List<VCQuest>();
    MyQuest Q;
	// Use this for initialization
	void Start () {
        Q = GameObject.FindGameObjectWithTag("Player").GetComponent<MyQuest>();
	}
	
	// Update is called once per frame
    void LateUpdate()
    {
        var quantidade = Q.QuestsComprimento();
        for (int i = 0; i < quantidade; i++)
        {
            if (i + 1 > Lista.Count)
            {
                Lista.Add(Instantiate(Prefab));
                Lista[i].transform.SetParent(gameObject.transform, false);
                Lista[i].Indice = i;
            }
            if (Lista[i] == null)
            {
                Lista.RemoveAt(i);
            }
            else
            {
                Lista[i].Indice = i;
            }
        }
    }
}
