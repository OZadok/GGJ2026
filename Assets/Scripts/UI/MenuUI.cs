using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
  [SerializeField] GameObject credits;
  public void ShowCredits()
  {
    credits.SetActive(true);
  }
  public void Restart()
  {
    SceneManager.LoadScene(0);
  }
  public void ExitGame()
  {
    #if !UNITY_EDITOR
    Application.Quit();
    #endif
  }
}
