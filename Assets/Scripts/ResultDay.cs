using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultDay : MonoBehaviour
{
    [SerializeField] private MyMoney _myMoney;

    [SerializeField] private TMP_Text _myMoneyText;
    [SerializeField] private TMP_Text _workDays;

    private int _howWorkDays;

    private bool isX2 = true;

    private int _profit, _range;

    private void Start()
    {
        _profit = PlayerPrefs.GetInt("Profit");
        _range = PlayerPrefs.GetInt("MyMoney") - _profit;
        _howWorkDays = PlayerPrefs.GetInt("HowDays");
    }
    public void OnX2Button()
    {
        //Add Reklam Logic

        if(isX2)
            X2();
    }

    private void Update()
    {
        _myMoneyText.text = $"{_profit}";
        _workDays.text = $"{_howWorkDays}";
    }

    private void X2()
    {
        isX2 = false;

        _range += _profit * 2;

        PlayerPrefs.SetInt("MyMoney", _range);
    }
}
