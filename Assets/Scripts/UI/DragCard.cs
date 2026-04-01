using UnityEngine;
using UnityEngine.EventSystems; // 드래그 이벤트를 위해 필수!
using UnityEngine.UI;

// 이 스크립트를 붙이면 CanvasGroup 컴포넌트가 자동으로 붙습니다.
[RequireComponent(typeof(CanvasGroup))]
public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent; // 원래 있던 스크롤 뷰의 위치 기억
    private CanvasGroup canvasGroup;
    private Transform dragCanvas;     // 맨 앞으로 빼낼 캔버스

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        // 씬에서 DragCanvas를 자동으로 찾습니다.
        GameObject canvasObj = GameObject.Find("DragCanvas");
        if (canvasObj != null) dragCanvas = canvasObj.transform;
    }

    // 1. 드래그 시작할 때 (카드를 집어들 때)
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 돌아갈 고향(부모) 위치 기억
        originalParent = transform.parent;

        // 다른 UI에 가려지지 않게 DragCanvas로 이사! (화면 맨 앞으로 옴)
        transform.SetParent(dragCanvas);

        // 레이캐스트 끄기 (내 마우스 포인터가 내 밑에 있는 '덱 슬롯'을 볼 수 있게 해줌)
        canvasGroup.blocksRaycasts = false;

        // (선택) 드래그 중일 때 카드를 살짝 투명하게 만들기
        canvasGroup.alpha = 0.8f;
    }

    // 2. 드래그 중일 때 (마우스 따라다니기)
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // 3. 드래그 끝났을 때 (마우스에서 손을 뗄 때)
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // 레이캐스트 다시 켜기
        canvasGroup.alpha = 1.0f;          // 투명도 원상복구

        // 만약 카드가 여전히 DragCanvas에 남아있다면? (덱 슬롯에 안 들어갔다면)
        if (transform.parent == dragCanvas)
        {
            ReturnToOriginalPosition(); // 원래 자리로 튕겨 돌아감
        }
    }

    // 원래 자리로 돌려보내는 함수
    public void ReturnToOriginalPosition()
    {
        transform.SetParent(originalParent);
    }
}