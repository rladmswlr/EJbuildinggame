using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	SpriteRenderer spriteRenderer;
	Rigidbody2D E_rigid;
	public Sprite[] sprites;

	Score score;
	Player player;
	EnemySpawn enemyspawn;

	public bool PerringMonster = false;
	private float PerringCoolTime = 0.1f;

	public bool Alive = true;

	//적의 정보 세팅
	[Header("[적 세팅]")]
	public float E_Hp = 1f;
	public float E_Speed = 8f;

	// Start is called before the first frame update
	private void Start()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
		enemyspawn = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>();
		score = GameObject.Find("ScoreCounter").GetComponent<Score>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		E_rigid = GetComponent<Rigidbody2D>();
	}

	private void LateUpdate()
	{
		if (PerringMonster)
		{

			if (enemyspawn.spawnCount == 5)
			{
				E_rigid.AddForce(Vector2.up * 0.5f, ForceMode2D.Impulse);
			}
			else if (enemyspawn.spawnCount == 4)
			{
				E_rigid.AddForce(Vector2.up * 0.3f, ForceMode2D.Impulse);
			}
			else if (enemyspawn.spawnCount == 3)
			{
				E_rigid.AddForce(Vector2.up * 0.2f, ForceMode2D.Impulse);
			}
			else if (enemyspawn.spawnCount == 2)
			{
				E_rigid.AddForce(Vector2.up * 0.1f, ForceMode2D.Impulse);
			}
			else if (enemyspawn.spawnCount == 1)
				E_rigid.AddForce(Vector2.up * 0.03f, ForceMode2D.Impulse);

			//Debug.Log("Perring");
			PerringCoolTime = 0.1f;
			PerringMonster = false;
		}

		PerringCoolTimeSet();
	}


	public void OnHit(int dmg)
	{
		E_Hp -= dmg;
		player.Damaged(player.Damage);

		if (E_Hp <= 0)
		{
			enemyspawn.EnemyCountDown();
			score.ComboUp();
			score.ScoreUp();
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "BreakBlock")
			StartCoroutine(BreakZone());

	}


	private void OnTriggerStay2D(Collider2D collision)
	{

		if (collision.gameObject.tag == "Sword" && player.isAttack)
		{
			player.HitEnemySound();
			OnHit(player.Damage);
			player.isAttack = false;
		}

		if (collision.gameObject.tag == "Shield"&& player.isPerring)
		{
			if (enemyspawn.spawnCount == 5)
			{
				E_rigid.AddForce(Vector2.up * 80f, ForceMode2D.Impulse);
			}
			else if (enemyspawn.spawnCount == 4)
			{
				E_rigid.AddForce(Vector2.up * 60f, ForceMode2D.Impulse);
			}
			else if (enemyspawn.spawnCount == 3)
			{
				E_rigid.AddForce(Vector2.up * 30f, ForceMode2D.Impulse);
			}
			else if (enemyspawn.spawnCount == 2)
			{
				E_rigid.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);
			}
			else if (enemyspawn.spawnCount == 1)
				E_rigid.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
		
			player.HitShieldSound();
			PerringMonster = true;

			player.isPerring = false;
		}

	}

	IEnumerator BreakZone()
	{
		yield return new WaitForSeconds(0.5f);
		Debug.Log("2222");
		score.ResetCombo();
		enemyspawn.EnemyCountDown();
		Destroy(gameObject);
		player.PlayerDamaged();
	}

	public void PerringCoolTimeSet()
	{
		if (PerringMonster)
		{
			PerringCoolTime -= Time.deltaTime;

			if (PerringCoolTime <= 0.0f)
			{
				PerringCoolTime = 0.1f;
				PerringMonster = false;
			}
		}
	}

}
