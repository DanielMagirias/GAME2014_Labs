using TMPro;
using UnityEngine;

public class EndSceneUI : MonoBehaviour
{
    [SerializeField] private TMP_Text finalScoreText;

    void Start()
    {
        finalScoreText.text = "Final Score: " + GameController.totalScore;
    }
}