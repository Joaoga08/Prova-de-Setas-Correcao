using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int pontos, teclaAtual;
    float relogio;
    KeyCode[] teclas;
    private KeyCode[] direcoes = {KeyCode.DownArrow,KeyCode.LeftArrow,KeyCode.UpArrow,KeyCode.RightArrow};




    private void Awake()
    {
        if(Instance != null && Instance != this)
        {

          Destroy(gameObject);

            return;
        }

        Instance = this;
DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GerarSetas();
    }

    private void Update()
    {
       foreach(KeyCode direcao in direcoes)
        {

            if (Input.GetKeyDown(direcao))
            {
                ChecarTecla(direcao);
                break;
            }
        }

        ContagemRegressiva();
    }

    void ContagemRegressiva()
    {
        relogio -= Time.deltaTime;

        UIManager.instance.AtualizarTextos(pontos, relogio);

        if(relogio <= 0)
        {
            pontos -= teclas.Length - teclaAtual;
            GerarSetas();
        }
    }

    void GerarSetas()
    {
        teclaAtual = 0;
        teclas = new KeyCode[Random.Range(5,15)];

        for(int i = 0; i < teclas.Length; i++)
        {
            teclas[i] = (KeyCode)Random.Range(273, 276);
        }

        relogio = teclas.Length / 2;
        UIManager.instance.AtualizarSetas(teclas);
    }

    void ChecarTecla(KeyCode teclaPressionada)
    {
        if (teclaPressionada == teclas[teclaAtual])
        {
            pontos++;
            UIManager.instance.AtualizarSeta(teclaAtual, true);
        }
        else 
        {
            pontos--;
            relogio--;
            UIManager.instance.AtualizarSeta(teclaAtual, false);
        }

        UIManager.instance.AtualizarTextos(pontos, relogio);

        teclaAtual++;
        if(teclaAtual == teclas.Length)
        {
            GerarSetas();
        }
    }


}
