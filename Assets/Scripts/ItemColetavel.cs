using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    [Header("Configurações")]
    public float quantidadeCura = 30f;
    
    // Usamos OnTriggerEnter (Entrar) e não Stay.
    // Assim o evento só dispara uma vez no momento do toque.
    void OnTriggerEnter(Collider other)
    {
        // Verifica se quem encostou é o jogador
        PlayerStats stats = other.GetComponent<PlayerStats>();

        if (stats != null)
        {
            // 1. Aplica o efeito (Cura)
            stats.Curar(quantidadeCura);

            // 2. Toca um som ou efeito visual aqui (futuramente)
            
            // 3. Destrói o objeto (A poção some)
            Destroy(gameObject);
        }
    }
}