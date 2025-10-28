using UnityEngine;

public class ControladorDeLiquidoPorRotacao : MonoBehaviour
{
    [Header("Materiais dos líquidos")]
    public Material obj1Material;
    public Material obj2Material;
    public Material obj3Material;

    [Header("Configuração")]
    public Axis eixoDeRotacao = Axis.Z;
    public string nomeDaPropriedadeDoShader = "fill";

    // ângulos de rotação definidos no seu caso
    private float anguloInicial = 240f;
    private float anguloFinal = 60f;

    public enum Axis { X, Y, Z }

    void Update()
    {
        float anguloAtual = ObterAnguloDeRotacao();
        float t = MapearRotacaoParaT(anguloAtual);

        // Mapeia os valores com base no t (0 a 1)
        float obj1Fill = Mathf.Lerp(1f, -0.5f, t);
        float obj2Fill = Mathf.Lerp(1f, -0.5f, t);
        float obj3Fill = Mathf.Lerp(-0.5f, 1f, t);

        AtualizarShader(obj1Material, obj1Fill);
        AtualizarShader(obj2Material, obj2Fill);
        AtualizarShader(obj3Material, obj3Fill);
    }

    float ObterAnguloDeRotacao()
    {
        Vector3 rot = transform.localEulerAngles;

        return eixoDeRotacao switch
        {
            Axis.X => rot.x,
            Axis.Y => rot.y,
            Axis.Z => rot.z,
            _ => rot.z,
        };
    }

    float MapearRotacaoParaT(float anguloAtual)
    {
        // Calcula a distância angular entre o início (240) e fim (60), tratando wrap de 360°
        float distancia = Mathf.DeltaAngle(anguloInicial, anguloFinal); // deve dar 180
        float progresso = Mathf.DeltaAngle(anguloInicial, anguloAtual); // atual distância desde 240

        float t = Mathf.Clamp01(progresso / distancia); // valor de 0 a 1

        return t;
    }

    void AtualizarShader(Material mat, float valor)
    {
        if (mat != null && mat.HasProperty(nomeDaPropriedadeDoShader))
        {
            mat.SetFloat(nomeDaPropriedadeDoShader, valor);
        }
    }
}
