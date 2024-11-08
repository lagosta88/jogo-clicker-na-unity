using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject[] tabs; // Array para armazenar os panels de cada aba
    public Button[] buttons;  // Array para armazenar os buttons das abas

    private void Start()
    {
        // Define a primeira aba como ativa
        ShowTab(0);

        // Adiciona a funcionalidade de clique em cada botão
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Necessário para preservar o índice dentro do loop
            buttons[i].onClick.AddListener(() => ShowTab(index));
        }
    }

    // Função para mostrar o conteúdo da aba
    public void ShowTab(int index)
    {
        // Desativa todas as abas
        foreach (var tab in tabs)
        {
            tab.SetActive(false);
        }

        // Ativa a aba selecionada
        tabs[index].SetActive(true);
    }
}
