using System.Collections;
using System.Collections.Generic;
using Assets.scripts;
using UnityEngine;

public class BackToLocations : MonoBehaviour {
    public void Click() {
        var container = GameObject.FindGameObjectWithTag("StateManager")
            .GetComponent<StateManagerContainer>();
        container.BackToLocations();
    }
}