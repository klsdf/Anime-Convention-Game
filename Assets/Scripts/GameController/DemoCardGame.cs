using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


    ///<summary>
    ///Kehan Gong
    ///2025-02-08
    ///</summary>   
    ///Card Game类
    ///抽卡
    ///所有种类的稀有度总和必须为100%

public enum Rarity {Common, Rare, Legend}

  [System.Serializable]
    public class Card
    {
        public Rarity rarity;
        public string cardName;
        public GameObject cardObject;

        ///<summary>
        ///Define the card name, rarity, probability, value, and price
        ///</summary>

    }

public class DemoCardGame : MonoBehaviour
{
    /// <summary>
    /// Define the rarity setting and the probability increase设置概率
    /// </summary>
    [System.Serializable]
    public class RaritySetting
    {
    public Rarity rarity;
    public List<Card> cards = new List <Card>();
    [Range(0,100)] public float probability;

    [HideInInspector] public float currentProbability;
    }
   [SerializeField] private List<RaritySetting> raritySettings = new List<RaritySetting>();
   [SerializeField] private float legendProbabilityIncrease = 1f; 
   [SerializeField] [Range(0,200)] private int probabilityCount = 100;
   private int drawCount = 0;

   ///<summary>
   ///Define the rarity setting and the probability increase设置概率
   ///</summary>

   public void Start()
   {
        ///初始化概率   
         foreach (var setting in raritySettings)
        {
            setting.currentProbability = setting.probability;
        }
        //Normalize the probability
   }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DrawCard(); // 按下空格键触发抽卡
        }
    }   


    public Card DrawCard()
    {
    /// <summary>
    /// 为每种卡设置概率,计算所有稀有度的总概率,生成一个 0 到总概率之间的随机数,根据概率选择卡。
    /// DrawCard可以直接调用在别的脚本中
    /// </summary>

    /// 计算所有稀有度的总概率。
    float total = raritySettings.Sum(r => r.currentProbability);
    Debug.Log("total: " + total);
    /// 生成一个 0 到总概率之间的随机数。
    float random = Random.Range(0f, total);

    /// 初始化一个变量来累加概率。
    float currentProbability = 0f;
    /// 根据概率选择卡。
    RaritySetting selectedRarity = null;
    foreach (var setting in raritySettings)
    {
        // 将当前稀有度的概率加到累加器中。
        currentProbability += setting.probability;
        Debug.Log("currentProbability: " + currentProbability);

        // 如果随机数落在当前概率范围内，选择这个稀有度。
        if (random <= currentProbability)
        {
            selectedRarity = setting;
            break;
        }
    }

    // 从选中的稀有度的卡列表中随机选择一张卡。
    Card selectedCard = selectedRarity.cards[Random.Range(0, selectedRarity.cards.Count)];

  
    /// 如果选中的卡不是传说卡，调整传说卡的概率。
    if (drawCount >= probabilityCount && selectedRarity.rarity != Rarity.Legend)
    {
        AdjustLegendProbability();
    }
    else if (selectedRarity.rarity == Rarity.Legend)
    {
        // 如果选中的卡是传说卡，重置所有概率。
        ResetProbabilities();
    }
    drawCount++;
    Debug.Log("drawCount: " + drawCount);
    // 返回选中的卡。
    return selectedCard;

    }

    ///<summary>
    ///Adjust the probability of the legend card
    ///</summary>
    private void AdjustLegendProbability()
    {
        ///找到传奇概率设置和非传奇概率设置
        var legendSetting = raritySettings.Find(r => r.rarity == Rarity.Legend);
        var nonLegendSettings = raritySettings.Where(r => r.rarity != Rarity.Legend).ToList();
        
        ///计算新的概率       
        float newLegendProb = legendSetting.probability + legendProbabilityIncrease;

        ///确保概率不超过100%
        newLegendProb = Mathf.Clamp(newLegendProb, 0f, 100f);

        ///计算重新计算的概率    
        float delta = newLegendProb - legendSetting.probability;
        if(delta <= 0)return;
        
        ///计算非传奇概率设置的总概率
        float totalNonLegend = nonLegendSettings.Sum(r => r.probability);
        if(totalNonLegend <= 0 )return;
        
        ///计算每个非传奇概率设置的新概率
        ///确保概率不低于0%
        foreach (var setting in nonLegendSettings)
        {
            float proportion = setting.probability/totalNonLegend;
            setting.probability -= delta * proportion;
            setting.probability = Mathf.Max(setting.probability,0);
        }
        legendSetting.probability = newLegendProb;
    }

    ///<summary>
    ///重置概率
    ///</summary>
    private void ResetProbabilities()
    {
        foreach (var setting in raritySettings)
        {
            setting.probability = setting.currentProbability;
        }
        drawCount = 0;
        NormalizeProbabilities(); // 确保概率总和为100%
    }

    ///<summary>
    ///归一化概率，确保所有稀有度的概率总和为100%
    ///</summary>
    
    private void NormalizeProbabilities()
    {
        float total = raritySettings.Sum(r => r.probability);
        if (Mathf.Abs(total - 100) > 0.01f)
        {
            Debug.LogWarning("Probabilities not normalized! Total: " + total);
        }
    }
}
