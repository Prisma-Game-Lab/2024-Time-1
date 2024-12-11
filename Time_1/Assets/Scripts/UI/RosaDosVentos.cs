using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Necessário para usar corrotinas

public class RosaDosVentos : MonoBehaviour
{
    [System.Serializable]
    public class Direcao
    {
        public string Nome;          // Nome da direção
        public Image Seta;  // Sprite da seta
    }

    public Direcao[] Direcoes; // Lista de direções e suas setas
    public Color CorAtiva;
    public Color CorPadrao;
    public float pisca = 10f;

    private void Start()
    {
        Debug.Log("Iniciando RosaDosVentos");
        ResetarSetas();
        
    }

    // Atualiza a direção ativa, fazendo a seta piscar
    public void DirecaoInimigo(int index)
    {
        if (index >= 0 && index < Direcoes.Length)
        {
            Debug.Log("Inimigo vindo da direção: " + Direcoes[index].Nome);

            // Inicia a corrotina para a direção correspondente
            StartCoroutine(PiscarSeta(Direcoes[index]));
        }
        else
        {
            Debug.LogWarning("Índice de direção inválido na RosaDosVentos!");
        }
    }

    // Corrotina para piscar a seta
    private IEnumerator PiscarSeta(Direcao direcao)
    {
        float tempoDecorrido = 0f;
        bool corAtiva = false;

        while (tempoDecorrido < pisca)
        {
            // Alterna entre a cor ativa e a cor padrão
            direcao.Seta.color = corAtiva ? CorAtiva : CorPadrao;
            corAtiva = !corAtiva;

            // Espera 0.5 segundos antes de alternar a cor novamente
            yield return new WaitForSeconds(0.5f);

            tempoDecorrido += 0.5f;
        }

        // Após o tempo de piscagem, reseta a seta para a cor padrão
        direcao.Seta.color = CorPadrao;
    }

    // Reseta todas as setas para a cor padrão
    public void ResetarSetas()
    {
        Debug.Log("Resetando setas para cor padrão");
        foreach (var direcao in Direcoes)
        {
            direcao.Seta.color = CorPadrao;
        }
    }
}
