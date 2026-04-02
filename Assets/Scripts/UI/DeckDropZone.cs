using UnityEngine;
using UnityEngine.EventSystems;

public class DeckDropZone : MonoBehaviour, IDropHandler
{
    public GameObject cardPrefab; // 덱에 생성할 카드 프리팹

    public void OnDrop(PointerEventData eventData)
    {
        // eventData.pointerDrag에는 항상 '처음 클릭했던 원본'이 들어있습니다.
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent(out DragCard originalCard))
            {
                Debug.Log($"<color=cyan>[덱 편집기]</color> '{originalCard.myCardData.cardName}' 카드를 덱에 추가합니다!");

                // 1. 덱 슬롯(내 아래)에 새로운 진짜 카드를 생성합니다.
                GameObject newCardInDeck = Instantiate(cardPrefab, this.transform);

                // 2. 원본 카드가 가지고 있던 데이터를 새 카드에 주입합니다.
                if (newCardInDeck.TryGetComponent(out CardDisplay display))
                {
                    display.Setup(originalCard.myCardData);
                }

                // 3. (선택) 여기에 DeckManager.AddCard(originalCard.myCardData) 같은 로직 추가
            }
        }
    }
}