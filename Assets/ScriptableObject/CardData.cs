using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    [Header("기본 정보")]
    public string cardName;        // 카드 이름
    [TextArea]
    public string description;     // 설명
    public Sprite cardArt;         // 카드 일러스트

    [Header("효과 설정")]
    public int energyCost;         // 소모 기력
    public int satisfactionValue;  // 제공 만족도 점수
    public CardType type;          // 카드 타입 (Offering - 공물/ Action - 활동)

    public enum CardType { Action, Offering }
}
