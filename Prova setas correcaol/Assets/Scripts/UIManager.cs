using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Sprite[] sprites;
    [SerializeField] Image[] imagens;
    [SerializeField] TextMeshProUGUI textoDePontuacao;
    [SerializeField] TextMeshProUGUI textoDoRelogio;

    // Delegate para atualizar as setas
    public delegate Sprite AtualizarSetaDelegate(KeyCode key);

    // Função que define a lógica padrão para atualizar as setas
    private Sprite DefaultAtualizarSeta(KeyCode key)
    {
        if (key == KeyCode.DownArrow) return sprites[1];
        if (key == KeyCode.UpArrow) return sprites[2];
        if (key == KeyCode.LeftArrow) return sprites[3];
        if (key == KeyCode.RightArrow) return sprites[4];
        return sprites[0];
    }

    private void Awake()
    {
        instance = this;
    }

    public void AtualizarSetas(KeyCode[] setas, AtualizarSetaDelegate atualizarSetaCallback = null)
    {
        // Usa a lógica padrão se nenhum delegate for passado
        if (atualizarSetaCallback == null)
        {
            atualizarSetaCallback = DefaultAtualizarSeta;
        }

        for (int i = 0; i < imagens.Length; i++)
        {
            if (i >= setas.Length)
            {
                imagens[i].sprite = sprites[0];
            }
            else
            {
                imagens[i].sprite = atualizarSetaCallback(setas[i]);
            }

            imagens[i].color = Color.white;
        }
    }

    public void AtualizarSeta(int setaSelecionada, bool acertou)
    {
        imagens[setaSelecionada].color = acertou ? Color.green : Color.red;
    }

    public void AtualizarTextos(int pontuacao, float relogio)
    {
        textoDePontuacao.text = pontuacao.ToString();
        textoDoRelogio.text = relogio.ToString();
    }
}
