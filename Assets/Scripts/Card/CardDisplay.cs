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

    public void Setup(CardData newData)
    {
        cardData = newData;
        UpdateUI();
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
        if (cardData == null) return;

        if (nameText != null) nameText.text = cardData.cardName;
        if (descriptionText != null) descriptionText.text = cardData.description;
        if (costText != null) costText.text = cardData.energyCost.ToString();

        if (cardImage != null && cardData.cardArt != null)
        {
            cardImage.sprite = cardData.cardArt;
        }
    }
}
