using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomOrder : MonoBehaviour
{
    public bool isOrder = false;
    public bool isDone = false;

    [SerializeField] private GameContr _gameContr;

    [SerializeField] private GameObject _orderUI;
    [SerializeField] private List<GameObject> _orders;
    [SerializeField] private List<GameObject> _ordersObjects;
    [SerializeField] private GameObject _client;
    [SerializeField] private GameObject _colaOrder, _sodaOrder, _donutOrder, _desertOrder;
    [SerializeField] private GameObject _cola, _soda, _donut, _desert;


    private int _orderNum;
    public bool isGame = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("cola") == 1)
            AddObjectsToList(_colaOrder, _cola);
        if (PlayerPrefs.GetInt("soda") == 1)
            AddObjectsToList(_sodaOrder, _soda);
        if (PlayerPrefs.GetInt("donut") == 1)
            AddObjectsToList(_donutOrder, _donut);
        if (PlayerPrefs.GetInt("desert") == 1)
            AddObjectsToList(_desertOrder, _desert);

        for (int i = 0; i < _ordersObjects.Count; i++)
            _ordersObjects[i].SetActive(true);
    }

    private void AddObjectsToList(GameObject objectOrder, GameObject objectAnBar)
    {
        _orders.Add(objectOrder);
        _ordersObjects.Add(objectAnBar);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
            SceneManager.LoadScene("GameScene");

        if (!isOrder)
        {
            _orderUI.SetActive(false);
            _orders[_orderNum].SetActive(false);
        }

        if (isGame)
            CreateClients();
    }

    public void CreateOrder()
    {
        _orderNum = RandomNum(0, _orders.Count);

        _orders[_orderNum].SetActive(true);
        _orderUI.SetActive(true);

        _gameContr._orderObj = _orders[_orderNum];

        isOrder = true;
    }

    private int RandomNum(int an, int at)
    {
        return Random.Range(an, at);
    }

    private void CreateClients()
    {
        isGame = false;
        GameObject _createdObject = Instantiate(_client);
        _createdObject.SetActive(true);
    }
}
