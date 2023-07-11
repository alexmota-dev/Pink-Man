using UnityEngine;

public class BoxMakerController : MonoBehaviour
{
    public GameObject boxPrefab; // Prefab do objeto box a ser gerado
    public Transform spawnPoint1; // Ponto de spawn 1 do objeto box
    public Transform spawnPoint2; // Ponto de spawn 2 do objeto box
    public string boxTag; // Tag do objeto box
    public float intervalo = 10f; // Intervalo de tempo em segundos entre a geração de caixas

    private float tempoDecorrido = 0f;
    private bool alternarSpawnPoints = false;

    private void Update()
    {
        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido >= intervalo)
        {
            GerarCaixa();
            tempoDecorrido = 0f;
        }
    }

    private void GerarCaixa()
    {
        if (VerificarCaixaExistente())
        {
            // Se houver uma caixa em qualquer um dos pontos, não gere uma nova caixa
            return;
        }

        Transform spawnPoint = alternarSpawnPoints ? spawnPoint1 : spawnPoint2;
        alternarSpawnPoints = !alternarSpawnPoints;

        Instantiate(boxPrefab, spawnPoint.position, Quaternion.identity);
    }

    private bool VerificarCaixaExistente()
    {
        Collider2D[] colliders1 = Physics2D.OverlapBoxAll(spawnPoint1.position, spawnPoint1.localScale, 0f);
        Collider2D[] colliders2 = Physics2D.OverlapBoxAll(spawnPoint2.position, spawnPoint2.localScale, 0f);

        // Verifica se há uma caixa com a mesma tag em qualquer um dos pontos de spawn
        bool caixaExistente = false;
        foreach (Collider2D collider in colliders1)
        {
            if (collider.CompareTag(boxTag))
            {
                caixaExistente = true;
                break;
            }
        }

        if (!caixaExistente)
        {
            foreach (Collider2D collider in colliders2)
            {
                if (collider.CompareTag(boxTag))
                {
                    caixaExistente = true;
                    break;
                }
            }
        }

        return caixaExistente;
    }
}
