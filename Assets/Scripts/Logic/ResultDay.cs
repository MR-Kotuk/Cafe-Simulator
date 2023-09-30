﻿using UnityEngine;
using TMPro;

public class ResultDay : MonoBehaviour
{
    [SerializeField] private TMP_Text _myMoneyText;
    [SerializeField] private TMP_Text _workDays;

    private bool isX2;

    private int _howWorkDays;
    private int _profit, _range;

    private void Start()
    {
        isX2 = true;

        _profit = PlayerPrefs.GetInt("Profit");
        _range = PlayerPrefs.GetInt("MyMoney");
        _howWorkDays = PlayerPrefs.GetInt("HowDays");

        PlayerPrefs.SetInt("MyMoney", _range + _profit);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        _myMoneyText.text = $"+ {_profit}";
        _workDays.text = $"День: {_howWorkDays}";
    }

    public void OnX2Button()
    {
        //Add Reklam Logic

        if(isX2)
            X2();
    }

    private void X2()
    {
        isX2 = false;

        _profit *= 2;

        PlayerPrefs.SetInt("MyMoney", _range + _profit);
        PlayerPrefs.Save();
    }
}