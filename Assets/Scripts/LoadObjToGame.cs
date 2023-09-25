using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObjToGame : MonoBehaviour
{

    [SerializeField] private Material[] _materials;

    [SerializeField] private List<GameObject> _chairs, _tables;

    [SerializeField] private GameObject _walls, _ceiling;

    private void Update()
    {
        int numColor = PlayerPrefs.GetInt("ColorWalls");
        int numChairs = PlayerPrefs.GetInt("Chairs");
        int numTables = PlayerPrefs.GetInt("Tables");

        Debug.Log(numChairs);
        NewColor(_materials[numColor]);
        NewCustomObj(_chairs, numChairs);
        NewCustomObj(_tables, numTables);
    }

    private void NewColor(Material material)
    {
        _walls.GetComponent<MeshRenderer>().material = material;
        _ceiling.GetComponent<MeshRenderer>().material = material;
    }

    private void NewCustomObj(List<GameObject> _obj, int num)
    {
        for (int i = 0; i < _obj.Count; i++)
            _obj[i].SetActive(false);

        _obj[num].SetActive(true);
    }
}
