using UnityEngine;
using System.Collections;

public class CharacterMove :MonoBehaviour, ITarget
{

    public string Nome { get { return "Eran Airikina"; } }
    public int Vida { get { return HP; } set { Vida = HP; } }
    public int VidaTotal { get { return 100; } }
    public int Amanus { get { return MP; } set { Amanus = MP; } }
    public int Ataque { get { return 50; } }
    public int AmanusTotal { get { return 100; } }
    public enum CameraDirection { Front, Back };
    public TargetVision visao { get; set; }
    public GameObject obj { get { return this.gameObject; } }
    public Item[] Equipamentos { get; set; }
    public int velocity = 5;
    public bool Inverse = false;
    public ITarget Alvo;
	public bool DontMove = true;
    int HP, MP;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start()
    {
        HP = VidaTotal;
        MP = AmanusTotal;
        
    }
    public void StatsChange(int _vida, int _Amanus)
    {
        HP -= _vida;
        MP -= _Amanus;
    }
    


    // Update is called once per frame
    void Update()
    {

		if (!DontMove) {
			if (Input.GetKeyDown (KeyCode.Q)) {
				transform.eulerAngles -= new Vector3 (0, 45, 0);
			}
			if (Input.GetKeyDown (KeyCode.E)) {
				transform.eulerAngles += new Vector3 (0, 45, 0);
			}

			if (Inverse) {
				if (Input.GetKey (KeyCode.UpArrow)) {
					transform.localPosition += (Vector3.back * Time.deltaTime) * velocity;

				}
				if (Input.GetKey (KeyCode.DownArrow)) {
					transform.localPosition += ((Vector3.forward * Time.deltaTime) * velocity);
				}
				if (Input.GetKey (KeyCode.LeftArrow)) {
					transform.localPosition += ((Vector3.right * Time.deltaTime) * velocity);
				}
				if (Input.GetKey (KeyCode.RightArrow)) {
					transform.localPosition += ((Vector3.left * Time.deltaTime) * velocity);
				}
			} else {
				if (Input.GetKey (KeyCode.UpArrow)) {
					transform.localPosition += ((Vector3.forward * Time.deltaTime) * velocity);
				}
				if (Input.GetKey (KeyCode.DownArrow)) {
					transform.localPosition += ((Vector3.back * Time.deltaTime) * velocity);
				}
				if (Input.GetKey (KeyCode.LeftArrow)) {
                
					transform.localPosition += ((Vector3.left * Time.deltaTime) * velocity);
				}
				if (Input.GetKey (KeyCode.RightArrow)) {
					transform.localPosition += ((Vector3.right * Time.deltaTime) * velocity);
                
				}
			}
		}
    }

    void OnCollisionEnter(Collision col)
    {
        if (Input.GetButtonDown("Action"))
        {
            try
            {
                Alvo = (ITarget)col.gameObject.GetComponent(typeof(ITarget));
            }
            catch
            {
            }
        }
    }
}
