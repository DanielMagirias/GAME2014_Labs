using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;

    [SerializeField] private TMP_Text screenText;

    private float scoreTextHalfHeight;
    private float scoreTextHalfWidth;
    private float livesTextHalfHeight;
    private float livesTextHalfWidth;

    void Start()
    {
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
        
            case ScreenOrientation.LandscapeLeft:
                screenText.text = "LandscapeLeft";
                break;

            case ScreenOrientation.LandscapeRight:
                screenText.text = "LandscapeRight";
                break;

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
}
