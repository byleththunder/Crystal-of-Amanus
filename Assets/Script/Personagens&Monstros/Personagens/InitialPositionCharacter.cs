using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

[AddComponentMenu("Scripts/Personagens e Monstros/Personagens Sripts/Posicao Inicial")]
public class InitialPositionCharacter : MonoBehaviour {

    
    public GameObject prefab;
    private GameObject Instancia;
    private Character player;
	// Use this for initialization
	void Start () {
       Instancia = GameObject.FindGameObjectWithTag("Player");
       
       if (Instancia != null)
        {
            Instancia.transform.position = transform.position;
            
        }else
        {
            print("Nulo");
          Instancia =(GameObject)Instantiate(prefab, transform.position,transform.rotation);
        }
       player = Instancia.GetComponent<Character>();
       if (player != null)
       {
           player.CheckPointPosition = transform.position;
       }
	}
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.localPosition, transform.localPosition + new Vector3(2f, 0, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.localPosition, transform.localPosition + new Vector3(0f, 2f, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.localPosition, transform.localPosition + new Vector3(0f, 0, 2f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.localPosition, 0.5f);
    }
    #if UNITY_EDITOR
    [MenuItem("GameObject/Scripts/Initial Position")]
	static void CreateInitalObject()
    {
        GameObject obj = new GameObject();
        obj.AddComponent<InitialPositionCharacter>();
        obj.name = "InitialPosition";
    }
#endif
}
