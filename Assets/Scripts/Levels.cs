using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public GameObject ball,panel;

    private void Update()
    {
        OpenPanel();
    }

    private void OpenPanel()
    {
        if(ball == null || ball.GetComponent<MainScript>()._collidersBricks == null)
            panel.SetActive(true);
    }

    public void RestartButton(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void NExtLevelButton(int index)
    {
        SceneManager.LoadScene(index);
    }
}