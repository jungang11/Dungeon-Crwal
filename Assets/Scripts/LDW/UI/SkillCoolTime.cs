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
        // SO Skill 에 등록한 스킬 아이콘 연결
        imgIcon.sprite = skill.Skills[0].icon;

        // Cool 이미지 초기 설정
        imgCool.fillAmount = 0;
    }

    public void UsedSkill(SkillData skills)
    {
        // Cool 이미지의 fillAmount 가 0 보다 크다는 것은
        // 아직 쿨타임이 끝나지 않았다는 뜻
        if (imgCool.fillAmount > 0) return;

        // 스킬 Cool 처리
        StartCoroutine(SC_Cool());
    }

    IEnumerator SC_Cool()
    {
        // skill.cool 값에 따라 달라짐
        // 예: skill.cool 이 10초 라면
        // tick = 0.1
        float tick = 1f / skill.Skills[0].cool;
        float t = 0;

        imgCool.fillAmount = 1;

        // 10초에 걸쳐 1 -> 0 으로 변경하는 값을
        // imgCool.fillAmout 에 넣어주는 코드
        while (imgCool.fillAmount > 0)
        {
            imgCool.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);

            yield return null;
        }
    }
}
