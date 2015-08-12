using UnityEngine;
using System.Collections;

public class InitialPositionCharacter : MonoBehaviour {

    public GameObject prefab;
    public GameObject obj;
    public PlayerMovement player;
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
       player = obj.GetComponent<PlayerMovement>();
       if (player != null)
       {
           player.Ini = transform.position;
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
                    player.StatsChange(-(player.VidaTotal), -player.AmanusTotal);
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
	
	
}
