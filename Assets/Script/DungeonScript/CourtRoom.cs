using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CourtRoom : MonoBehaviour
{


    [SerializeField]
    private Image painel;
    [SerializeField]
    private Image Continua;
    Color Fade;
    Color TextFade;
    float timer = 0;
    bool comecou = false;
    GameObject BoxDeMensagem;
    // Use this for initialization
    void Start()
    {
        MessageBox.PrefabPath = "CaixaDeTexto";
        Fade = new Color(painel.color.r, painel.color.g, painel.color.b, 0);
        TextFade = Continua.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (comecou)
        {
            if (!BoxDeMensagem.activeInHierarchy)
            {
                if (Fade.a < 1)
                {
                    timer += Time.deltaTime;
                    if (timer > 0.1f)
                    {
                        timer = 0;
                        Fade.a += 0.05f;
                    }
                    painel.color = Fade;
                }
                else
                {
                    if(Continua.color.a <1f)
                    {
                         timer += Time.deltaTime;
                         if (timer > 0.1f)
                         {
                             timer = 0;
                             TextFade.a += 0.05f;
                         }
                         Continua.color = TextFade;
                    }
                    
                }
            }
        }
        else
        {
            Mundanca();
        }
    }
    void Conversa()
    {

        MessageBox.Instance.WriteMessage("Meus senhores, vejam bem! Esse homem não roubou o tesouro! Ele estava apenas passando pelo local.", "Advogado");
        MessageBox.Instance.WriteMessage("Foram os monstros que roubaram a diligência real!", "Advogado");
        MessageBox.Instance.WriteMessage("Inaceitável! Por que monstros roubariam ouro!? Ele é um ladrão que atua contra a soberania de nosso rei!", "Promotor");
        MessageBox.Instance.WriteMessage("Culpado! Culpado! Matem-no! Em praça pública! Que sirva de exemplo para os demais!", "Juiz");
        MessageBox.Instance.WriteMessage("Minha corte, acalme-se! O histórico do réu pode nos ajudar a enxergar as coisas com mais clareza. ", "Advogado");
        MessageBox.Instance.WriteMessage("Eran Airikina é um mercenário, um aventureiro por profissão.", "Advogado");
        MessageBox.Instance.WriteMessage("Oras, ele é mais valioso ao rei vivo, servindo com sua espada, do que morto, servindo aos deuses da morte!", "Advogado");
        MessageBox.Instance.WriteMessage("Eran Airikina, isso é verdade?", "Juiz");
        MessageBox.Instance.WriteMessage("Eu consegui desbravar as ruínas e derrotei os monstros sozinho...", "Eran");
        MessageBox.Instance.WriteMessage("Isso é verdade. Ouvi dizer que até os soldados do rei tiveram um pouco de dificuldade nessas ruínas...", "Juiz");
        MessageBox.Instance.WriteMessage("Muito bem! Sua punição será trabalhar para o exército real até pagar sua dívida!", "Juiz");
        MessageBox.Instance.WriteMessage("Erm... Dívida?", "Eran");
        MessageBox.Instance.WriteMessage("Nós só encontramos o ouro de uma das carruagens. Ainda precisamos encontrar o ouro que estava na segunda carruagem!", "Juiz");
        MessageBox.Instance.WriteMessage("Você deve trabalhar agora para pagar o valor perdido!", "Juiz");
        BoxDeMensagem = MessageBox.Instance.gameObject;
        comecou = true;
    }
    void Mundanca()
    {

        if (Fade.a < 0.2f)
        {
            timer += Time.deltaTime;
            if (timer > 0.1f)
            {
                timer = 0;
                Fade.a += 0.05f;
            }
            painel.color = Fade;
        }
        else
        {
            Conversa();

        }
    }
}
