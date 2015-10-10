using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Personagens e Monstros/Personagens Sripts/Posicao Inicial")]
public class InitialPositionCharacter : MonoBehaviour {

    public GameObject prefab;
    public GameObject obj;
    public Character player;
	// Use this for initialization
	void Awake () {
       obj = GameObject.FindGameObjectWithTag("Player");
       if (obj != null)
        {
            obj.transform.position = transform.position;
            
        }else
        {
          obj =(GameObject)Instantiate(prefab, transform.position,transform.rotation);
        }
       player = obj.GetComponent<Character>();
       if (player != null)
       {
           player.CheckPointPosition = transform.position;
       }
	}
    void Update()
    {
       
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,transform.position+ new Vector3(2f,0,0));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0f, 2f, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0f, 0, 2f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
	
	
}
