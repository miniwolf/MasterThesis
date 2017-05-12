using UnityEngine;

namespace Assets.scripts {
    public class BackToLocations : MonoBehaviour {
        public void Click() {
            var container = GameObject.FindGameObjectWithTag("StateManager")
                .GetComponent<StateManagerContainer>();
            container.BackToLocations();
        }
    }
}