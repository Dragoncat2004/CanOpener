using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode] // 에디터에서도 실시간으로 확인 가능
public class CollectionalPanelGridLayout : MonoBehaviour
{
    [Header("열 설정")]
    public int columns = 3;         // 한 줄에 보여줄 카드 개수
    public float cardRatio = 1.4f;  // 카드 세로/가로 비율 (예: 1.4)

    [Header("간격 설정 (비율)")]
    [Range(0f, 0.2f)]
    public float spacingPercent = 0.02f; // 너비의 몇 %를 간격으로 쓸지 (0.02 = 2%)

    [Header("여백 설정 (비율)")]
    [Range(0f, 1.0f)]
    public float paddingPercent = 0.02f; // 너비의 몇 %를 여백으로 쓸지 (0.02 = 2%)

    [Header("참조 연결")]
    public RectTransform viewportRect;

    private GridLayoutGroup grid;

    void Awake()
    {
        grid = GetComponent<GridLayoutGroup>();
        if (viewportRect == null && transform.parent != null)
        {
            viewportRect = transform.parent.GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        UpdateLayout();
    }

    void UpdateLayout()
    {
        if (grid == null || viewportRect == null) return;

        float totalWidth = viewportRect.rect.width;
        if (totalWidth <= 0) return;

        // 1. Spacing 계산 (전체 너비 기준 비율)
        float dynamicSpacing = totalWidth * spacingPercent;
        grid.spacing = new Vector2(dynamicSpacing, dynamicSpacing);

        // 2. Padding과 Spacing 합계 계산
        float spacingSum = grid.spacing.x * (columns - 1);
        float paddingSum = grid.padding.left + grid.padding.right;

        // 3. 가용 너비 계산
        float availableWidth = totalWidth - spacingSum - paddingSum;

        if (availableWidth > 0)
        {
            // 4. Cell Size 결정
            float cellWidth = availableWidth / columns;
            float cellHeight = cellWidth * cardRatio;

            grid.cellSize = new Vector2(cellWidth, cellHeight);
        }

        grid.padding.left = (int)(totalWidth * paddingPercent);
        grid.padding.right = (int)(totalWidth * paddingPercent);
    }
}