using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OpenCanvas : MonoBehaviour {

    public GameObject cv;
    public EventSystem evento;
    public GameObject FirstButton;
    public Character pers;
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(!cv.activeInHierarchy)
            {
                cv.SetActive(true);
                evento.SetSelectedGameObject(FirstButton);
                pers.EstadoDoJogador = GameStates.CharacterState.DontMove;
                GameStates.IsAWindowOpen = true;
            }
        }
    }

    public void Sair()
    {
        cv.SetActive(false);
        pers.EstadoDoJogador = GameStates.CharacterState.Playing;
        GameStates.IsAWindowOpen = false;
    }
}
