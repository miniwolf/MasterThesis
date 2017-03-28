﻿using System.Collections.Generic;
using Assets.scripts;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PreFiller : MonoBehaviour {
    private GameObject grid;
    private StateManagerContainer manager;

    public GameObject ButtonTemplate;

    // Use this for initialization
    public void Start() {
        grid = GameObject.FindGameObjectWithTag("PreGrid");
        Assert.IsNotNull(ButtonTemplate);
        manager = GameObject.FindGameObjectWithTag("StateManager")
            .GetComponent<StateManagerContainer>();;
        FillGrid(manager.manager.Player1.State);
    }

    private void FillGrid(IEnumerable<string> states) {
        foreach (var state in states) {
            if (state == null) {
                continue; // TODO: Fucking fix this
            }
            var buttonInstance = Instantiate(ButtonTemplate);
            buttonInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(220, 88);
            buttonInstance.GetComponentInChildren<Text>().text = state;

            buttonInstance.transform.parent = grid.transform;
        }
    }
}