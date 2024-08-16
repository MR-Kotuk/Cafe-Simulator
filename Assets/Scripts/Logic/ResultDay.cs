using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using YG;

public class ResultDay : MonoBehaviour
{
    [SerializeField] private List<string> _objName;

    [SerializeField] private Text _myMoneyText;
    [SerializeField] private Text _workDays;

    private bool isX2;

    private int _howWorkDays;
    private int _profit, _range;


    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    private void Start()
    {
        YandexGame.ReviewShow(true);

        isX2 = true;

        _profit = PlayerPrefs.GetInt("Profit");
        _range = PlayerPrefs.GetInt("MyMoney");
        _howWorkDays = PlayerPrefs.GetInt("HowDays");

        PlayerPrefs.SetInt("MyMoney", _range + _profit);
        PlayerPrefs.SetInt("HowDays", _howWorkDays += 1);
        PlayerPrefs.Save();

        _howWorkDays--;

        for (int i = 0; i < _objName.Count; i++)
            if (PlayerPrefs.GetInt(_objName[i]) > 0)
                PlayerPrefs.SetInt(_objName[i], PlayerPrefs.GetInt(_objName[i]) - 1);
    }

    private void Update()
    {
        _myMoneyText.text = $"+ {_profit}";
        _workDays.text = $"День: {_howWorkDays}";
    }

    public void OnX2Button()
    {
        if (isX2)
        {
            isX2 = false;
            YandexGame.RewVideoShow(1000);
        }
    }

    private void Rewarded(int id)
    {
        if(id == 1000)
        {
            PlayerPrefs.SetInt("MyMoney", _range + (_profit *= 2));
            PlayerPrefs.Save();
        }
    }
}
