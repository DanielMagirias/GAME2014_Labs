using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;

    [SerializeField] private TMP_Text screenText;

    private float scoreTextHalfHeight;
    private float scoreTextHalfWidth;
    private float livesTextHalfHeight;
    private float livesTextHalfWidth;

    private PlayerScript ps;

    public static int totalScore;

    void Start()
    {

        totalScore = 0;

        ps = FindAnyObjectByType<PlayerScript>();
        UpdateLivesText();

        scoreTextHalfHeight = scoreText.rectTransform.rect.height * 0.5f;
        scoreTextHalfWidth = scoreText.rectTransform.rect.width * 0.5f;

        livesTextHalfHeight = livesText.rectTransform.rect.height * 0.5f;
        livesTextHalfWidth = livesText.rectTransform.rect.width * 0.5f;
    }

    void Update()
    {

        scoreText.rectTransform.position = new Vector2(Screen.safeArea.xMin + scoreTextHalfWidth, Screen.safeArea.yMax - scoreTextHalfHeight);
        livesText.rectTransform.position = new Vector2(Screen.safeArea.xMax - livesTextHalfWidth, Screen.safeArea.yMax - livesTextHalfHeight);

        switch (Screen.orientation) { 

            case ScreenOrientation.Portrait:
                screenText.text = "Portrait";
                break;

            case ScreenOrientation.PortraitUpsideDown:
                screenText.text = "PortraitUpsideDown";
                break;

            default:
                screenText.text = "unknown";
                break;

        }

    }

    public void UpdateLivesText()
    {
        livesText.text = "Lives: " + ps.lives;
    }

    public void UpdateScoreText(int score)
    {
        totalScore += score;
        scoreText.text = "Score: " + totalScore;
    }
}
