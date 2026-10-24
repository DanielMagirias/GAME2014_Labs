using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button rulesButton;
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_Text rulesText;

    public void PlayButton() {

        SceneManager.LoadScene(1);
        Debug.Log("Play button pressed");
    }

    public void BackButton()
    {

        SceneManager.LoadScene(0);
        Debug.Log("Back/Restart button pressed");
    }

    public void NextButton()
    {

        SceneManager.LoadScene(2);
        Debug.Log("Next button pressed");
    }

    public void RulesButton()
    {
        rulesText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        rulesButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
    }

    public void StartScreenBackButton()
    {
        rulesText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
        rulesButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
    }


}
