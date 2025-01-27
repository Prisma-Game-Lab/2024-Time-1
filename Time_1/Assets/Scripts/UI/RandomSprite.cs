using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] sprites; // Vetor de sprites disponíveis
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}
