using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{


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


}
