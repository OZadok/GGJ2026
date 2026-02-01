using UnityEngine;

namespace UI
{
    public class GameOverPanel: MonoBehaviour
    {
        public void Reset()
        {
            MainManager.Instance.ResetLevel();
            gameObject.SetActive(false);
        }
    }
}