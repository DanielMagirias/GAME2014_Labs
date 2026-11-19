using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void StartButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
