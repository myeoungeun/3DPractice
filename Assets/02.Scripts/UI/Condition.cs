using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue; //현재값
    public float startValue; //시작값
    public float maxValue;
    public float minValue;
    public float passiveValue; //계속 변하는 값(ex : 시간 지날때마다 계속 피 -1씩 깎이는거). PlayerCondition에서 사용함
    public Image uiBar;

    void Start()
    {
        curValue = startValue;
    }

    void Update()
    {
        // ReSharper disable once PossibleLossOfFraction
        GetPercentage();
    }

    float GetPercentage()
    {
        return uiBar.fillAmount = curValue / maxValue;
        //화면상으로는 1로 보이게 함. (실제로는 1보다 더 큰 수가 들어가도 계속 1이라고 뜸)
    }

    public void Plus(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue); //값은 아무리 커져도 최댓값을 넘길 수 없음
    }

    public void Minus(float value)
    {
        curValue = Mathf.Max(curValue - value, minValue); //값은 아무리 작아져도 최소값보다 작아질 수 없음
    }
}