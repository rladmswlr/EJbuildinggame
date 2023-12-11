using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int Combo = 0;
    public int pointScore = 0;

    public int bonus = 0;

    private float comboTimer = 10.0f;


    public Text scoreText;
    public Text ComboText;
    public Slider ComboSlider;

    EnemySpawn enemyspawn;

    // Start is called before the first frame update
    void Start()
    {
        enemyspawn = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>();

        ComboSlider.gameObject.SetActive(false);
        ComboText.gameObject.SetActive(false);

        ComboSlider.maxValue = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        ComboCount();
        UpdateScore();
        UpdateCombo();
        ManageCombo();
    }

    public void ScoreUp()
	{
        pointScore += 500;
        pointScore += bonus;
	}

    public void ComboUp()
	{
        Combo++;

        if (Combo >= 2)
		{
            comboTimer = 10f;
            ComboSlider.value = 10f;
        }
	}

    public void ResetCombo()
	{
        Combo = 0;
	}
    
    public void ComboCount()
	{

        if (comboTimer > 0f && enemyspawn.spawnCount != 0)
        {
            comboTimer -= Time.deltaTime;
            ComboSlider.value -= Time.deltaTime;
        }

        else if (comboTimer <= 0f && Combo >=2)
		{
            ResetCombo();
		}
	}

    void ManageCombo()
	{
        if (Combo > 0)
        {
            if (Combo > 3)
            {
                bonus = 300;
            }

            else if (Combo > 8)
            {
                bonus = 500;
            }
        }
    }

    void UpdateScore()
	{
        scoreText.text = "Score : " + pointScore;
	}

    void UpdateCombo()
	{
        if (Combo >= 2)
        {
            ComboText.text = Combo + " Combo";
            ComboSlider.gameObject.SetActive(true);
            ComboText.gameObject.SetActive(true);
        }

        else
		{
            ComboSlider.gameObject.SetActive(false);
            ComboText.gameObject.SetActive(false);
        }
	}

}
