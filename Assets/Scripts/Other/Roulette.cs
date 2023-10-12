using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Globalization;

public class Roulette : MonoBehaviour
{
    [SerializeField] private GameObject _roulette, _buttoToGame;
    [SerializeField] private GameObject _wonMoney, _won, _winCustom;

    [SerializeField] private TMP_Text _howWonMoney, _timeToNextRoulette;

    [SerializeField] private List<string> _customObjName = new List<string>();
    [SerializeField] private List<GameObject> _winCustomObj;

    [SerializeField] private int _minPower, _maxPower, _plus;
    [SerializeField] private float _speed, _smooth, _wait;

    private AudioSource _rouletteSound;

    private int _randomWinObj;
    private bool isSkins;
    private bool isRoulette, isRot;
    private float hoursPassed, minutPassed, secondPassed;

    private void Awake()
    {
        _rouletteSound = gameObject.GetComponent<AudioSource>();
        _won.SetActive(false);
    }
    private void Update()
    {
        Time.timeScale = 0;

        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("GameScene");

        if (PlayerPrefs.HasKey("LastTime"))
        {
            string lastTime = PlayerPrefs.GetString("LastTime");
            TimeSpan timePassed = DateTime.UtcNow - DateTime.ParseExact(lastTime, "u", CultureInfo.InvariantCulture);
            hoursPassed = (int)timePassed.TotalHours;
            minutPassed = timePassed.Minutes;
            secondPassed = timePassed.Seconds;

            if (hoursPassed >= 24)
            {
                isRoulette = true;
                _timeToNextRoulette.text = "";
            }
            else
            {
                _timeToNextRoulette.text = $"{23 - hoursPassed}.{60 - minutPassed}.{60 - secondPassed}";
                isRoulette = false;
            }
        }
        else
            isRoulette = true;
    }
    private void OnMouseDown()
    {
        if (isRoulette && !isRot)
        {
            isRot = true;
            isRoulette = false;
            _rouletteSound.Play();
            _buttoToGame.SetActive(false);
            StartCoroutine(Rotate());
        }
    }
    private IEnumerator Rotate()
    {
        float lastSpeed = _speed;

        int rotCount = UnityEngine.Random.Range(_minPower, _maxPower);

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

        Debug.Log(roundWin);

        if (roundWin >= 180 && roundWin < 220)
            WinMoney(5);
        else if (roundWin >= 220 && roundWin < 260)
            WinCustom();
        else if (roundWin >= 260 && roundWin < 300)
            WinMoney(75);
        else if (roundWin >= 300 && roundWin < 340)
            WinMoney(15);
        else if (roundWin >= 340 || roundWin < 20)
            WinCustom();
        else if (roundWin >= 20 && roundWin < 60)
            WinMoney(45);
        else if (roundWin >= 60 && roundWin < 100)
            WinMoney(10);
        else if (roundWin >= 100 && roundWin < 140)
            WinCustom();
        else if (roundWin >= 140 && roundWin <= 180)
            WinMoney(100);

        _speed = lastSpeed;
    }


    private void WinMoney(int howWinMoney)
    {
        int myMoney = PlayerPrefs.GetInt("MyMoney");
        PlayerPrefs.SetInt("MyMoney", myMoney += howWinMoney);

        _howWonMoney.text = $"+{howWinMoney}";

        _wonMoney.SetActive(true);
        _won.SetActive(true);
    }

    private void WinCustom()
    {
        _randomWinObj = UnityEngine.Random.Range(0, _customObjName.Count);

        isSkins = false;

        for (int y = 0; y < _customObjName.Count; y++)
            _winCustomObj[y].SetActive(false);

        for (int i = 0; i < _customObjName.Count; i++)
            if (PlayerPrefs.GetInt(_customObjName[i]) == 0)
                isSkins = true;

        if (isSkins)
        {
            while (PlayerPrefs.GetInt(_customObjName[_randomWinObj]) == 1)
            {
                _randomWinObj = UnityEngine.Random.Range(0, _customObjName.Count);
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

    public void ContinueGame(bool isWin)
    {
        _roulette.SetActive(false);
        _won.SetActive(false);
        _winCustom.SetActive(false);
        _wonMoney.SetActive(false);

        Time.timeScale = 1;

        if (isWin)
        {
            isRot = false;
            _buttoToGame.SetActive(true);
            PlayerPrefs.SetString("LastTime", DateTime.UtcNow.ToString("u", CultureInfo.InvariantCulture));
        }
    }
}
