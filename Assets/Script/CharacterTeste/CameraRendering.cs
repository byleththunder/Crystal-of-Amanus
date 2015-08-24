using UnityEngine;
using System.Collections;

public class CameraRendering : MonoBehaviour
{
    //Estou utilizando um raycast para saber o que tem na frente da camera
    Ray raio;
    // Os GameObjects são para comparação, só.
    GameObject LastObject, NewObject;
	public GameObject Personagem;
    // Use this for initialization
    void Start()
    {
        Personagem = GameObject.FindGameObjectWithTag("Player");
        print(Personagem.name);
    }

    // Update is called once per frame
    void Update()
    {
	FollowCamera();
        raio = new Ray(transform.position, transform.forward);//O Raio vai ir na direção em que a câmera aponta.
        RaycastHit hit;
        if (Physics.Raycast(raio, out hit, 8f))
        {
            Debug.DrawLine(raio.origin, hit.point);
            //Aqui eu começo passando o objeto para uma das variaveis
            NewObject = hit.collider.gameObject;
            if (NewObject != null)
            {
                Color _tempC = NewObject.GetComponent<Renderer>().material.color;
                if(_tempC.a == 1f )
                {
                    NewObject.GetComponent<Renderer>().material.color = new Color(_tempC.r, _tempC.g, _tempC.b, 0.5f);
                }
                    
                    
            }
            if (LastObject != null)
            {
                //Caso o objeto que eu estou vendo, seja diferente do objeto que eu estava vendo...
                if (LastObject != NewObject)
                {
                    Color _tempC = NewObject.GetComponent<Renderer>().material.color;
                    if (_tempC.a == 0.5f)
                    {
                        NewObject.GetComponent<Renderer>().material.color = new Color(_tempC.r, _tempC.g, _tempC.b, 1f);
                    }
                    
                }
            }
            LastObject = NewObject;
        }else
        {
            //Caso eu não detecte nenhum objeto na frente da câmera, eu venho para cá.
            if (LastObject != null)
            {
                //print("Não Colidiu com nada");
                
                    Color _tempC = NewObject.GetComponent<Renderer>().material.color;
                    if (_tempC.a == 0.5f)
                    {
                        NewObject.GetComponent<Renderer>().material.color = new Color(_tempC.r, _tempC.g, _tempC.b, 1f);
                    }
                    //Se o ultimo Objeto pertencer a Layer Paredes, eu reabilito(?) a mesh.
                    
               
            }
        }

    }
	void FollowCamera()
    {
        if (Personagem != null)
        {
            Vector3 tempC = new Vector3(transform.position.x, Personagem.transform.position.y + 10, Personagem.transform.position.z - 8);
            Vector3 tempP = new Vector3(Personagem.transform.position.x, Personagem.transform.position.y + 10, Personagem.transform.position.z - 8);
            transform.position = Vector3.Lerp(tempC, tempP, Time.deltaTime * 2);
            transform.eulerAngles = new Vector3(45, 0, 0);
        }
    }
}
