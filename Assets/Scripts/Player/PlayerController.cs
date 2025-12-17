using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configurações de Velocidade")]
    public float velocidadeAndar = 6f;
    public float velocidadeCorrer = 10f;
    
    // NOVO: Custo de stamina para correr (por segundo)
    public float custoStaminaCorrer = 15f; 

    [Header("Configurações Gerais")]
    public LayerMask layerChao;

    [Header("Estado")]
    public bool modoCombate = false;

    private CharacterController controller;
    private Camera cam;
    private PlayerStats stats; // NOVO: Referência ao script de status

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        
        // NOVO: O controlador precisa encontrar o script de status no mesmo objeto
        stats = GetComponent<PlayerStats>(); 
    }

    void Update()
    {
        Mover();
        
        if(Input.GetKeyDown(KeyCode.F))
        {
            modoCombate = !modoCombate;
        }

        if (modoCombate) OlharParaMouse();
        else OlharParaMovimento();
    }

    void Mover()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // --- LÓGICA DE CORRIDA ATUALIZADA ---
        
        // 1. O jogador quer correr? (Segurando Shift e se movendo)
        bool querCorrer = Input.GetKey(KeyCode.LeftShift) && (h != 0 || v != 0);
        
        // 2. Define a velocidade padrão como andar
        float velocidadeAtual = velocidadeAndar;

        // 3. Se quer correr, verifica se tem stamina
        if (querCorrer)
        {
            // Tenta gastar um pouquinho de stamina baseado no tempo (Time.deltaTime)
            // Se retornar 'true', significa que tinha stamina, então corremos.
            if (stats.TentarGastarStamina(custoStaminaCorrer * Time.deltaTime))
            {
                velocidadeAtual = velocidadeCorrer;
            }
            else
            {
                // Se retornar false, a stamina acabou, então ele só anda.
                velocidadeAtual = velocidadeAndar;
            }
        }
        // -------------------------------------

        Vector3 direcao = (cam.transform.right * h) + (cam.transform.forward * v);
        direcao.y = 0; 
        direcao.Normalize(); 

        Vector3 movimentoFinal = direcao * velocidadeAtual;
        movimentoFinal.y = -9.8f; 

        controller.Move(movimentoFinal * Time.deltaTime);
    }

    void OlharParaMovimento()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        Vector3 direcao = (cam.transform.right * h) + (cam.transform.forward * v);
        direcao.y = 0;

        // Só gira se estiver se movendo
        if (direcao != Vector3.zero)
        {
            Quaternion novaRotacao = Quaternion.LookRotation(direcao);
            // Slerp deixa o giro suave. Aumente o 15f se quiser giros mais rápidos
            transform.rotation = Quaternion.Slerp(transform.rotation, novaRotacao, 15f * Time.deltaTime);
        }
    }

    void OlharParaMouse()
    {
        // 1. Cria o raio saindo da câmera em direção ao mouse
        Ray raio = cam.ScreenPointToRay(Input.mousePosition);

        // 2. Cria um "Plano Matemático" invisível
        // Vector3.up = A direção do plano (virado para cima)
        // transform.position = A altura onde esse plano está (na altura do player)
        Plane planoInvisivel = new Plane(Vector3.up, transform.position);

        // 3. Variável para calcular a distância do raio até o plano
        float distanciaDoRaio;

        // 4. O método 'Raycast' do plano calcula se o raio bateu nele
        if (planoInvisivel.Raycast(raio, out distanciaDoRaio))
        {
            // Pega o ponto exato no mundo (X, Y, Z) onde o raio cruzou o plano
            Vector3 pontoOlhar = raio.GetPoint(distanciaDoRaio);

            // (Segurança) Garante que a altura seja igual a do player para ele não olhar para cima/baixo
            pontoOlhar.y = transform.position.y;

            // Faz o personagem olhar para esse ponto
            transform.LookAt(pontoOlhar);
        }
    }
}