using System.Collections.Generic;
using UnityEngine;

public class CollectionUI : MonoBehaviour
{
    [Header("데이터베이스")]
    [Tooltip("화면에 보여줄 CardData 에셋들을 여기에 끌어다 놓으세요.")]
    public List<CardData> allCards;

    [Header("UI 연결")]
    [Tooltip("CardDisplay 스크립트가 붙어있는 프리팹")]
    public GameObject cardPrefab;

    [Tooltip("Scroll View 내부의 Content 오브젝트")]
    public Transform contentParent;

    void Start()
    {
        // 게임이 시작되면 자동으로 카드를 나열합니다.
        GenerateCollection();
    }

    public void GenerateCollection()
    {
        // 1. 혹시 이미 생성된 더미 카드가 있다면 깨끗하게 지워줍니다.
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // 2. 리스트에 있는 데이터를 하나씩 돌면서 프리팹을 찍어냅니다.
        foreach (CardData data in allCards)
        {
            GameObject newCard = Instantiate(cardPrefab, contentParent);

            // 3. 방금 생성된 카드에서 CardDisplay를 찾아 데이터를 꽂아줍니다!
            if (newCard.TryGetComponent(out CardDisplay display))
            {
                display.Setup(data);
            }
        }
    }
}