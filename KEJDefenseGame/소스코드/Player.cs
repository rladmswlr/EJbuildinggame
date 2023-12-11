using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D p_rigid;
    protected CapsuleCollider2D p_capsule;
    public Animator p_Anim;

    public bool IsJump = false;
    public bool isAttack = false;
    private bool IsSkill = true;

    //체력 이미지오브젝트
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    public Image SkillCheckImage;
    public Image ShieldCheckImage;
    public float PerringUseTime = 0.1f;
    //방패 막기 변수
    public bool isPerring = false;
    float PerringCoolTime = 3.0f;
    public bool canPerring = true;

    public Slider SkillSlider;
    public Slider ShieldSlider;

    //데미지 스킨 
    public GameObject PlayerCanvas;
    public Text DamageText;

    //사운드 관리 변수
    private AudioSource PlayerAudio;
    public AudioClip JumpSound;
    public AudioClip SwordSound;
    public AudioClip SkillSound;
    public AudioClip HitSound;
    public AudioClip ShieldSound;

    EnemySpawn enemyspawn;
    //게임 종료 관련
    public AudioSource backgroundMusic;
    public GameObject GameEndPanal;

    public bool isPause = false;
    public GameObject GamePausePanal;
    public GameObject GameWinPanal;
    Score score;
    public Text Endscore;
    public Text Winscore;

    //스킬과 관련된변수
    private float skillContinueTime = 1f;
    public bool OnOffSkill = false;

    private float skillCoolTime = 30f;

    bool isPlayerDead = false;
    public GameObject SkillImage;

    //어빌리티 능력과 관련된 변수
    public bool isShield = false;
    public GameObject CircleShield;
    public AbilityUpGrade upgrade;

    [Header("[기본능력]")]
    public float jumpPower = 15f;
    public int Life = 3;
    public int Damage = 5;
    public float AttackSpeed = 2f;



    void Start()
    {
        upgrade = GameObject.Find("AbilityManage").GetComponent<AbilityUpGrade>();
        enemyspawn = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>();
        score = GameObject.Find("ScoreCounter").GetComponent<Score>();
        life1.GetComponent<Image>().enabled = true;
        life2.GetComponent<Image>().enabled = true;
        life3.GetComponent<Image>().enabled = true;

        p_rigid = GetComponent<Rigidbody2D>();  
        p_Anim = GetComponent<Animator>();
        p_capsule = GetComponent<CapsuleCollider2D>();

        PlayerAudio = GetComponent<AudioSource>();

        StartCoroutine(PlayerDead());

        SkillSlider.value = 30.0f;
        ShieldSlider.value = 5.0f;

        PerringUseTime = 0.1f;
        isPerring = false;
        PerringCoolTime = 3.0f;
        canPerring = true;
        IsJump = false;
        isAttack = false;
        IsSkill = true;
        isShield = false;
        isPause = false;
        GameWinPanal.SetActive(false);
        SkillImage.SetActive(false);
        SkillCheckImage.gameObject.SetActive(false);
        ShieldCheckImage.gameObject.SetActive(false);

    }

    void Update()
    {
        HpCheck();
        if (isPlayerDead) return;

        if (enemyspawn.RoundCount > 20)
        {
            Time.timeScale = 0;
            GameWinPanal.SetActive(true);
            Winscore.text = "최종 Score : " + score.pointScore + "점";
            return;
        }

        if(upgrade.canTimeStop)
        { return; }

        PauseCheck();


        if (Input.GetButtonDown("Jump") && !IsJump && !isPause && !upgrade.canTimeStop)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isPause)
		{
            Attack();
		}

        if (Input.GetKeyDown(KeyCode.Mouse1) && canPerring)
        {
            perringSkill();
        }

        if (Input.GetKeyDown(KeyCode.W))
		{
            AttackSpeed += 0.2f;
            p_Anim.SetFloat("AttackSpeed", AttackSpeed);
        }

        if (Input.GetKeyDown(KeyCode.D) && IsSkill)
        {
            PlayerAudio.PlayOneShot(SkillSound);
            SkillSlider.value = 0f;
            p_rigid.velocity = new Vector2(0, 30);
            OnOffSkill = true;
            IsSkill = false;
        }

        DamageText.transform.position = transform.position + new Vector3(2, 2, 0);

        AttackSpeedCheck();
        JumpCheck();
        CoolTimeCheck();
        SkillUse();
        PerringCoolTimeSet();
        CheckSkill();
        PerringTime();
        ShieldCheck();
    }


    void Jump()
	{
        PlayerAudio.PlayOneShot(JumpSound);
        p_rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        p_Anim.SetBool("IsJump", true);
        IsJump = true;
    }

    void Attack()
	{
        p_Anim.SetTrigger("Attack");
       
	}

    void AttackSpeedCheck()
	{
        p_Anim.SetFloat("AttackSpeed", AttackSpeed);

    }

    void JumpCheck()
	{

        if (p_rigid.velocity.y < 0) 
        {
            Debug.DrawRay(p_rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(p_rigid.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    IsJump = false;
                    p_Anim.SetBool("IsJump", false);
                }
            }
        }
    }


    //때렸는지 판정
    void IsAttackStart()
	{
        PlayerAudio.PlayOneShot(SwordSound);
        isAttack = true;
	}

    void IsAttackEnd()
	{
        isAttack = false;
    }

    //데미지입으면 체력 감소
    public void PlayerDamaged()
	{
        if(isShield)
		{
            isShield = false;
            return;
		}

        if (Life > 0)
        {
            Life -= 1;
        }
	}


    //hp 이미지 관리
    void HpCheck()
	{
        switch(Life)
		{
            case 2:
                life3.GetComponent<Image>().enabled = false;
                break;
            case 1:
                life2.GetComponent<Image>().enabled = false;
                break;
            case 0:
                life1.GetComponent<Image>().enabled = false;
                break;
        }
	}


    public void AddForce()
	{
        p_rigid.AddForce(Vector2.up * 1f, ForceMode2D.Impulse);
    }

    public void CoolTimeCheck()
	{
        if(!SkillCheck())
		{
            SkillSlider.value += Time.deltaTime;
            skillCoolTime -= Time.deltaTime;

            if (skillCoolTime < 0.0f)
			{
                IsSkill = true;
                skillCoolTime = 30f;
			}
		}
	}

    public bool SkillCheck()
	{
        bool _isSkill = IsSkill;
        return _isSkill;
	}

    public void SkillUse()
	{
        if(OnOffSkill)
		{
            SkillImage.SetActive(true);
            skillContinueTime -= Time.deltaTime;
            if(skillContinueTime < 0.0f)
			{
                SkillImage.SetActive(false);
                OnOffSkill = false;
                skillContinueTime = 1f;
			}
		}
	}

    IEnumerator PlayerDead()
	{
        while (true)
        {
            if (Life <= 0 && !isPlayerDead)
            {
                isPlayerDead = true;
                p_Anim.SetTrigger("IsDie");
                GameEndPanal.SetActive(true);
                Endscore.text = "Score : " +score.pointScore + " 점";
                Time.timeScale = 0;
                backgroundMusic.Stop();
                yield return new WaitForSeconds(3);
            }
            yield return new WaitForEndOfFrame();
        }
	}

    public void HitEnemySound()
	{
        PlayerAudio.PlayOneShot(HitSound);
	}

    public void HitShieldSound()
    {
        PlayerAudio.PlayOneShot(ShieldSound);
    }

    public void perringSkill()
	{
        ShieldSlider.value = 0f;
        canPerring = false;
        isPerring = true;
        Debug.Log(isPerring);
    }

    public void PerringCoolTimeSet()
	{
        if(!canPerring)
		{
            ShieldSlider.value += Time.deltaTime;
            PerringCoolTime -= Time.deltaTime;

            if(PerringCoolTime <= 0.0f)
			{
                PerringCoolTime = 3.0f;
                canPerring = true;
			}
		}

	}

    public void CheckSkill()
	{
        if(ShieldSlider.value >= 3.0f)
		{
            ShieldCheckImage.gameObject.SetActive(true);
		}

        else
		{
            ShieldCheckImage.gameObject.SetActive(false);
        }        

        if (SkillSlider.value >= 30.0f)
		{
            SkillCheckImage.gameObject.SetActive(true);
		}

        else
		{
            SkillCheckImage.gameObject.SetActive(false);
        }
	}

    public void LifeUp()
	{
        if(Life < 3)
		{
            Life += 1;
		}

        switch (Life)
        {
            case 3:
                life3.GetComponent<Image>().enabled = true;
                break;
            case 2:
                life2.GetComponent<Image>().enabled = true;
                break;
            case 1:
                life1.GetComponent<Image>().enabled = true;
                break;
        }
    }

    public void PerringTime()
	{
        if (isPerring)
        {
            PerringUseTime -= Time.deltaTime;

            if (PerringUseTime <= 0.0f)
            {
                PerringUseTime = 0.1f;
                isPerring = false;
            }
        }
    }

    public void Damaged(float damage)
    {
        StartCoroutine(OnDamageText(damage));
    }

    public IEnumerator OnDamageText(float damage)
    {
        PlayerCanvas.SetActive(true);
        DamageText.text = damage.ToString();
        DamageText.fontSize = 60;
        for(int i = DamageText.fontSize; i >= 30; i--)
		{
            DamageText.fontSize = i;
            yield return new WaitForFixedUpdate();
		}

        yield return new WaitForSeconds(1f);
        PlayerCanvas.SetActive(false);
    }

    public void ShieldCheck()
	{
        if(isShield)
		{
            CircleShield.SetActive(true);
		}

        else if(!isShield)
		{
            CircleShield.SetActive(false);
		}
	}

    public void PauseCheck()
	{
        if(isPause)
		{
            GamePausePanal.SetActive(true);
            Time.timeScale = 0;
		}

        else
		{
            GamePausePanal.SetActive(false);
            Time.timeScale = 1;
        }
	}

}
