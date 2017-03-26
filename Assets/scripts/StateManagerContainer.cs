using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts {
    public class StateManagerContainer : MonoBehaviour {
        public GameStateManager manager = new GameStateManager();

        private void Start() {
            DontDestroyOnLoad(gameObject.transform);
        }

        public void Goto(location location) {
            SceneManager.LoadScene("scenes/ChooseNpc");
            manager.Player1.Goto(location);
        }
    }
}