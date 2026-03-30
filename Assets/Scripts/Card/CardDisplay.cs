using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [Header("연결할 데이터")]
    public CardData cardData;

    [Header("UI 요소들")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;
    public Image cardImage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cardData != null)
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        nameText.text = cardData.name;
        descriptionText.text = cardData.description;
        costText.text = cardData.energyCost.ToString();
        cardImage.sprite = cardData.cardArt;
    }
}
