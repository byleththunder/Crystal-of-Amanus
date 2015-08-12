using UnityEngine;
using System.Collections;

public class DeadlyCylinder : MonoBehaviour {
    public float YMin = 1;
    bool StartPuzzle = false;
    public float Impulso = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= YMin && !StartPuzzle)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -Impulso), ForceMode.Impulse);
            StartPuzzle = true;
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
