using UnityEngine;
using System.Collections;

public class MonsterRespawn : MonoBehaviour {

    //Quais monstros quer que apareçam
    public Monster[] PrefabsMonster = new Monster[1];
    //Quantos deles quer que apareçam;
    public int NumberOfMonsters = 1;
    //Lista de Monstros.
    Monster[] Infield;
    //Tempo aleatorio.
    int Tempo = 0;
	// Use this for initialization
	void Start () {
        Infield = new Monster[NumberOfMonsters];
        Tempo = Random.Range(2, 5);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!IsInvoking("FillField"))
        {
            Invoke("FillField", Tempo);
        }
	}
    void FillField()
    {
        for (int i = 0; i < NumberOfMonsters; i++ )
        {
            if (Infield[i] == null)
            {
                Infield[i] = (Monster)Instantiate(PrefabsMonster[Random.Range(0, NumberOfMonsters)]);
                Infield[i].transform.SetParent(transform);
                Infield[i].transform.position = new Vector3(transform.position.x + Random.Range(0, 5), transform.position.y, transform.position.z + Random.Range(0, 5));
                Infield[i].transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
   
    

}
