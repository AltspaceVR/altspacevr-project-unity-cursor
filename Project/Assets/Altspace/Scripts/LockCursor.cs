using UnityEngine;
using System.Collections;

public class LockCursor : MonoBehaviour {
	void Update ()
	{
		Screen.lockCursor = true;

		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
