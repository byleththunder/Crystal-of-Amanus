using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum TiposDeSalas { Corredor, Sala};
public class Sala : MonoBehaviour {

    [Header("Tipo de Sala")]
    public TiposDeSalas Tipo;
    [Header("Extremidades da Sala")]
    public List<Transform> Extremidades = new List<Transform>();
    
	// Use this for initialization
	void Start () {
	
	}
    [ContextMenu("Criar Extremidade")]
    void CriarTransform()
    {
        GameObject temp = new GameObject();
        temp.transform.SetParent(transform,false);
        temp.name = "Extremidade" + (Extremidades.Count + 1);
        Extremidades.Add(temp.transform);
    }
    void OnDrawGizmos()
    {
        for (int i = 0; i < Extremidades.Count; i++)
        {
            if (Extremidades[i] != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(Extremidades[i].position, Extremidades[i].position + Extremidades[i].forward * 2);
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(Extremidades[i].position, 0.3f);
            }
        }
    }
}
