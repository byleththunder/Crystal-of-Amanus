using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary "Leia aqui">
/// Link do tutorial da internet
/// http://gamedevelopment.tutsplus.com/tutorials/bake-your-own-3d-dungeons-with-procedural-recipes--gamedev-14360
/// </summary>

public class GeradorAleatorioDeFases : MonoBehaviour
{

    [Header("Tamanho da Fase")]
    public int Tamanho = 2;
    [Space(1f)]
    [Header("Peça que possue um InitialPosition")]
    public Sala SalaInicial;
    [Header("Peça que impede que duas peças se sobreponham")]
    public Sala SalaVazia;
    [Header("Peça que fecha os corredores")]
    public Sala CorredorFechado;
    [Header("Prefabs das salas")]
    public List<Sala> Prefabs = new List<Sala>();
    //Para cada extremidade eu coloco uma sala.
    List<Transform> Extremidades = new List<Transform>();
    // Use this for initialization
    void Start()
    {
        //Instacio a sala inicial
        Sala Inicio = Instantiate(SalaInicial);
        Inicio.transform.SetParent(transform, false);//Coloco a sala inicial como filha do Gerador de Fase 
        Extremidades.AddRange(Inicio.Extremidades.FindAll(x => x.gameObject.activeInHierarchy == true));//Pego somente as extremidades que estão ativas.
        //Aqui eu instancio os prefabs na cena. A fase vai ser gerada até o tamanho total dela, que seria o mesmo que iterações possiveis.
        for (int i = 0; i < Tamanho; i++)
        {
            List<Transform> _NovasExtremidades = new List<Transform>();
            for (int j = 0; j < Extremidades.Count; j++)
            {
                Sala temp = Instantiate(ChecarCaminho(Extremidades[j], i));
                temp.transform.SetParent(transform);
                //temp.name = "Sala";
                #region "Conectando Salas"
                Transform _Extremidade = temp.Extremidades[Random.Range(0, temp.Extremidades.Count)];
                MatchExits(Extremidades[j], _Extremidade);
                _Extremidade.gameObject.SetActive(false);
                Extremidades[j].gameObject.SetActive(false);
                _NovasExtremidades.AddRange(temp.Extremidades.FindAll(x => x.gameObject.activeInHierarchy));
                #endregion
            }
            Extremidades = _NovasExtremidades;
            
            //
        }
    }
   
    void MatchExits(Transform ExtremidadeAntiga, Transform ExtremidadeNova)
    {
        Transform PaiDaExtremidade = ExtremidadeNova.parent;
        Vector3 forwardVectorToMatch = -ExtremidadeAntiga.forward;
       float correctiveRotation = Azimuth(forwardVectorToMatch) - Azimuth(ExtremidadeNova.forward);
        PaiDaExtremidade.RotateAround(ExtremidadeNova.position, Vector3.up, correctiveRotation);
        Vector3 correctiveTranslation = ExtremidadeAntiga.position - ExtremidadeNova.position;
        PaiDaExtremidade.transform.position += correctiveTranslation;
    }
    private static float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }
    

    
    Sala ChecarCaminho(Transform Extremidade, int indice)
    {
        RaycastHit hit;
        //Para saber se eu devo colocar um objeto vázio entre caminhos ou fechar a passagem, eu disparo um raio 1m acima da Extremidade para que o raio detecte as paredes
        //Se eu colocar o raio na mesma altura da extremidade, o raio ultrapassa o Quad(Chão), fazendo com que não se detecte nada. Por que as distancias 5f e 20f?
        //5f é a distancia minima para detectar se existe uma sala na frente do corredor. 20f é uma distancia segura, que impede que salas e corredores se colidam.
        //obs.: Quando se atinge o limite de iterações, eu fecho as passagens
        /*6|
         *5|
         *4|
         *3|                         
         *2|                        _____
         *1|---------------->(Raio) |___| (Parede)
         *o-------------------------------(Quad)
         * */
        if (Physics.Raycast(Extremidade.position + new Vector3(0, 1, 0), Extremidade.forward - new Vector3(0, 1, 0), out hit, 5f))
        {

            Debug.DrawLine(Extremidade.position, hit.point, Color.green);
            return SalaVazia;
        }

        if (Physics.Raycast(Extremidade.position + new Vector3(0, 1, 0), Extremidade.forward, out hit, 20f))
        {
            Debug.DrawLine(Extremidade.position, hit.point, Color.blue);
            return CorredorFechado;
        }
        if (indice + 1 == Tamanho)
        {
            return CorredorFechado;
        }
        //Se a ultima sala for uma sala, eu vou colocar um corredor, se a ultima sala for um corredor, eu vou colocar uma sala.
        if (Extremidade.parent.GetComponent<Sala>().Tipo == TiposDeSalas.Corredor)
        {
            var temp = Prefabs.FindAll(x => x.Tipo == TiposDeSalas.Sala);
            return temp[Random.Range(0, temp.Count)];
        }

        return Prefabs.Find(x => x.Tipo == TiposDeSalas.Corredor);
    }

}
