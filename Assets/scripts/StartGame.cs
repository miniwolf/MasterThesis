using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts {
    public class StartGame : MonoBehaviour {
        public void Game() {
            SceneManager.LoadScene("Locations");
        }
    }
}
