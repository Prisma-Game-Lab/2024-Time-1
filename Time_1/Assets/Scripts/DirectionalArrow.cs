using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    public Transform target; // O alvo (inimigo)
    public Camera mainCamera; // A câmera principal
    public float screenEdgeOffset = 50f; // Distância da borda da tela

    private RectTransform arrowRectTransform;

    void Start()
    {
        arrowRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (target == null) // Se o alvo foi destruído
        {
            Destroy(gameObject); // Destroi a seta
            return;
        }

        // Converte a posição do alvo para coordenadas da tela
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

        // Verifica se o alvo está dentro da tela
        if (screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
        {
            // Se o alvo está na tela, desativa a seta
            gameObject.SetActive(false);
        }
        else
        {
            // Caso contrário, ativa a seta
            gameObject.SetActive(true);

            // Ajusta a posição para a borda da tela
            screenPos.x = Mathf.Clamp(screenPos.x, screenEdgeOffset, Screen.width - screenEdgeOffset);
            screenPos.y = Mathf.Clamp(screenPos.y, screenEdgeOffset, Screen.height - screenEdgeOffset);

            // Aponta a seta na direção do inimigo
            Vector3 targetPosition = target.position;
            Vector3 direction = targetPosition - mainCamera.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Atualiza a posição da seta
            arrowRectTransform.position = screenPos;
        }
    }
}
