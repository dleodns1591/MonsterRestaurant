using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface I_CustomerType
{
    /// <summary>
    /// 주문 들어오면 실행되는 함수(손님 대사 & 버튼 내용 등등...)
    /// </summary>
    void SpecialType();

    string SpecialAnswer();
}
