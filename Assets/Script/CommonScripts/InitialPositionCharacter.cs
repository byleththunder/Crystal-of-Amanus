using UnityEngine;
using System.Collections;

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
        if (player != null)
        {
            if (player.Vida <= 0)
            {
                try
                {
                    player.HealOrDamage(-(player.VidaTotal), -player.AmanusTotal);
                    Application.LoadLevel("GameOver");
                }catch
                {
                    Debug.LogError("Não foi possivel fazer o GameOver");
                }

            }
        }else
        {
           
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,transform.position+ new Vector3(1f,0,0));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0f, 1f, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0f, 0, 1f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
	
	
}
