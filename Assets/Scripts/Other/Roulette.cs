using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Roulette : MonoBehaviour
{
    [SerializeField] private GameObject _roulette;
    [SerializeField] private int _minPower, _maxPower, _plus;
    [SerializeField] private float _speed, _smooth, _wait;
    private AudioSource _rouletteSound;

    protected bool isSpinEnd;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("GameScene");
    }
    private void Start()
    {
        isSpinEnd = false;
        _rouletteSound = gameObject.GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        _rouletteSound.Play();
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

        isSpinEnd = true;
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
        else if (roundWin >= 320 && roundWin < 360)
            WinMoney(10);
    }


    private void WinMoney(int howWinMoney)
    {
        Debug.Log("You win:" + howWinMoney);
    }

    private void WinCustom()
    {
        Debug.Log("You win custom obj");
    }
}
