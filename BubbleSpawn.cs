using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BubbleSpawn : MonoBehaviour
{
    public static BubbleSpawn instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private int level = 1;
    [SerializeField] private double baseHP = 1;
    [SerializeField] private double multiplier = 1.2;
    [SerializeField] private double HP { get { return baseHP * Math.Pow(multiplier, level); } }
    [SerializeField] private double bossHP { get { return baseHP * 10 * Math.Pow(multiplier, level); } }

    [Space()]
    [SerializeField] private Animator animator = default;
    [SerializeField] private TextMeshProUGUI bubbleHPText = default;

    [Space()]
    [SerializeField] private Image bossTimerFillImage = default;
    [SerializeField] private GameObject bossTimerImage = default;
    [SerializeField] private TextMeshProUGUI bossTimerText = default;

    private double currentHP;

    private bool bossFight = false;

    private const float bossFightTime = 15f;
    


    private void Start()
    {
        LoadData();
        SetBubbleHP();
        UpdateBubbleUI();
    }    

    private void SetBubbleHP()
    {
        currentHP = HP;
    }

    private void SetBossHP()
    {
        currentHP = bossHP;
    }

    private void UpdateBubbleUI()
    {
        bubbleHPText.text = Score.ScoreToText(currentHP);
    }

    private void LoadData()
    {
        level = 1;
    }

    private void PopBubble()
    {
        animator.SetTrigger("pop");
        level += 1;
        if (level % 5 == 0)
            SpawnBoss();
        else
            RespawnBubble(level);
    }

    private void SpawnBoss()
    {
        animator.SetTrigger("respawn");
        SetBossHP();
        UpdateBubbleUI();

        bossTimerImage.SetActive(true);
        bossFight = true;
        StartCoroutine("BossFight");
    }

    IEnumerator BossFight()
    {
        float t = bossFightTime;
        while(t > 0)
        {
            t -= Time.deltaTime;
            bossTimerFillImage.fillAmount = t / bossFightTime;
            yield return null;
        }
        if(bossFight)
            BossNotDefeated();
    }

    private void BossNotDefeated()
    {
        bossTimerImage.SetActive(false);
        bossFight = false;
        animator.SetTrigger("pop");
        level -= 2;
        RespawnBubble(level);
    }

    private void BossDefeated()
    {
        bossTimerImage.SetActive(false);
        bossFight = false;
        StopCoroutine("BossFight");
        animator.SetTrigger("pop");
        level += 1;
        RespawnBubble(level);
    }

    private void RespawnBubble(int level)
    {
        animator.SetTrigger("respawn");
        SetBubbleHP();
        UpdateBubbleUI();
    }

    public void Damage(double amount)
    {
        //double remainder = currentHP;
        if (bossFight)
        {
            currentHP -= amount;
            if (currentHP <= 0)
            {
                //Balance.addSimpleBubble(remainder);
                currentHP = 0;
                BossDefeated();
                return;
            }
            //Balance.addSimpleBubble(amount);
            animator.SetTrigger("click_tap");
            UpdateBubbleUI();
            return;
        }
        else
        {

            currentHP -= amount;
            if (currentHP <= 0)
            {
                //Balance.addSimpleBubble(remainder);
                currentHP = 0;
                PopBubble();
                return;
            }
            //Balance.addSimpleBubble(amount);
            animator.SetTrigger("click_tap");
            UpdateBubbleUI();
        }
    }
}
