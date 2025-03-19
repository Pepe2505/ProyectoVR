using System.Collections;
using UnityEngine;

public class PanSpawner : MonoBehaviour
{
    public GameObject breadPrefab; // Prefab del pan
    public float spawnIntervalo = 3f; // Intervalo de spawn
    public Vector3 spawnMinimo = new Vector3(-0.30f, 1.6f, -18.83f); // M�nimos en X, Y y Z
    public Vector3 spawnMaximo = new Vector3(-0.32f, 1.6f, -18.48f);  // M�ximos en X, Y y Z
    public int maxBreads = 10; // N�mero m�ximo de panes en escena
    public int maxPuntuables = 15; // M�ximo de panes que pueden dar puntos
    private int activeBreads = 0; // Contador de panes activos
    private int totalPuntuados = 0; // Contador de panes que han dado puntos

    void Start()
    {
        StartCoroutine(SpawnBread());
    }

    IEnumerator SpawnBread()
    {
        while (totalPuntuados < maxPuntuables) // Limita el n�mero de panes puntuables
        {
            // Esperar si hay demasiados panes en escena
            if (activeBreads >= maxBreads)
            {
                yield return new WaitUntil(() => activeBreads < maxBreads);
            }

            // Generar una posici�n dentro de los l�mites definidos
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnMinimo.x, spawnMaximo.x),
                Random.Range(spawnMinimo.y, spawnMaximo.y),
                Random.Range(spawnMinimo.z, spawnMaximo.z)
            );

            // Instanciar el pan en la posici�n generada
            GameObject newBread = Instantiate(breadPrefab, spawnPosition, Quaternion.identity);
            activeBreads++; // Aumentar contador

            // Configurar aleatoriamente la masa del pan (si tiene Rigidbody)
            Rigidbody breadRb = newBread.GetComponent<Rigidbody>();
            if (breadRb != null)
            {
                breadRb.mass = Random.Range(0.5f, 2.5f);
            }

            // Tama�o aleatorio
            newBread.transform.localScale = new Vector3(
                Random.Range(0.4f, 1f),
                Random.Range(0.3f, 1f),
                Random.Range(0.6f, 0.9f)
            );

            // Agregar evento para detectar cuando el pan sea destruido
            newBread.AddComponent<BreadDestroyer>().SetSpawner(this);

            // Esperar el intervalo de tiempo antes de generar otro pan
            yield return new WaitForSeconds(spawnIntervalo);
        }
    }

    // M�todo para reducir el contador cuando un pan desaparece
    public void BreadDestroyed()
    {
        activeBreads = Mathf.Max(0, activeBreads - 1);
        totalPuntuados++; // Aumenta el contador de panes que han dado puntos

        // Si el jugador ha recolectado 15 panes, detener la generaci�n
        if (totalPuntuados >= maxPuntuables)
        {
            StopCoroutine(SpawnBread());
        }
    }
}

// Destrucci�n de panes
public class BreadDestroyer : MonoBehaviour
{
    private PanSpawner spawner;

    public void SetSpawner(PanSpawner spawner)
    {
        this.spawner = spawner;
    }

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.BreadDestroyed();
        }
    }
}
