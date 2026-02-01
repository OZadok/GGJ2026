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
    MainManager.Instance.ResetGame();
  }
  
  public void ExitGame()
  {
    #if !UNITY_EDITOR 
    Application.Quit();
    #endif
  }
}
