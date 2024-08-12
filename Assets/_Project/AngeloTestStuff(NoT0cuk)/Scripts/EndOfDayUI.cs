using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class EndOfDayUI : MonoBehaviour
{
    [SerializeField] private BoolObject gamePause, pause;
    [SerializeField] private IntObject day, servedCustomers, lostCustomers;
    [SerializeField] private FloatObject moneySpent, moneyEarned;
    [SerializeField] private UnityEvent saveGame;

    [Header("UI Elements")]
    [SerializeField] private Image backPanel;
    [SerializeField] private GameObject customersServedObj, customersLostObj, backPanelObj, backPaper, moneySpentObj, moneyEarnedObj, totalProfitObj, nextDayButton;
    [SerializeField] private TMP_Text dayText, customersServedText, customersLostText, moneySpentText, moneyEarnedText, totalProfitText;

    private void Update() // will remove later
    {
      if(Input.GetKeyDown(KeyCode.K))
      {
        StartEndOfDayDisplay();
      }
    }

    public void StartEndOfDayDisplay()
    {
      gamePause.SetTrue();
      pause.SetTrue();
      SetUIValues();
      StartCoroutine(DisplayUI());
    }

    private void SetUIValues()
    {
      dayText.text = day.value.ToString();
      if(moneySpent.value > 0)
      {
        moneySpentText.text = "$-" + moneySpent.value;
        moneySpentText.color = new Color(0.76f, 0.22f, 0.22f, 1);
      } else {
        moneySpentText.text = "$0";
        moneySpentText.color = new Color(1, 1, 1, 1);
      }
      if(moneyEarned.value > 0)
      {
        moneyEarnedText.text = "$" + moneyEarned.value;
        moneyEarnedText.color = new Color(0.16f, 0.57f, 0.23f, 1);
      } else {
        moneyEarnedText.text = "$0";
        moneyEarnedText.color = new Color(1, 1, 1, 1);
      }
      totalProfitText.text = "$" + (moneyEarned.value - moneySpent.value);
      if(moneyEarned.value - moneySpent.value > 0)
      {
        totalProfitText.color = new Color(0.16f, 0.57f, 0.23f, 1);
      } else if(moneyEarned.value - moneySpent.value < 0)
      {
        totalProfitText.color = new Color(0.76f, 0.22f, 0.22f, 1);
      } else {
        totalProfitText.color = new Color(1, 1, 1, 1);
      }
      customersServedText.text = servedCustomers.value.ToString();
      customersLostText.text = lostCustomers.value.ToString();

      ResetValues();
    }

    public void ResetValues()
    {
      moneySpent.SetValue(0);
      moneyEarned.SetValue(0);
      servedCustomers.SetValue(0);
      lostCustomers.SetValue(0);
    }

    private IEnumerator DisplayUI()
    {
      backPanelObj.SetActive(true);
      for(float a = 0; a < 100; a++)
      {
        backPanel.color = new Color(0,0,0, 0 + (a/143));
        yield return new WaitForSeconds(0.02f);
      }
      for(float a = 0; a < 200; a++)
      {
        backPanel.color = new Color(0,0,0, 0.7f + (a/660));
        yield return new WaitForSeconds(0.01f);
      }

      yield return new WaitForSeconds(0.5f);
      backPaper.SetActive(true);

      yield return new WaitForSeconds(0.5f);
      customersServedObj.SetActive(true);
      yield return new WaitForSeconds(0.7f);
      customersLostObj.SetActive(true);
      yield return new WaitForSeconds(0.7f);
      moneySpentObj.SetActive(true);
      yield return new WaitForSeconds(0.7f);
      moneyEarnedObj.SetActive(true);
      yield return new WaitForSeconds(0.7f);
      totalProfitObj.SetActive(true);
      
      saveGame.Invoke();

      yield return new WaitForSeconds(1f);
      nextDayButton.SetActive(true);
    }

    public void CloseEndOfDayUI()
    {
      backPanel.color = new Color(0, 0, 0, 0);
      backPanelObj.SetActive(false);
      backPaper.SetActive(false);
      customersServedObj.SetActive(false);
      customersLostObj.SetActive(false);
      moneySpentObj.SetActive(false);
      moneyEarnedObj.SetActive(false);
      totalProfitObj.SetActive(false);
      nextDayButton.SetActive(false);
      gamePause.SetFalse();
      pause.SetFalse();
    }
}
