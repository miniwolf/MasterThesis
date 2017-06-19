using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts {
    public class ChangeScene : MonoBehaviour {
        public void NextLevelButton(string levelName) {
            SceneManager.LoadScene(levelName);
        }
    }
}