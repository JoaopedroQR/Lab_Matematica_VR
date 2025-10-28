using UnityEngine;

public class LiquidContainer : MonoBehaviour
{
    [Range(0f, 0.1f)]
    public float fillAmount = 0f;

    [Header("Shader")]
    public Renderer targetRenderer;
    public string fillProperty = "_Fill";
    public bool invertFill = false;

    [Header("Estado")]
    public bool isDepleted = false;
    public float initialFill = 0f;

    private MaterialPropertyBlock _mpb;

    private void Awake()
    {
        if (!targetRenderer)
            targetRenderer = GetComponentInChildren<Renderer>();

        initialFill = Mathf.Clamp(fillAmount, 0f, 0.1f);

        _mpb = new MaterialPropertyBlock();
        ApplyToShader();

        // Se começar vazio, esconde
        UpdateVisibility();
    }

    public void SetFill(float value)
    {
        fillAmount = Mathf.Clamp(value, 0f, 0.1f);
        ApplyToShader();
        UpdateVisibility();
    }

    public void AddFill(float delta)
    {
        SetFill(fillAmount + delta);
    }

    public void Empty()
    {
        isDepleted = true;
        SetFill(0f); // isso também chama UpdateVisibility
    }

    public void ResetToInitial()
    {
        isDepleted = false;
        SetFill(initialFill); // isso também chama UpdateVisibility
    }

    private void ApplyToShader()
    {
        if (!targetRenderer) return;

        float v = invertFill ? 0.1f - fillAmount : fillAmount;

        if (_mpb == null) _mpb = new MaterialPropertyBlock();
        targetRenderer.GetPropertyBlock(_mpb);
        _mpb.SetFloat(fillProperty, v);
        targetRenderer.SetPropertyBlock(_mpb);
    }

    private void UpdateVisibility()
    {
        if (!targetRenderer) return;

        // invisível se vazio
        targetRenderer.enabled = fillAmount > 0f;
    }
}