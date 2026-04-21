using UnityEngine;
using UnityEngine.EventSystems;

public class DeckDropZone : MonoBehaviour, IDropHandler
{
    public GameObject cardPrefab;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent(out DragCard originalCard))
            {
                if (originalCard.myCardData == null)
                {
                    Debug.LogWarning($"<color=yellow>[덱 편집기]</color> 드래그된 카드에 카드 데이터가 없습니다!");
                    return;
                }   

                CardData droppedData = originalCard.myCardData;
                if (droppedData != null)
                {
                    Debug.Log($"<color=cyan>[덱 편집기]</color> '{droppedData.cardName}' 카드를 덱에 추가합니다!");
                }

                    GameObject newCardInDeck = Instantiate(cardPrefab, this.transform);
                if (newCardInDeck.TryGetComponent(out CardDisplay display))
                {
                    display.Setup(droppedData);
                }
            }
        }
    }
}