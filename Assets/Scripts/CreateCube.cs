using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    [SerializeField, Header("生成するオブジェクト")] private GameObject _cube = default;
    [SerializeField, Header("スタートの位置")] private Transform _startPos = default;
    [SerializeField, Header("ゴールの位置")] private Transform _goalPos = default;

    void Start()
    {
        
        for(int i = 1; _startPos.position.z + i * 5 < _goalPos.position.z; i++)
        {
            Vector3 insPos = new Vector3(_startPos.position.x, _startPos.position.y + _cube.transform.localScale.y, _startPos.position.z + i * 5);
            Instantiate(_cube, insPos, Quaternion.identity);
        }

    }

}
