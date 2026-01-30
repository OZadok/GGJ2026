using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelClearedPanel: MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        public void Init(int level)
        {
            text.text = $"Cleared Level {level}";
            gameObject.SetActive(true);
        }
        
        public void NextLevel()
        {
            MainManager.Instance.NextLevel();
            gameObject.SetActive(false);
        }
    }
}