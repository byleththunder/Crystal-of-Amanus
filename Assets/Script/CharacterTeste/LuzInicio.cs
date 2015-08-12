using UnityEngine;
using System.Collections;

public class LuzInicio : MonoBehaviour
{
	Ray ray;
	public GameObject compare;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		ray = new Ray (transform.position , transform.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {


			if (hit.collider.tag == "frente") {
				if(compare == null)
				{
				Debug.DrawLine (ray.origin, hit.point);
				compare = hit.collider.gameObject;
				EspelhoRefletir reflexo = compare.GetComponent<EspelhoRefletir> ();
				reflexo.LuzBatendo (true);
				}
			} else if (compare != null) {
				if (hit.collider.tag != "Player") {
					if (compare != hit.collider.gameObject) {
						print ("diferete");
						EspelhoRefletir reflexo = compare.GetComponent<EspelhoRefletir> ();
						reflexo.LuzBatendo (false);
						compare = null;
					}
				}
			} 
		}
	}
}
