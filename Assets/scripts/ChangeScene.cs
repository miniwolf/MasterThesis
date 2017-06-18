using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {

	public void NextLevelButton(string levelName)
	{
		Application.LoadLevel(levelName);
	}
}
