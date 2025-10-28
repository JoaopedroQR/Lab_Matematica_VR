using UnityEngine;

[DisallowMultipleComponent]
public class ResettableTransform : MonoBehaviour
{
    [Tooltip("Se true, tamb�m reseta escala.")]
    public bool resetScale = false;

    [Tooltip("Se true, zera velocidade do Rigidbody ao resetar.")]
    public bool resetPhysics = true;

    private Vector3 _initPos;
    private Quaternion _initRot;
    private Vector3 _initScale;

    private Rigidbody _rb;

    private void Awake()
    {
        _initPos = transform.position;
        _initRot = transform.rotation;
        _initScale = transform.localScale;
        _rb = GetComponent<Rigidbody>();
    }

    public void ResetPose()
    {
        if (_rb && resetPhysics)
        {
            // congela momentaneamente para evitar jitter
            bool wasKinematic = _rb.isKinematic;
            _rb.isKinematic = true;

            transform.position = _initPos;
            transform.rotation = _initRot;
            if (resetScale) transform.localScale = _initScale;

            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.isKinematic = wasKinematic;
        }
        else
        {
            transform.position = _initPos;
            transform.rotation = _initRot;
            if (resetScale) transform.localScale = _initScale;
        }
    }
}
