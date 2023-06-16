using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    public SkillData skill;
    public PlayerAttacker player;
    public Image imgIcon;
    public Image imgCool;

    void Start()
    {
        // SO Skill �� ����� ��ų ������ ����
        imgIcon.sprite = skill.Skills[0].icon;

        // Cool �̹��� �ʱ� ����
        imgCool.fillAmount = 0;
    }

    public void UsedSkill(SkillData skills)
    {
        // Cool �̹����� fillAmount �� 0 ���� ũ�ٴ� ����
        // ���� ��Ÿ���� ������ �ʾҴٴ� ��
        if (imgCool.fillAmount > 0) return;

        // ��ų Cool ó��
        StartCoroutine(SC_Cool());
    }

    IEnumerator SC_Cool()
    {
        // skill.cool ���� ���� �޶���
        // ��: skill.cool �� 10�� ���
        // tick = 0.1
        float tick = 1f / skill.Skills[0].cool;
        float t = 0;

        imgCool.fillAmount = 1;

        // 10�ʿ� ���� 1 -> 0 ���� �����ϴ� ����
        // imgCool.fillAmout �� �־��ִ� �ڵ�
        while (imgCool.fillAmount > 0)
        {
            imgCool.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);

            yield return null;
        }
    }
}
