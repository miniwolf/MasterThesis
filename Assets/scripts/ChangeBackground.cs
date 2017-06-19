using UnityEngine;

namespace Assets.scripts {
    public class ChangeBackground : MonoBehaviour {
        // Use this for initialization
        private void Start() {
            var manager = GameObject.FindGameObjectWithTag("StateManager")
                .GetComponent<StateManagerContainer>();

            var canvas = GameObject.FindGameObjectWithTag("Canvas");
            if (canvas == null) {
                return;
            }

            foreach (Transform child in canvas.transform) {
                if (!child.name.Equals(manager.manager.Player1.CurrentLocation.Name)) {
                    continue;
                }
                child.gameObject.SetActive(true);
                Debug.Log("Dimmerminner");
                break;
            }
        }
    }
}