using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomOrder : MonoBehaviour
{
    public bool isOrder = false;
    public bool isDone = false;
    public bool isGame = false;
    public bool isTimes;

    [SerializeField] private GameContr _gameContr;

    [SerializeField] private Image _timeBar;

    [SerializeField] private List<GameObject> _orders;
    [SerializeField] private List<GameObject> _ordersObjects;

    [SerializeField] private GameObject _orderUI;
    [SerializeField] private GameObject _client;

    [SerializeField] private GameObject _colaOrder, _sodaOrder, _donutOrder, _desertOrder;
    [SerializeField] private GameObject _cola, _soda, _donut, _desert;

    private int _orderNum;
    public float time;
    private readonly float _maxTime = 100;

    private void Start()
    {
        time = _maxTime;
        isTimes = true;

        AddObjectsToList(_colaOrder, _cola, "cola");
        AddObjectsToList(_sodaOrder, _soda, "soda");
        AddObjectsToList(_donutOrder, _donut, "donut");
        AddObjectsToList(_desertOrder, _desert, "desert");

        for (int i = 0; i < _ordersObjects.Count; i++)
            _ordersObjects[i].SetActive(true);
    }

    private void Update()
    {

        if (isGame)
            CreateClients();

        if (Input.GetKeyDown(KeyCode.RightShift))
            SceneManager.LoadScene("GameScene");

        if (!isOrder)
        {
            _orderUI.SetActive(false);
            _orders[_orderNum].SetActive(false);
        }
    }
    public void CreateOrder()
    {
        isTimes = true;
        StartCoroutine(TimeMake());

        _orderNum = RandomNum(0, _orders.Count);

        _orders[_orderNum].SetActive(true);
        _orderUI.SetActive(true);

        _gameContr.OrderObj = _orders[_orderNum];

        isOrder = true;
    }

    private void AddObjectsToList(GameObject objectOrder, GameObject objectAnBar, string name)
    {
        if (PlayerPrefs.GetInt(name) == 1)
        {
            _orders.Add(objectOrder);
            _ordersObjects.Add(objectAnBar);
        }
    }

    private void CreateClients()
    {
        isGame = false;
        GameObject _createdObject = Instantiate(_client);
        _createdObject.SetActive(true);
    }

    private IEnumerator TimeMake()
    {
        while (isTimes)
        {
            if (time >= 0)
            {
                yield return new WaitForSeconds(0.2f);
                time -= 0.4f;
                _timeBar.fillAmount = time / _maxTime;
            }
            else
                isTimes = false;
        }
    }

    private int RandomNum(int an, int at)
    {
        return Random.Range(an, at);
    }
}
