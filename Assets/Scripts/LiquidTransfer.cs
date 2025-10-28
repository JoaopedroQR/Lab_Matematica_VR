using UnityEngine;
using System.Collections;

public class LiquidTransfer : MonoBehaviour
{
    [Header("Referências (Containers)")]
    public LiquidContainer source1;
    public LiquidContainer source2;
    public LiquidContainer target;

    [Header("Referências (Reset de Pose)")]
    public ResettableTransform source1Reset;
    public ResettableTransform source2Reset;

    [Header("Animação")]
    [Tooltip("Velocidade de enchimento (no range 0–0.1). Ex: 0.01 = leva 5s para encher 0.05.")]
    public float unitsPerSecond = 0.01f;

    [Header("Reset automático")]
    public float resetDelaySeconds = 5f;

    private const float MAX_FILL = 0.1f;   // cheio visual
    private const float CONTRIBUTION = 0.05f; // cada fonte adiciona metade
    private const float EPS = 0.00001f;

    private bool source1Used = false;
    private bool source2Used = false;

    private void OnTriggerEnter(Collider other)
    {
        var container = other.GetComponentInParent<LiquidContainer>();
        if (!container) return;

        // Se for Objeto1
        if (container == source1 && !source1Used && source1.fillAmount > 0f)
        {
            float targetFill = Mathf.Min(target.fillAmount + CONTRIBUTION, MAX_FILL);
            StartCoroutine(TransferRange(source1, targetFill, () =>
            {
                source1.Empty();
                source1Used = true;
            }));
        }
        // Se for Objeto2
        else if (container == source2 && !source2Used && source2.fillAmount > 0f)
        {
            float targetFill = Mathf.Min(target.fillAmount + CONTRIBUTION, MAX_FILL);
            StartCoroutine(TransferRange(source2, targetFill, () =>
            {
                source2.Empty();
                source2Used = true;
            }));
        }
    }

    private IEnumerator TransferRange(LiquidContainer source, float targetFill, System.Action onFinish)
    {
        while (target.fillAmount + EPS < targetFill && source.fillAmount > 0f)
        {
            float step = unitsPerSecond * Time.deltaTime;
            float remainingToTarget = targetFill - target.fillAmount;
            float transfer = Mathf.Min(step, remainingToTarget, source.fillAmount);

            if (transfer <= 0f) break;

            target.SetFill(target.fillAmount + transfer);
            source.SetFill(source.fillAmount - transfer);

            yield return null;
        }

        onFinish?.Invoke();

        // Se chegou no cheio, agenda reset
        if (target.fillAmount + EPS >= MAX_FILL)
        {
            StartCoroutine(ResetAllAfterDelay());
        }
    }

    private IEnumerator ResetAllAfterDelay()
    {
        yield return new WaitForSeconds(resetDelaySeconds);

        // 1) Zera alvo
        target.SetFill(0f);

        // 2) Restaura fills iniciais
        source1.ResetToInitial();
        source2.ResetToInitial();

        // 3) Restaura posições/rotações
        if (source1Reset) source1Reset.ResetPose();
        if (source2Reset) source2Reset.ResetPose();

        // 4) Libera uso novamente
        source1Used = false;
        source2Used = false;
    }
}
