using UnityEngine;
using System.Collections;

public class AssassinsRock : MonoBehaviour {
    public GameObject Armadilha;
    RockPrank prank;
	// Use this for initialization
	void Start () {
        prank = Armadilha.GetComponent<RockPrank>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            print("fim");

            Destroy(Armadilha);
        }
        if (collider.gameObject.tag == "PrankEnd")
        {

            Destroy(Armadilha);
        }
    }
}
