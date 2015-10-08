using UnityEngine;
using System.Collections;

public class optionsScript : MonoBehaviour 
{

	public void ChangeScene(string sceneName)
	{
		Application.LoadLevel(sceneName);
	}

}
