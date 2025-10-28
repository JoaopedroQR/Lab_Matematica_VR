using UnityEngine;

public class LiquidRotationHandler : MonoBehaviour
{
    [Header("Refer�ncias (Containers)")]
    public LiquidContainer obj1;  // Objeto1 (vai esvaziar)
    public LiquidContainer obj2;  // Objeto2 (vai esvaziar)
    public LiquidContainer obj3;  // Objeto3 (vai encher)

    [Header("Configura��es de rota��o")]
    public float startRotation = 240f;  // In�cio da rota��o (no �ngulo de 240�)
    public float endRotation = 60f;    // Fim da rota��o (no �ngulo de 60�)

    [Header("Velocidade de rota��o")]
    public float rotationSpeed = 30f;  // Velocidade de rota��o (graus por segundo)

    private float currentRotation;
    private bool isRotating = false;  // Controla se a rota��o est� ativa

    private void Update()
    {
        // S� come�a a rota��o se a flag isRotating for verdadeira
        if (isRotating)
        {
            // Atualiza a rota��o do objeto
            currentRotation += rotationSpeed * Time.deltaTime;

            // Garantir que a rota��o est� dentro do intervalo de 240� a 60�
            if (currentRotation > startRotation) currentRotation = startRotation;
            if (currentRotation < endRotation) currentRotation = endRotation;

            // Calculando a porcentagem de rota��o entre 240 e 60
            float rotationPercentage = Mathf.InverseLerp(startRotation, endRotation, currentRotation);

            // Mapear a rota��o para o fillAmount de 1 (cheio) para 0 (vazio) nos objetos 1 e 2
            float obj1Fill = Mathf.Lerp(1f, 0f, rotationPercentage);  // Esvaziar o obj1
            float obj2Fill = Mathf.Lerp(1f, 0f, rotationPercentage);  // Esvaziar o obj2
            float obj3Fill = Mathf.Lerp(0f, 1f, rotationPercentage);  // Encher o obj3

            // Atualiza os fills dos objetos
            obj1.SetFill(obj1Fill);
            obj2.SetFill(obj2Fill);
            obj3.SetFill(obj3Fill);
        }
    }

    // Fun��o p�blica que pode ser chamada no OnClick do bot�o para iniciar a rota��o
    public void StartRotation()
    {
        isRotating = true;
    }
}
