using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
	public GameObject[] EnemyPrefab;

	private float spawnPosX = 0f;
	private float spawnPosY = 70f;

	public int spawnCount = 0;
	private int maxspawn = 5;
	public bool IsSpawn = false;
	private bool IsRound = true;

	public int RoundCount = 0;
	private int MaxRound = 20;
	public Slider RoundSlider;

	public Text RoundText;
	public Image RoundBackGround;

	public float texttime = 2.0f;
	bool canSeeText = false;

	private void Start()
	{
		RoundText = GameObject.Find("RoundText").GetComponent<Text>();
		RoundBackGround.GetComponent<Image>().gameObject.SetActive(false);
		RoundSlider.value = 0;
		RoundSlider.minValue = 0;
		RoundSlider.maxValue = MaxRound;
	}

	void Update()
	{
		if (spawnCount < maxspawn && !IsSpawn)
		{
			MakeEnemy();
		}

		else if (spawnCount == maxspawn)
		{

			IsSpawn = true;
		}

		else if (spawnCount == 0)
		{
			StartCoroutine(DelaySpawn());
			IsRound = true;
		}

		if (spawnCount == maxspawn && IsRound)
		{
			canSeeText = true;
			RoundBackGround.GetComponent<Image>().gameObject.SetActive(true);
			RoundCount++;
			RoundText.text = RoundCount + "라운드 시작";
			
			IsRound = false;
		}

		RoundSlider.value = RoundCount;
		TextTimer();
	}

	public void EnemyCountDown()
	{
		spawnCount--;
	}

	void MakeEnemy()
	{
		spawnCount++;
		Vector2 spawnPos = new Vector2(spawnPosX, spawnPosY + 14.5f);

		if (RoundCount >= 0 && RoundCount <= 2)
		{
			int EnemyIndex = Random.Range(0, 1);
			GameObject enemy = Instantiate(EnemyPrefab[EnemyIndex], spawnPos, EnemyPrefab[EnemyIndex].transform.rotation);
			enemy.name = "Enemy";
		}

		else if (RoundCount > 2 && RoundCount <= 5)
		{
			int EnemyIndex = Random.Range(0, 2);
			GameObject enemy = Instantiate(EnemyPrefab[EnemyIndex], spawnPos, EnemyPrefab[EnemyIndex].transform.rotation);
			enemy.name = "Enemy";
		}

		else if (RoundCount < 10 && RoundCount > 5)
		{
			int EnemyIndex = Random.Range(0, 2);
			GameObject enemy = Instantiate(EnemyPrefab[EnemyIndex], spawnPos, EnemyPrefab[EnemyIndex].transform.rotation);
			enemy.name = "Enemy";
		}

		else if (RoundCount >= 10 && RoundCount < 15)
		{
			int EnemyIndex = Random.Range(2, 4);
			GameObject enemy = Instantiate(EnemyPrefab[EnemyIndex], spawnPos, EnemyPrefab[EnemyIndex].transform.rotation);
			enemy.name = "Enemy";
		}

		else if(RoundCount >= 15)
		{
			int EnemyIndex = Random.Range(4, 6);
			GameObject enemy = Instantiate(EnemyPrefab[EnemyIndex], spawnPos, EnemyPrefab[EnemyIndex].transform.rotation);
			enemy.name = "Enemy";
		}

	}

	IEnumerator DelaySpawn()
	{
		spawnCount = 0;
		yield return new WaitForSeconds(2.0f);
		IsSpawn = false;
	}

	IEnumerator DelayRoundText()
	{
		RoundBackGround.GetComponent<Image>().gameObject.SetActive(true);
		RoundText.text = RoundCount + "라운드 시작";
		yield return new WaitForSeconds(2.0f);

		RoundBackGround.GetComponent<Image>().gameObject.SetActive(false);
	}

	void TextTimer()
	{
		if (canSeeText)
		{
			texttime -= Time.deltaTime;
			if (texttime < 0f)
			{
				RoundBackGround.GetComponent<Image>().gameObject.SetActive(false);
				texttime = 2.0f;
				canSeeText = false;
			}
		}
	}

}
