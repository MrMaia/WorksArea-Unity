using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Vida (Health)")]
    public float vidaMaxima = 100f;
    public float vidaAtual;

    [Header("Mana")]
    public float manaMaxima = 50f;
    public float manaAtual;
    public float regenMana = 1f; // Quanto de mana recupera por segundo

    [Header("Stamina (Vigor)")]
    public float staminaMaxima = 100f;
    public float staminaAtual;
    public float regenStamina = 5f; // Recupera 5 de stamina por segundo

    void Start()
    {
        // Começa o jogo com tudo cheio
        vidaAtual = vidaMaxima;
        manaAtual = manaMaxima;
        staminaAtual = staminaMaxima;
    }

    void Update()
    {
        RegenerarStatus();
    }

    void RegenerarStatus()
    {
        // Recupera Stamina se ela não estiver cheia
        if (staminaAtual < staminaMaxima)
        {
            staminaAtual += regenStamina * Time.deltaTime;
        }

        // Recupera Mana se não estiver cheia
        if (manaAtual < manaMaxima)
        {
            manaAtual += regenMana * Time.deltaTime;
        }

        // Garante que nunca ultrapasse o máximo (Clamp)
        // Isso evita ter 105/100 de vida, por exemplo.
        staminaAtual = Mathf.Clamp(staminaAtual, 0, staminaMaxima);
        manaAtual = Mathf.Clamp(manaAtual, 0, manaMaxima);
    }

    // Método para ser chamado quando levar dano
    public void ReceberDano(float dano)
    {
        vidaAtual -= dano;
        Debug.Log("Vida Restante: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Debug.Log("O Personagem Morreu!");
            // Aqui colocaremos a lógica de morte no futuro
        }
    }

    // Método que tenta gastar stamina. Retorna 'true' se conseguiu, 'false' se não tinha energia.
    public bool TentarGastarStamina(float quantidade)
    {
        if (staminaAtual >= quantidade)
        {
            staminaAtual -= quantidade;
            return true; // Sucesso, pode correr/atacar
        }
        else
        {
            return false; // Falha, está cansado demais
        }
    }
}