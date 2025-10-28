using UnityEngine;

public class LiquidRotationHandler : MonoBehaviour
{
    [Header("Referências (Containers)")]
    public LiquidContainer obj1;  // Objeto1 (vai esvaziar)
    public LiquidContainer obj2;  // Objeto2 (vai esvaziar)
    public LiquidContainer obj3;  // Objeto3 (vai encher)

    [Header("Configurações de rotação")]
    public float startRotation = 240f;  // Início da rotação (no ângulo de 240°)
    public float endRotation = 60f;    // Fim da rotação (no ângulo de 60°)

    [Header("Velocidade de rotação")]
    public float rotationSpeed = 30f;  // Velocidade de rotação (graus por segundo)

    private float currentRotation;
    private bool isRotating = false;  // Controla se a rotação está ativa

    private void Update()
    {
        // Só começa a rotação se a flag isRotating for verdadeira
        if (isRotating)
        {
            // Atualiza a rotação do objeto
            currentRotation += rotationSpeed * Time.deltaTime;

            // Garantir que a rotação está dentro do intervalo de 240° a 60°
            if (currentRotation > startRotation) currentRotation = startRotation;
            if (currentRotation < endRotation) currentRotation = endRotation;

            // Calculando a porcentagem de rotação entre 240 e 60
            float rotationPercentage = Mathf.InverseLerp(startRotation, endRotation, currentRotation);

            // Mapear a rotação para o fillAmount de 1 (cheio) para 0 (vazio) nos objetos 1 e 2
            float obj1Fill = Mathf.Lerp(1f, 0f, rotationPercentage);  // Esvaziar o obj1
            float obj2Fill = Mathf.Lerp(1f, 0f, rotationPercentage);  // Esvaziar o obj2
            float obj3Fill = Mathf.Lerp(0f, 1f, rotationPercentage);  // Encher o obj3

            // Atualiza os fills dos objetos
            obj1.SetFill(obj1Fill);
            obj2.SetFill(obj2Fill);
            obj3.SetFill(obj3Fill);
        }
    }

    // Função pública que pode ser chamada no OnClick do botão para iniciar a rotação
    public void StartRotation()
    {
        isRotating = true;
    }
}
