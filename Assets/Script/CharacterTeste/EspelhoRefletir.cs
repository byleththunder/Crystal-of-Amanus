using UnityEngine;
using System.Collections;

public class EspelhoRefletir : MonoBehaviour
{
    Ray ray;
    public ParticleSystem particulas;
    public bool refletindo = false;
	public GameObject compare;
	public EspelhoRefletir Origem;
    public bool ignore = false;
    public bool Comecou = false;
	int IndiceTeste = 0;
    // Use this for initialization
    void Start()
    {
        particulas.loop = true;
    }

    // Update is called once per frame
    void Update()
    {

        ray = new Ray(transform.position + transform.forward, transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100))
        {
			if (hit.collider.tag == "frente")
			{
				Debug.DrawLine(ray.origin, hit.point);
				compare = hit.collider.gameObject;
                
                if (!Comecou)
                {

                    EspelhoRefletir reflexo = compare.GetComponent<EspelhoRefletir>();
                    reflexo.LuzBatendo(true, this);
                    Comecou = true;

                }
               
			}else if (compare != null)
			{
				if (hit.collider.tag != "Player")
				{
					if (compare != hit.collider.gameObject)
					{

						EspelhoRefletir reflexo = compare.GetComponent<EspelhoRefletir>();
						reflexo.LuzBatendo(false,this);
						compare = null;
					}
				}
			} 
			
			
			
        }
        if (!ignore)
        {
            Reflexao();
        }
    }
    void Reflexao()
    {
        if (this.refletindo == true)
        {
            if (particulas.isStopped)
            {
                particulas.Play();
            }

            return;
        }
        else
        {
            if (particulas.isPlaying)
            {
                particulas.Stop();
				if(Origem!=null)
				{
                 Origem.Comecou= false;
				}
            }
            return;
        }
    }
    public void LuzBatendo(bool valor, EspelhoRefletir _origem)
    {
        refletindo = valor;
		Origem = _origem;
    }
	public void LuzBatendo(bool valor)
	{
		refletindo = valor;

	}
	/*void OnParticleCollision(GameObject other)
    {
        if (other.name == "Luz")
        {
            if (particulas.isStopped || particulas.isPaused)
            {
                particulas.Play();
                //particulas.loop = true;
            }
        }

    }*/

}
