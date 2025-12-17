using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configurações")]
    public Transform alvo; // O Player
    public Vector3 offset = new Vector3(0, 10, -10); // Distância inicial
    public float velocidadeGiro = 100f; // Velocidade do giro Q/E

    // Nota: Removi a variável "suavidade", pois não vamos mais usar Lerp

    void LateUpdate()
    {
        if (alvo == null) return;

        // 1. Calcular Rotação (Q e E)
        // Fazemos isso ANTES de mover a câmera para atualizar o offset
        RotacionarCamera();

        // 2. Posicionamento IMEDIATO (Sem Lerp)
        // Ao invés de mover suavemente, dizemos: "Sua posição É esta, agora."
        transform.position = alvo.position + offset;

        // 3. Garantir que olha para o alvo
        transform.LookAt(alvo.position);
    }

    void RotacionarCamera()
    {
        float inputGiro = 0;
        if (Input.GetKey(KeyCode.Q)) inputGiro = -1;
        if (Input.GetKey(KeyCode.E)) inputGiro = 1;

        if (inputGiro != 0)
        {
            // Giramos o próprio vetor de offset
            // Isso mantém a câmera na mesma distância, mas muda o ângulo
            offset = Quaternion.AngleAxis(inputGiro * velocidadeGiro * Time.deltaTime, Vector3.up) * offset;
        }
    }
}