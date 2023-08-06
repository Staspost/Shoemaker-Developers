using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] 
    private List<GameObject> _roads; 

    [SerializeField] 
    private List<GameObject> _pits;   

    [SerializeField] 
    private float _roadLength = 40;

    private GameObject _road;
    private GameObject _pit;


    
    private int _side;
    private int _selectType;
    private int _type;
    private float _x;

    Vector3 position_pit;


    void Start()
    {
        _road = Instantiate(_roads[Random.Range(0, _roads.Count)], transform.position, Quaternion.identity);
        _pit = Instantiate(_pits[Random.Range(0, _pits.Count)], transform.position, Quaternion.identity);
    }   

    public void Spawn()
    {
        _side = Random.Range(0, 90);
        if (_side < 30) _x = -2.5f;
        else if (_side > 60) _x = 2.5f;
        else _x = 0f;

        _selectType = Random.Range(0, 90);
        if (_selectType < 10) _type = 0; //понижает скорость
        else if (_selectType >= 10 && _selectType < 20) _type = 1; //повышает скорость
        else if (_selectType >= 20 && _selectType < 30) _type = 2; //случайный
        else if (_selectType >= 30 && _selectType < 40) _type = 3; //баги
        else if (_selectType >= 40 && _selectType < 50) _type = 4; //баги
        else if (_selectType >= 50 && _selectType < 60) _type = 5; //баги
        else if (_selectType >= 60 && _selectType < 70) _type = 6; //идея
        else if (_selectType >= 70 && _selectType < 80) _type = 7; //идея
        else if (_selectType >= 80 && _selectType < 90) _type = 8; //идея
        //else _type = 3; //нельзя попадать


        Vector3 position = new Vector3(0, 0, _road.transform.position.z + _roadLength);
        if (_type == 6 || _type == 7 || _type == 8)
        {
            position_pit = new Vector3(_x, 2f, _road.transform.position.z + _roadLength);
        }
        else
        {
            position_pit = new Vector3(_x, 1.1f, _road.transform.position.z + _roadLength);
        }        

        _road = Instantiate(_roads[Random.Range(0, _roads.Count)], position, Quaternion.identity);
        _pit = Instantiate(_pits[_type], position_pit, Quaternion.identity);
        _pit.transform.Rotate(0, 180f, 0, Space.Self);
    }
}
