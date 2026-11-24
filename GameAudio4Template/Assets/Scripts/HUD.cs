using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI scoreValue;

    private float score = 0;
    
    public void IncreasedScore(int add)
    {
        score += add;
        scoreValue.text = score.ToString();
    }
}