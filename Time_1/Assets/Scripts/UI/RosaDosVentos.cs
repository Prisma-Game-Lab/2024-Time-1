using UnityEngine;
using UnityEngine.UI;

public class RosaDosVentos : MonoBehaviour
{
    [System.Serializable]
    public class Direcao
    {
        public string Nome;          // Nome da direção
        public SpriteRenderer Seta;  // Sprite da seta
    }

    public Direcao[] Direcoes; // Lista de direções e suas setas
    public Color CorAtiva = new Color(1f, 0.1f, 0.1f); // Cor para direção ativa (vermelho)
    public Color CorPadrao = new Color(0.1f, 1f, 0.4f); // Cor padrão (verde)

    private void Start()
    {
        ResetarSetas();
    }

    // Atualiza a seta da direção ativa
    public void AtualizarDirecaoAtiva(int index)
    {
        ResetarSetas();

        if (index >= 0 && index < Direcoes.Length)
        {
            Direcoes[index].Seta.color = CorAtiva; // Define a cor ativa
        }
        else
        {
            Debug.LogWarning("Índice de direção inválido na RosaDosVentos!");
        }
    }

    // Reseta todas as setas para a cor padrão
    public void ResetarSetas()
    {
        foreach (var direcao in Direcoes)
        {
            direcao.Seta.color = CorPadrao;
        }
    }
}
