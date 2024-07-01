using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private FloatObject money;
    [SerializeField] private TMP_Text moneyTextUI;

    private void Awake()
    {
      money.value = 0f;
    }

    public void UpdateMoney()
    {
      moneyTextUI.text = money.value.ToString();
    }
}
