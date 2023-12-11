using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
	Player player;

	private void Start()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
	}

	public void OnClickRestart()
	{
		player.GameEndPanal.SetActive(false);
		Time.timeScale = 1;
		SceneManager.LoadScene(1);
	}

	public void OnClickExit()
	{
		Application.Quit();
	}

	public void OnClickstart()
	{
		SceneManager.LoadScene(1);
	}

	public void OnClickTitle()
	{
		SceneManager.LoadScene(0);
	}

	public void OnClickPause()
	{
		if (!player.isPause)
		{
			player.isPause = true;
		}

		else
		{
			player.isPause = false;
		}	
	}
}
