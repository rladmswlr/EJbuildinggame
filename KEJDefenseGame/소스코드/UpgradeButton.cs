using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UpgradeButton : MonoBehaviour
{
    AbilityUpGrade upgrade;

	private void Start()
	{
		upgrade = GameObject.Find("AbilityManage").GetComponent<AbilityUpGrade>();
	}
	public void OnUpgradePlayer()
    {

		GameObject clickObject = EventSystem.current.currentSelectedGameObject;

		Debug.Log(clickObject.name + "," + clickObject.GetComponentInChildren<Text>().text);

		if(clickObject.GetComponentInChildren<Text>().text == "공격속도 업(소)")
		{
			upgrade.player.AttackSpeed += 0.2f;
		}

		else if(clickObject.GetComponentInChildren<Text>().text == "체력 업")
		{
			upgrade.player.LifeUp();
		}

		else if (clickObject.GetComponentInChildren<Text>().text == "공격속도 업(중)")
		{
			upgrade.player.AttackSpeed += 0.4f;
		}

		else if (clickObject.GetComponentInChildren<Text>().text == "공격력 업(소)")
		{
			upgrade.player.Damage += 5;
		}

		else if (clickObject.GetComponentInChildren<Text>().text == "공격력 업(중)")
		{
			upgrade.player.Damage += 10;
		}

		else if (clickObject.GetComponentInChildren<Text>().text == "쉴드")
		{
			upgrade.player.isShield = true;
		}

		upgrade.canTimeStop = false;
		upgrade.firstnumber[0] = 20;
		upgrade.firstnumber[1] = 20;
		upgrade.firstnumber[2] = 20;
		foreach(Transform child in upgrade.boxzone.transform)
		{
			Destroy(child.gameObject);
		}
		upgrade.BackGround.gameObject.SetActive(false);
	}

}
