using Assets.Network.Client.Handlers;
using Assets.scripts;
using UnityEngine;

namespace Assets.Network.Client {
    public class Client : MonoBehaviour {
        public Communication Communication { get; private set; }

        public void Start() {
            Communication = new Communication("localhost", 8001);
            var stateManagerContainer = FindObjectOfType<StateManagerContainer>();
            GeneralHandlerFactory.Construct(stateManagerContainer.manager);
            DontDestroyOnLoad(gameObject.transform);
        }

        public void Close() {
            Communication.Close();
        }
    }
}