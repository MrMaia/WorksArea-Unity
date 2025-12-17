using UnityEngine;

public class AreaDeDano : MonoBehaviour
{
    public float danoPorSegundo = 10f;

    // OnTriggerStay é chamado EM TODOS OS FRAMES enquanto alguém estiver dentro do objeto
    void OnTriggerStay(Collider other)
    {
        // Verifica se o objeto que entrou tem o script "PlayerStats"
        PlayerStats statsDoJogador = other.GetComponent<PlayerStats>();

        // Se encontrou o script, significa que é o jogador (ou um inimigo com vida)
        if (statsDoJogador != null)
        {
            // Aplica o dano
            // Multiplicamos por Time.deltaTime para ser "10 de dano por SEGUNDO"
            // Se não usasse deltaTime, seria 10 de dano por FRAME (morte instantânea)
            statsDoJogador.ReceberDano(danoPorSegundo * Time.deltaTime);
        }
    }
}