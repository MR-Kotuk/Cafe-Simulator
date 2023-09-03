using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClient : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private Vector3[] _randomPos = new Vector3[3];

    [SerializeField] private GameObject[] _skins = new GameObject[6];

    [SerializeField] private GameContr _gameContr;
    [SerializeField] private RandomOrder _randomOrder;

    [SerializeField] private Transform _bar;
    [SerializeField] private Transform _door;

    [SerializeField] private int _speed;
    [SerializeField] private float _distToBar, _distToDoor, _exitDistToDoor;

    private bool isExit;
    private bool isMove, isMoveToDoor;
    private Vector3 _exitPosMove;

    private void Awake()
    {
        RandomSkine();

        _exitPosMove = _randomPos[RandomNum(1, _randomPos.Length)];
        transform.position = _randomPos[RandomNum(0, _randomPos.Length)];
        isMoveToDoor = true;
        isMove = true;
        isExit = false;

        _anim.SetBool("isMove", true);
        _anim.SetBool("isReturn", false);
    }

    private void Update()
    {
        if (_randomOrder.isDone)
        {
            _anim.SetBool("isUp", true);
            Invoke("ExitAtCafe", 2f);
        }

        if (isMove)
            MoveControl();
    }

    private void MoveControl()
    {
        float dist = DistTo(_door.position);
        float distBar = DistTo(_bar.position);
        float distExitPos = DistTo(_exitPosMove);

        if (dist <= 1.3f && !isExit || dist <= _exitDistToDoor && isExit)
            isMoveToDoor = false;

        if (isMoveToDoor)
            Move(_door.position);
        else if (!isMoveToDoor && isExit)
            Move(_exitPosMove);
        else if (!isMoveToDoor && !isExit)
            Move(_bar.position);

        if (distExitPos <= 0.5f && isExit)
        {
            isExit = false;
            gameObject.SetActive(false);
            _gameContr.EndGame();
        }      

        if (distBar <= _distToBar && !isExit)
        {
            isMove = false;

            _anim.SetBool("isWait", RandomNum());
            _anim.SetBool("isMove", false);

            transform.eulerAngles = new Vector3(0, 0, 0);

            _randomOrder.CreateOrder();
        }
    }

    private void Move(Vector3 currentTo)
    {
        transform.LookAt(currentTo);
        transform.Translate(new Vector3(0, 0, 1) * _speed * Time.deltaTime);
    }

    private void RandomSkine()
    {
        for (int i = 0; i <= _skins.Length - 1; i++)
            _skins[i].SetActive(false);

        _skins[RandomNum(0, _skins.Length)].SetActive(true);
    }

    private void ExitAtCafe()
    {
        _anim.SetBool("isUp", false);
        _anim.SetBool("isMove", true);

        isExit = true;
        isMoveToDoor = true;
        isMove = true;
        _randomOrder.isDone = false;
    }

    private bool RandomNum()
    {
        int ranNum = Random.Range(0, 2);

        if (ranNum == 0)
            return false;
        else
            return true;
    }
    private int RandomNum(int an, int at)
    {
        return Random.Range(an, at);
    }

    private float DistTo(Vector3 obj)
    {
        return Vector3.Distance(transform.position, obj);

    }
}
