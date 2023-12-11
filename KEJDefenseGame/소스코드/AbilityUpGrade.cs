using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUpGrade : MonoBehaviour
{
    public Ability[] ability;
    public GameObject boxzone;
    public GameObject abilitybox; //ÀÚ½Ä
    public int stage;
    public Player player;
    EnemySpawn enemyspawn;
    int randomAbilityNumber;
    public int[] firstnumber = { 20,20,20};
    public Image BackGround;

    public bool canTimeStop = false;

    public bool canCheckAbility = false;
    public bool roundcheck = true;

	private void Awake()
	{
        enemyspawn = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>();
        canCheckAbility = false;
        roundcheck = true;
    }

	private void Start()
	{
        BackGround.gameObject.SetActive(false);
    }



	private void Update()
	{
        if (canTimeStop)
        {
            Time.timeScale = 0;
        }

        if (enemyspawn.RoundCount == 3 && roundcheck)
		{
            canCheckAbility = true;
            roundcheck = false;
        }

        else if (enemyspawn.RoundCount == 6 && roundcheck)
        {
            canCheckAbility = true;
            roundcheck = false;
        }

        else if (enemyspawn.RoundCount == 9 && roundcheck)
        {
            canCheckAbility = true;
            roundcheck = false;
        }

        else if (enemyspawn.RoundCount == 12 && roundcheck)
        {
            canCheckAbility = true;
            roundcheck = false;
        }

        else if (enemyspawn.RoundCount == 15 && roundcheck)
        {
            canCheckAbility = true;
            roundcheck = false;
        }

        else if (enemyspawn.RoundCount == 18 && roundcheck)
        {
            canCheckAbility = true;
            roundcheck = false;
        }

        else if(enemyspawn.RoundCount%3 != 0)
		{
            roundcheck = true;
		}

        if (canCheckAbility)
		{
            BackGround.gameObject.SetActive(true);
            while (true)
			{
                randomAbilityNumber = Random.Range(0, ability.Length);

                if (firstnumber[0] == 20)
				{
                    randomAbilityNumber = Random.Range(0, ability.Length);
                    firstnumber[0] = randomAbilityNumber;
                }

                if (firstnumber[1] == 20)
                {
                    randomAbilityNumber = Random.Range(0, ability.Length);
                    firstnumber[1] = randomAbilityNumber;
                }
                if (firstnumber[2] == 20)
                {
                    randomAbilityNumber = Random.Range(0, ability.Length);
                    firstnumber[2] = randomAbilityNumber;
                }

                if(firstnumber[1] == firstnumber[0])
				{
                    randomAbilityNumber = Random.Range(0, ability.Length);
                    firstnumber[1] = randomAbilityNumber;
                }

                else if(firstnumber[2] == firstnumber[0] || firstnumber[2] == firstnumber[1])
				{
                    randomAbilityNumber = Random.Range(0, ability.Length);
                    firstnumber[2] = randomAbilityNumber;
                }

                if (firstnumber[0] != firstnumber[1] && firstnumber[0] != firstnumber[2]
                    && firstnumber[2] != firstnumber[1])
                {
                    break;
                }
            }
            Vector3 spawnPos = new Vector3(0, 0, 0);
            for(int i = 0; i < 3; i++)
			{

                GameObject child = Instantiate(abilitybox);
                child.transform.SetParent(boxzone.transform);

                child.transform.localPosition = new Vector3(0f,0f,0f);
                child.transform.localScale = new Vector3(1f, 1f, 1f);
                child.GetComponentsInChildren<Text>()[0].text = ability[firstnumber[i]].MainText;
                child.GetComponentsInChildren<Text>()[1].text = ability[firstnumber[i]].SubText;
            }
            canCheckAbility = false;
            canTimeStop = true;
        }

    }

}
