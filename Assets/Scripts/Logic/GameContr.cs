using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameContr : MonoBehaviour
{
    public GameObject OrderObj;
    public GameObject MyOrderObj;

    [SerializeField] private int _limitTips;
    [SerializeField] private int _probabilityTips;

    [SerializeField] private GameObject _imageAngry;

    [SerializeField] private Text _myMoneyText;
    [SerializeField] private Text _tipsText;
    [SerializeField] private Text _workDays;

    [SerializeField] private Payments _payments;
    [SerializeField] private RandomOrder _randomOrder;
    [SerializeField] private SoundControl _soundControl;

    private int _range;
    private int _profit;
    private int _howDays;
    private int _tips;

    private void Start()
    {
        _howDays = PlayerPrefs.GetInt("HowDays");
        _profit = 0;

        WorkTime();
    }

    private void Update()
    {
        _range = PlayerPrefs.GetInt("MyMoney");

        _myMoneyText.text = $"{_range + _profit}";
        _workDays.text = _howDays + "";
    }

    private void EndGame()
    {
        SavePrefs();
        SceneManager.LoadScene("EndWorkDayScene");
    }

    public void ClientPay()
    {
        if (_randomOrder.isTimes)
        {
            if (OrderObj.gameObject.tag == MyOrderObj.gameObject.tag)
            {
                _soundControl.GameSFX("GetMoney");

                switch (OrderObj.gameObject.tag)
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
                        _soundControl.GameSFX("GetTips");

                        _tipsText.text = "+ " + _tips + " Чаевые";
                        Invoke("Tips", 3f);
                    }

                    _profit += _tips;
                }
                SavePrefs();
            }
            else
            {
                _imageAngry.SetActive(true);
                Invoke("AngryImage", 2f);
            }
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
                Invoke("EndGame", 120f);
                break;
            case < 15:
                Invoke("EndGame", 180f);
                break;
            case < 30:
                Invoke("EndGame", 240f);
                break;
            case > 30:
                Invoke("EndGame", 300f);
                break;
        }
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt("MyMoney", _range);
        PlayerPrefs.SetInt("Profit", _profit);

        PlayerPrefs.Save();
    }
}
