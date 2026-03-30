using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<CardData> allCards; 

    private List<CardData> drawPile = new List<CardData>();
    private List<CardData> hand = new List<CardData>();
    private List<CardData> discardPile = new List<CardData>();

    void Start()
    {
        setUpDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setUpDeck()
    {
        drawPile.AddRange(allCards);

    }

    public void ShuflfleDeck()
    {
        for (int i = 0; i < drawPile.Count; i++)
        {
            CardData temp = drawPile[i];
            int randomIndex = Random.Range(i, drawPile.Count);
            drawPile[i] = drawPile[randomIndex];
            drawPile[randomIndex] = temp;
        }
        Debug.Log("셔플");
    }

    public void DrawCard(int amout)
    {
        for (int i = 0; i < amout; i++)
        {
            if (drawPile.Count == 0)
            {
                ReshuffleDiscardIntoDraw();
            }

            if (drawPile.Count > 0)
            {
                CardData drawnCard = drawPile[0];
                drawPile.RemoveAt(0);
                hand.Add(drawnCard);
                Debug.Log($"{drawnCard.cardName}을(를) 뽑았습니다.");

                //핸드 호출
            }
        }
    }

    void ReshuffleDiscardIntoDraw()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
        ShuflfleDeck();
    }
}
