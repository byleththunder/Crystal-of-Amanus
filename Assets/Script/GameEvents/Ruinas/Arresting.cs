using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("Scripts/Game Events/Ruinas/Cinematics/Arresting")]
public class Arresting : MonoBehaviour
{

    /// <summary>
    /// Nesse script acontece a cinematic. Quando o Eran vai entrar na sala com o tesouro, os guardas do rei chegam e pensam que ele é o ladrão que
    /// roubou o tesouro do rei, por isso eles avançam e o prendem, enquanto a tela entra em fade.
    /// </summary>

    public Guarda_Ruinas Capitan;
    public Guarda_Ruinas[] Guardas = new Guarda_Ruinas[1];
    Animator cam;
    private bool IsTalking = false;
    bool Iniciar = false;
    bool startFade = false;
    public Image Fade;
    Color FadeColor;
    bool trigger = false;
    // Use this for initialization
    void Start()
    {
        MessageBox.PrefabPath = "CaixaDeTexto";
        cam = Camera.main.GetComponent<Animator>();
        FadeColor = Fade.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            if (!Capitan.IsMoving && !Iniciar)
            {
                Iniciar = true;
                IsTalking = true;
                MessageBox.Instance.WriteMessage("Mas... Quem são vocês!?", "Eran");
                MessageBox.Instance.WriteMessage("Então foi você quem roubou os tesouros de nosso rei! Soldados, prendam-no!", "Capitão");
            }
            if (IsTalking)
            {
                if (MessageBox.Instance.IsFinishAll)
                {
                    foreach (Guarda_Ruinas g in Guardas)
                    {
                        g.IsMoving = true;
                    }
                    IsTalking = false;
                    startFade = true;
                }
            }
            if (startFade)
            {
                if (FadeColor.a < 1)
                {
                    FadeColor.a += 0.01f;
                }
                else
                {
                    LoadingScreen.NextLevelName = "Tribunal";
                    Application.LoadLevel("LoadingScene");

                }
                Fade.color = FadeColor;
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(!trigger)
        {
            Capitan.gameObject.SetActive(true);
            cam.enabled = true;
            cam.SetTrigger("Guardas");
            Capitan.IsMoving = true;
            foreach (Guarda_Ruinas g in Guardas)
            {
                g.gameObject.SetActive(true);
                g.IsMoving = true;
            }
            trigger = true;
        }
    }
    
}
