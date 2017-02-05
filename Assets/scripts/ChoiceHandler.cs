using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts {
    public class ChoiceHandler : MonoBehaviour {
        Text choice1, choice2, choice3;

        private void Start() {
            choice1 = GameObject.FindGameObjectWithTag(TagConstants.Choice1).GetComponent<Text>();
            choice2 = GameObject.FindGameObjectWithTag(TagConstants.Choice2).GetComponent<Text>();
            choice3 = GameObject.FindGameObjectWithTag(TagConstants.Choice3).GetComponent<Text>();
        }
    }
}
