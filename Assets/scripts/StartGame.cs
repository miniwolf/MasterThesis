using Assets.Network.Shared.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts {
    public class StartGame : MonoBehaviour {
        public void Game(string clazz) {
            var manager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManagerContainer>();
            manager.manager.Player1.ClassString = clazz;
            manager.client.Communication.SendObject(new ClassChosen(clazz));
            SceneManager.LoadScene("Locations");
        }
    }
}
