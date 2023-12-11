using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    Player player;
    EnemySpawn enemyspawn;
    Score score;
    public bool skillcheck;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        score = GameObject.Find("ScoreCounter").GetComponent<Score>();
        enemyspawn = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        skillcheck = player.SkillCheck();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && player.OnOffSkill)
        {
            Destroy(collision.gameObject);
            score.ScoreUp();
            score.ComboUp();
            enemyspawn.EnemyCountDown();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && player.OnOffSkill)
        {
            Destroy(collision.gameObject);
            score.ScoreUp();
            score.ComboUp();
            enemyspawn.EnemyCountDown();
        }
    }

}
