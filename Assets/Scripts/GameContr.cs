using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameContr : MonoBehaviour
{
    public GameObject _orderObj;
    public GameObject _myOrderObj;

    [SerializeField] private TMP_Text _myMoney;
    [SerializeField] private TMP_Text _tipsText;
    [SerializeField] private GameObject _imageAngry;
    [SerializeField] private GameObject _client;

    [SerializeField] private int _limitTips, _probabilityTips;

    [SerializeField] private int _payCola, _paySoda, _payCoffee, _payDesert, _payCoffeePlus, _payDonut;


    private int _range;
    private int _howClients;
    private int _howDays;
    private int _tips;

    private void Start()
    {
        _howClients = PlayerPrefs.GetInt("HowClients");
        _range = PlayerPrefs.GetInt("MyMoney");
        _howClients++;
        DaysCount();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
            _range = 0;

        _myMoney.text = _range + "";

        if (Input.GetKeyDown(KeyCode.R))
            Instantiate(_client);
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("MyMoney", _range);
        PlayerPrefs.SetInt("HowClients", _howClients);
    }

    public void ClientPay()
    {
        if (_orderObj.gameObject.tag == _myOrderObj.gameObject.tag)
        {
            switch (_orderObj.gameObject.tag)
            {
                case "cola":
                    _range += _payCola;
                    break;
                case "soda":
                    _range += _paySoda;
                    break;
                case "coffee":
                    _range += _payCoffee;
                    break;
                case "coffee+":
                    _range += _payCoffeePlus;
                    break;
                case "donut":
                    _range += _payDonut;
                    break;
                case "desert":
                    _range += _payDesert;
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
                
                _range += _tips;

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

    private void DaysCount()
    {

    }
}
