using System.Collections.Generic;
using Assets.scripts;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class QuestFiller : MonoBehaviour {
    private GameObject grid;
    private StateManagerContainer manager;

    public GameObject ButtonTemplate;

    // Use this for initialization
    public void Start() {
        grid = GameObject.FindGameObjectWithTag("LevelGrid");
        Assert.IsNotNull(ButtonTemplate);
        manager = GameObject.FindGameObjectWithTag("StateManager")
            .GetComponent<StateManagerContainer>();;
        FillGrid(manager.manager.PossibleQuests);
    }

    private void FillGrid(IEnumerable<Quest> quests) {
        foreach (var quest in quests) {
            if (quest == null) {
                continue; // TODO: Fucking fix this
            }
            var buttonInstance = Instantiate(ButtonTemplate);
            buttonInstance.GetComponentInChildren<Text>().text = quest.Name.Value;
            var button = buttonInstance.GetComponent<Button>();
            var questCopy = quest;
            button.onClick.AddListener(delegate { manager.StartQuest(questCopy); });

            buttonInstance.transform.parent = grid.transform;
        }
    }
}
