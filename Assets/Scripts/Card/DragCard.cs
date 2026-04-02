using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform dragCanvas;

    // 드래그할 때 마우스를 따라다닐 '가짜 복제본'을 담을 변수
    private GameObject dragGhost;

    // 내 카드의 정보 (나중에 덱에 넘겨주기 위해 필요함)
    public CardData myCardData;

    void Awake()
    {
        GameObject canvasObj = GameObject.Find("DragCanvas");
        if (canvasObj != null) dragCanvas = canvasObj.transform;

        // 만약 CardDisplay 스크립트가 붙어있다면 데이터를 자동으로 가져옴
        if (TryGetComponent(out CardDisplay display))
        {
            myCardData = display.cardData;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 1. 원본(나 자신)은 가만히 두고, 나와 똑같이 생긴 '가짜'를 생성합니다.
        dragGhost = Instantiate(this.gameObject, dragCanvas);

        // ⭐ [핵심 해결책] 원본 카드의 실제 크기를 유령에게 똑같이 적용합니다! ⭐
        RectTransform originalRect = GetComponent<RectTransform>();
        RectTransform ghostRect = dragGhost.GetComponent<RectTransform>();

        // Grid Layout이 만들어준 원본의 폭(width)과 높(height)을 그대로 복사
        ghostRect.sizeDelta = new Vector2(originalRect.rect.width, originalRect.rect.height);

        // (선택) 만약 카드가 마우스 포인터 정중앙에 안 온다면 pivot도 맞춰줍니다.
        ghostRect.pivot = originalRect.pivot;

        // 2. 가짜 카드는 드래그 용도이므로 광선(Raycast)을 막지 않게 설정합니다.
        // (그래야 가짜 카드 너머에 있는 덱 슬롯이 마우스를 인식할 수 있음)
        if (dragGhost.TryGetComponent(out CanvasGroup ghostGroup))
        {
            ghostGroup.blocksRaycasts = false;
            //ghostGroup.alpha = 0.8f; // 약간 투명하게 해서 드래그 중임을 표현
        }
        // 가짜 카드에 붙어있는 CardDisplay를 찾아서, 내 데이터를 다시 주입해줍니다.
        if (dragGhost.TryGetComponent(out CardDisplay ghostDisplay))
        {
            ghostDisplay.Setup(myCardData);
            Destroy(dragGhost.GetComponent<DragCard>());
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 3. 원본 대신 '가짜'가 마우스를 따라다니게 합니다.
        if (dragGhost != null)
        {
            dragGhost.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 4. 마우스에서 손을 떼면 임무를 다한 가짜는 삭제합니다.
        if (dragGhost != null)
        {
            Destroy(dragGhost);
        }
    }
}