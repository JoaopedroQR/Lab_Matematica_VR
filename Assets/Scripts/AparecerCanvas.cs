using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvasOnTrigger : MonoBehaviour
{
    [Header("O Canvas que vai sumir")]
    public GameObject canvasParaDesativar;

    [Header("O Canvas que vai aparecer")]
    public GameObject canvasParaAtivar;

    [Header("Tag do objeto que ativa")]
    public string tagDoAtivador = "Player";

    private bool alternado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagDoAtivador))
        {
            alternado = !alternado;

            if (canvasParaDesativar != null)
                canvasParaDesativar.SetActive(!alternado);

            if (canvasParaAtivar != null)
                canvasParaAtivar.SetActive(alternado);
        }
    }
}
