using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameContr : MonoBehaviour
{
    public GameObject _orderObj;
    public GameObject _myOrderObj;

    [SerializeField] private TMP_Text _myMoneyText;
    [SerializeField] private TMP_Text _tipsText;
    [SerializeField] private TMP_Text _workDays;

    [SerializeField] private GameObject _imageAngry;

    [SerializeField] private int _limitTips, _probabilityTips;

    [SerializeField] private Payments _payments;

    private int _range;
    private int _profit;
    private int _howDays;
    private int _tips;

    private void Start()
    {
        _range = PlayerPrefs.GetInt("MyMoney");
        _howDays = PlayerPrefs.GetInt("HowDays");

        _profit = 0;
        _howDays++;

        WorkTime();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.SetInt("MyMoney", 0);
            PlayerPrefs.SetInt("HowDays", 0);
            PlayerPrefs.SetInt("Profit", 0);

            _howDays = 1;
            _range = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightAlt))
            EndGame();

        _myMoneyText.text = $"{_range + _profit}";
        _workDays.text = _howDays + "";
    }

    private void EndGame()
    {
        PlayerPrefs.SetInt("MyMoney", _range);
        PlayerPrefs.SetInt("HowDays", _howDays);
        PlayerPrefs.SetInt("Profit", _profit);
        PlayerPrefs.Save();

        SceneManager.LoadScene("EndWorkDayScene");
    }

    public void ClientPay()
    {
        if (_orderObj.gameObject.tag == _myOrderObj.gameObject.tag)
        {
            switch (_orderObj.gameObject.tag)
            {
                case "cola":
                    _profit += _payments._payCola;
                    break;
                case "soda":
                    _profit += _payments._paySoda;
                    break;
                case "coffee":
                    _profit += _payments._payCoffee;
                    break;
                case "coffee+":
                    _profit += _payments._payCoffeePlus;
                    break;
                case "donut":
                    _profit += _payments._payDonut;
                    break;
                case "desert":
                    _profit += _payments._payDesert;
                    break;
            }
            if (Random.Range(0, _probabilityTips) == 0)
            {
                _tips = Random.Range(5, _limitTips);

                if (_tips > 0)
                {
                    _tipsText.text = "+ " + _tips + " is Tips";
                    Invoke("Tips", 3f);
                }
                
                _profit += _tips;
            }
        }
        else
        {
            _imageAngry.SetActive(true);
            Invoke("AngryImage", 2f);
        }
    }

    private void AngryImage()
    {
        _imageAngry.SetActive(false);
    }
    private void Tips()
    {
        _tipsText.text = " ";
    }

    private void WorkTime()
    {
        switch (_howDays)
        {
            case < 5:
                StartCoroutine(TimeToEnd(150f));
                break;
            case < 15:
                StartCoroutine(TimeToEnd(300f));
                break;
            case < 30:
                StartCoroutine(TimeToEnd(600f));
                break;
            case > 30:
                StartCoroutine(TimeToEnd(1200f));
                break;
        }
    }

    private IEnumerator TimeToEnd(float wait)
    {
        yield return new WaitForSeconds(wait);
        EndGame();
    }
}
