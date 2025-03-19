using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class marcador : MonoBehaviour
{
    int pan;
    public Text puntos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pan = 0;
        ActualizarPuntos();
    }

    // Método que suma puntos al chocar con el pan
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            contadorPanes();
            StartCoroutine(DestruirPanConDelay(other.gameObject)); // Inicia la corutina para la destrucción
        }
    }

    IEnumerator DestruirPanConDelay(GameObject pan)
    {
        yield return new WaitForSeconds(1f); // Espera 0.5 segundos antes de destruirlo
        Destroy(pan);
    }

    private void contadorPanes()
    {
            pan++;
            puntos.text = pan.ToString();
            ActualizarPuntos();
            Debug.Log("tus puntos son: " + pan);
    }

    private void ActualizarPuntos()
    {
        if (puntos != null)
        {
            puntos.text = pan.ToString();
        }
    }
}
