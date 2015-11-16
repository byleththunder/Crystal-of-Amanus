using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/MiniMap Script/MiniMap")]
public class MiniMap : MonoBehaviour {

    
    public Transform Player;
    public Transform MiniMapCamera;
    public RectTransform Ponteiro;

    Character Eran;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Eran = Player.gameObject.GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        MiniMapCamera.transform.position = new Vector3(Player.transform.position.x, MiniMapCamera.transform.position.y, Player.transform.position.z);
        Ponteiro.localEulerAngles = new Vector3(0, 0, AngleVisao(Eran.visao));

	}
    float AngleVisao(TargetVision v)
    {
        float angulo = 0;
        switch(v)
        {
            case TargetVision.Back:
                angulo = 0;
                break;
            case TargetVision.Front:
                angulo = 180;
                break;
            case TargetVision.Left:
                angulo = 90;
                break;
            case TargetVision.Right:
                angulo = -90;
                break;
        }

        return angulo;
    }
    

}
