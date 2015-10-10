using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/CameraScripts/Camera Follow")]
public class CameraRendering : MonoBehaviour
{
    ///<summary "Como Funciona ?">
    ///Eu acho o personagem na cena, e coloco a camera atrás dele em 45º em X.
    ///</summary>

    //Variaveis
	public GameObject Personagem;
    public float angle = 45f;
    public Vector3 Ajustes = new Vector3(0, 10, -8);
    // Métodos
    void Start()
    {
        
        Personagem = GameObject.FindGameObjectWithTag("Player");
        try
        {
            transform.position = GameObject.Find("InitialPosition").transform.position;
        }
        catch { Debug.LogError("Não tem um GameObject de nome InitialPosition"); }
    }
    void Update()
    {
	    FollowCamera();
    }
	void FollowCamera()
    {
        if (Personagem != null)
        {
            Vector3 tempC = new Vector3(transform.position.x+Ajustes.x, Personagem.transform.position.y + Ajustes.y, Personagem.transform.position.z + Ajustes.z);
            Vector3 tempP = new Vector3(Personagem.transform.position.x + Ajustes.x, Personagem.transform.position.y + Ajustes.y, Personagem.transform.position.z +Ajustes.z);
            transform.position = Vector3.Lerp(tempC, tempP, Time.deltaTime * 2);
            transform.eulerAngles = new Vector3(angle, 0, 0);
        }
    }
}
