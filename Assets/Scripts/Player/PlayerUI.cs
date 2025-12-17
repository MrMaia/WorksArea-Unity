using UnityEngine;
using UnityEngine.UI; // IMPORTANTE: Necessário para mexer com UI

public class PlayerUI : MonoBehaviour
{
    [Header("Conexões")]
    public PlayerStats stats; // Referência ao script de status do jogador

    [Header("Barras da UI (Arraste as imagens 'Fill' aqui)")]
    public Image barraVida;
    public Image barraMana;
    public Image barraStamina;

    void Update()
    {
        // Verifica se o stats existe para evitar erros
        if (stats == null) return;

        // A lógica é: Valor Atual dividido pelo Valor Máximo
        // Exemplo: 50 de vida / 100 max = 0.5 (Metade da barra)
        
        if (barraVida != null)
            barraVida.fillAmount = stats.vidaAtual / stats.vidaMaxima;

        if (barraMana != null)
            barraMana.fillAmount = stats.manaAtual / stats.manaMaxima;

        if (barraStamina != null)
            barraStamina.fillAmount = stats.staminaAtual / stats.staminaMaxima;
    }
}