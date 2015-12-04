using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/CameraScripts/Camera Follow")]
public class CameraRendering : MonoBehaviour
{
    ///<summary "Como Funciona ?">
    ///Eu acho o personagem na cena, e coloco a camera atrás dele em 45º em X.
    ///</summary>

    //Variaveis
    [SerializeField]
	private GameObject Personagem;
    [Header("Angulo X da Camera")]
    [SerializeField]
    private Vector3 Angle = new Vector3(45, 0, 0);
     [Header("Zoom Perspectiva 1")]
    [SerializeField]
    private Vector3 Zoom1 = new Vector3(0, 3, -2);
     [Header("Zoom Perspectiva 2")]
    [SerializeField]
    private Vector3 Zoom2 = new Vector3(0, 5, -4);
    [Header("Zoom Ortogonal")]
    [SerializeField]
    private Vector3 Zoom3 = new Vector3(0, 10, -8);
    [Range(0,2)]
    public int indice = 0;
    Vector3[] Zoom = new Vector3[3];
    Vector3 ActualZoom = Vector3.zero;
    // Métodos
    void Start()
    {
        Zoom[0] = Zoom1;
        Zoom[1] = Zoom2;
        Zoom[2] = Zoom3;
        ActualZoom = Zoom3;
        Personagem = GameObject.FindGameObjectWithTag("Player");
        try
        {
            transform.position = GameObject.Find("InitialPosition").transform.position;
        }
        catch { Debug.LogError("Não tem um GameObject de nome InitialPosition"); }
    }
    void Update()
    {
        if (Personagem == null)
        {
            try
            {
                Personagem = GameObject.FindGameObjectWithTag("Player");
            }
            catch
            {

            }
        }
        
		
		Zoom[0] = Zoom1;
        Zoom[1] = Zoom2;
        Zoom[2] = Zoom3;
		
        ActualZoom = Vector3.Lerp(ActualZoom, Zoom[indice], Time.deltaTime * 3);
	    FollowCamera();
    }
	void FollowCamera()
    {
        if (Personagem != null)
        {
            Vector3 tempC = new Vector3(transform.position.x + ActualZoom.x, Personagem.transform.position.y + ActualZoom.y, Personagem.transform.position.z + ActualZoom.z);
            Vector3 tempP = new Vector3(Personagem.transform.position.x + ActualZoom.x, Personagem.transform.position.y + ActualZoom.y, Personagem.transform.position.z + ActualZoom.z);
            transform.position = Vector3.Lerp(tempC, tempP, Time.deltaTime * 5);
            transform.eulerAngles = Angle;
        }
    }
}
