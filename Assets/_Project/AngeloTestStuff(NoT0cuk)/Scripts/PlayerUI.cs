using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private FloatObject money;
    [SerializeField] private TMP_Text moneyTextUI;
    [SerializeField] private TMP_Text shopMoneyText;

    private void Start()
    {
      UpdateMoney();
    }

    public void UpdateMoney()
    {
      moneyTextUI.text = money.value.ToString();
      shopMoneyText.text = money.value.ToString();
    }
}
