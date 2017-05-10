using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	public void Game() {
		SceneManager.LoadScene("Locations");
	}
}
