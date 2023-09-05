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
    [SerializeField] private GameObject[] _orders = new GameObject[5];
    [SerializeField] private GameObject _client;

    [SerializeField] private float _intervals;


    private int _orderNum;
    private bool isGame = true;

    private void Start()
    {
        isGame = true;
        StartCoroutine(CreateClients());
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
    }

    public void CreateOrder()
    {
        _orderNum = RandomNum(0, _orders.Length);

        _orders[_orderNum].SetActive(true);
        _orderUI.SetActive(true);

        _gameContr._orderObj = _orders[_orderNum];

        isOrder = true;
    }

    private int RandomNum(int an, int at)
    {
        return Random.Range(an, at);
    }

    private IEnumerator CreateClients()
    {
        while (isGame)
        {
            yield return new WaitForSeconds(_intervals);

            GameObject _createdObject = Instantiate(_client);
            _createdObject.SetActive(true);
        }
    }
}
