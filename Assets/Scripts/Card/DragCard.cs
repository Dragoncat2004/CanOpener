using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform dragCanvas;
    private GameObject dragGhost;
    public CardData myCardData;

    void Awake()
    {
        GameObject canvasObj = GameObject.Find("DragCanvas");
        if (canvasObj != null) dragCanvas = canvasObj.transform;

        if (TryGetComponent(out CardDisplay display))
        {
            myCardData = display.cardData;
        }
    }

    // ⭐ 2. 매 프레임마다 우클릭을 감시합니다.
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("<color=yellow>우클릭: 드래그 시작이 취소되었습니다.</color>");
            return; // 우클릭으로 드래그 시작 자체를 막습니다.
        }

        dragGhost = Instantiate(this.gameObject, dragCanvas);

        RectTransform originalRect = GetComponent<RectTransform>();
        RectTransform ghostRect = dragGhost.GetComponent<RectTransform>();
        ghostRect.sizeDelta = new Vector2(originalRect.rect.width, originalRect.rect.height);

        if (dragGhost.TryGetComponent(out CanvasGroup ghostGroup))
        {
            ghostGroup.blocksRaycasts = false;
            ghostGroup.alpha = 0.8f;
        }

        if (dragGhost.TryGetComponent(out CardDisplay ghostDisplay))
        {
            ghostDisplay.Setup(myCardData);
        }
        Destroy(dragGhost.GetComponent<DragCard>());
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragGhost != null)
        {
            dragGhost.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 4. OnEndDrag가 정상 발동되더라도, 유령 카드가 남아있을 때만 파괴합니다.
        // (우클릭으로 이미 파괴했다면 여기는 안전하게 패스됩니다.)
        if (dragGhost != null)
        {
            Destroy(dragGhost);
            dragGhost = null;
        }
    }
}