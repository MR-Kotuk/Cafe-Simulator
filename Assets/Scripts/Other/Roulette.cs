using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Roulette : MonoBehaviour
{
    [SerializeField] private GameObject _roulette, _client;
    [SerializeField] private GameObject _wonMoney, _won, _winCustom;

    [SerializeField] private TMP_Text _howWonMoney;

    [SerializeField] private List<string> _customObjName = new List<string>();
    [SerializeField] private List<GameObject> _winCustomObj;

    [SerializeField] private int _minPower, _maxPower, _plus;
    [SerializeField] private float _speed, _smooth, _wait;

    private AudioSource _rouletteSound;
    private CircleCollider2D _colider;

    private int _randomWinObj;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("GameScene");
    }
    private void Start()
    {
        _won.SetActive(false);
        _colider = gameObject.GetComponent<CircleCollider2D>();
        _rouletteSound = gameObject.GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        _rouletteSound.Play();
        _colider.enabled = false;
        StartCoroutine(Rotate());
    }


    private IEnumerator Rotate()
    {
        int rotCount = Random.Range(_minPower, _maxPower);

        for (int i = 0; i <= rotCount; i++)
        {
            yield return new WaitForSeconds(_wait);
            transform.Rotate(new Vector3(0, 0, -_smooth) * _speed * Time.fixedDeltaTime);

            if (i < rotCount / 6)
                _speed += _plus;
            else if (i >= rotCount / 3)
                _speed -= _speed / (rotCount - i);
        }

        _rouletteSound.Stop();

        int roundWin = Mathf.RoundToInt(transform.eulerAngles.z);

        if (roundWin >= 0 && roundWin < 40)
            WinMoney(5);
        else if (roundWin >= 40 && roundWin < 80)
            WinCustom();
        else if (roundWin >= 80 && roundWin < 120)
            WinMoney(75);
        else if (roundWin >= 120 && roundWin < 160)
            WinMoney(15);
        else if (roundWin >= 160 && roundWin < 200)
            WinCustom();
        else if (roundWin >= 200 && roundWin < 240)
            WinMoney(45);
        else if (roundWin >= 240 && roundWin < 280)
            WinMoney(10);
        else if (roundWin >= 280 && roundWin < 320)
            WinCustom();
        else if (roundWin >= 320 && roundWin <= 360)
            WinMoney(100);
    }


    private void WinMoney(int howWinMoney)
    {
        int myMoney = PlayerPrefs.GetInt("MyMoney");
        PlayerPrefs.SetInt("MyMoney", myMoney += howWinMoney);

        _howWonMoney.text = $"+{howWinMoney}";

        _wonMoney.SetActive(true);
        _won.SetActive(true);
    }
    bool isSkins;

    private void WinCustom()
    {
        _randomWinObj = Random.Range(0, _customObjName.Count);

        isSkins = false;

        for (int i = 0; i < _customObjName.Count; i++)
            if (PlayerPrefs.GetInt(_customObjName[i]) == 0)
                isSkins = true;

        if (isSkins)
        {
            while (PlayerPrefs.GetInt(_customObjName[_randomWinObj]) == 1)
            {
                _randomWinObj = Random.Range(0, _customObjName.Count);
            }
        }
        else
            WinMoney(300);
        
        
        if(PlayerPrefs.GetInt(_customObjName[_randomWinObj]) == 0 && isSkins)
        {
            _winCustom.SetActive(true);
            _winCustomObj[_randomWinObj].SetActive(true);
            _won.SetActive(true);

            PlayerPrefs.SetInt(_customObjName[_randomWinObj], 1);
        }
        
    }

    public void ContinueGame()
    {
        _roulette.SetActive(false);
        _client.SetActive(true);
    }
}
