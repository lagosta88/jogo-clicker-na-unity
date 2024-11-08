using UnityEngine;
using UnityEngine.UI;

public class ClickerGame : MonoBehaviour
{
    public int score = 0;                                   // Pontuação atual do jogador
    public int scorePerClick = 1;                           // Pontos ganhos por clique
    
    [System.Serializable]
    public class Generator
    {
        public int scorePerSecond = 1;                      // Quantidade de pontos gerados por este gerador a cada segundo
        public int cost = 10;                               // Custo inicial deste gerador
        public int costIncrement = 5;                       // Aumento do custo após cada compra
        public int upgradeMultiplier = 1;                   // Multiplicador de geração devido a upgrades
        public int upgradeCost = 20;                        // Custo do primeiro upgrade
        public int upgradeCostIncrement = 10;               // Aumento do custo de upgrade após cada compra
        public Text costText;                               // Texto de exibição do custo do gerador
        public Text upgradeCostText;                        // Texto de exibição do custo de upgrade do gerador
        public Button generatorButton;                      // Botão do gerador
        public Button upgradeButton;                        // Botão para upgrade de multiplicação
        public int upgradeMultipliermodifier = 2;           // Multiplicador para o upgrade    
        
        // Novo campo que permite definir o valor gerado
        public int generationValue = 1;                      // Valor de geração deste gerador
    }

    [System.Serializable]
    public class ClickGenerator
    {
        public int clickBonus = 1;                          // Bônus de pontos por clique fornecido por este gerador
        public int cost = 50;                               // Custo inicial deste gerador
        public int costIncrement = 25;                      // Aumento do custo após cada compra
        public Text costText;                               // Texto de exibição do custo do gerador de clique
        public Button generatorButton;                      // Botão do gerador de clique
    }

    public Generator[] generators;                          // Lista de geradores automáticos
    public ClickGenerator[] clickGenerators;                // Lista de geradores que aumentam o ganho por clique

    public Text scoreText;                                  // Referência ao texto da pontuação na interface
    public Button clickButton;                              // Referência ao botão de clique

    void Start()
    {
        UpdateScoreText();
        
        clickButton.onClick.AddListener(OnClick);

        foreach (Generator generator in generators)
        {
            UpdateGeneratorCostText(generator);
            UpdateUpgradeCostText(generator);
            generator.generatorButton.onClick.AddListener(() => ActivateGenerator(generator));
            generator.upgradeButton.onClick.AddListener(() => UpgradeGenerator(generator));
        }

        foreach (ClickGenerator clickGen in clickGenerators)
        {
            UpdateClickGeneratorCostText(clickGen);
            clickGen.generatorButton.onClick.AddListener(() => ActivateClickGenerator(clickGen));
        }

        InvokeRepeating("GenerateScorePerSecond", 1f, 1f);
    }

    void OnClick()
    {
        score += scorePerClick;
        UpdateScoreText();
    }

    void ActivateGenerator(Generator generator)
    {
        if (score >= generator.cost)
        {
            score -= generator.cost;
            generator.scorePerSecond += generator.generationValue; // Usando o valor de geração configurado
            generator.cost += generator.costIncrement;
            UpdateScoreText();
            UpdateGeneratorCostText(generator);
        }
        else
        {
            Debug.Log("Pontos insuficientes para comprar este gerador.");
        }
    }

    void UpgradeGenerator(Generator generator)
    {
        if (score >= generator.upgradeCost)
        {
            score -= generator.upgradeCost;
            generator.upgradeMultiplier *= 2;  // Dobra a geração deste gerador
            generator.generationValue *= generator.upgradeMultipliermodifier; // Aumenta o valor de geração
            generator.upgradeCost += generator.upgradeCostIncrement;
            UpdateScoreText();
            UpdateUpgradeCostText(generator);
        }
        else
        {
            Debug.Log("Pontos insuficientes para upgrade deste gerador.");
        }
    }

    void ActivateClickGenerator(ClickGenerator clickGen)
    {
        if (score >= clickGen.cost)
        {
            score -= clickGen.cost;
            scorePerClick += clickGen.clickBonus;
            clickGen.cost += clickGen.costIncrement;
            UpdateScoreText();
            UpdateClickGeneratorCostText(clickGen);
        }
        else
        {
            Debug.Log("Pontos insuficientes para este gerador de clique.");
        }
    }

    void GenerateScorePerSecond()
    {
        foreach (Generator generator in generators)
        {
            score += generator.scorePerSecond;
        }
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = " " + score.ToString();
    }

    void UpdateGeneratorCostText(Generator generator)
    {
        if (generator.costText != null)
        {
            generator.costText.text = " " + generator.cost.ToString();
        }
    }

    void UpdateUpgradeCostText(Generator generator)
    {
        if (generator.upgradeCostText != null)
        {
            generator.upgradeCostText.text = " " + generator.upgradeCost.ToString();
        }
    }

    void UpdateClickGeneratorCostText(ClickGenerator clickGen)
    {
        if (clickGen.costText != null)
        {
            clickGen.costText.text = " " + clickGen.cost.ToString();
        }
    }
}
