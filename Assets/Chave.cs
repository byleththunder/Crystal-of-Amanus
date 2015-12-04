using UnityEngine;
using System.Collections;

public class Chave : Item {

	// Use this for initialization
	void Start () {
        MetodoItem = Nada;
	}
	
    bool Nada(Target alvo)
    {
        return false;
    }
}
