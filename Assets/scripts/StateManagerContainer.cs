using Assets.Network.Client;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManagerContainer : MonoBehaviour {
    public GameStateManager manager = new GameStateManager();
    public Client client;

    private void Start() {
        DontDestroyOnLoad(gameObject.transform);
        client = FindObjectOfType<Client>();
    }

    public bool IsOtherPlayerAtThisLocation(location location) {
        return manager.Player2.CurrentLocation.Name.Value.Equals(location.Name.Value);
    }

    public void Goto(location location) {
        if (IsOtherPlayerAtThisLocation(location)) {
            // Maybe address group sign up
        }

        if (!manager.Player1.Goto(location)) {
            return;
        }

        client.Communication.SendObject(location);
        SceneManager.LoadScene("scenes/Quest");
    }

    public void StartQuest(Quest quest) {
        manager.Player1.StartQuest(quest);
        client.Communication.SendObject(quest);
        SceneManager.LoadScene("scenes/Choice");
    }

    public void Choose(choicesChoice choiceCopy) {
        manager.Player1.Choose(choiceCopy);
        client.Communication.SendObject(choiceCopy);
        SceneManager.LoadScene(manager.Player1.CurrentQuest == null
            ? "scenes/Quest"
            : "scenes/Choice");
    }

    public void BackToLocations() {
        SceneManager.LoadScene("scenes/Locations");
    }
}