using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    [SerializeField, Header("��������I�u�W�F�N�g")] private GameObject _cube = default;
    [SerializeField, Header("�X�^�[�g�̈ʒu")] private Transform _startPos = default;
    [SerializeField, Header("�S�[���̈ʒu")] private Transform _goalPos = default;

    void Start()
    {
        
        for(int i = 1; _startPos.position.z + i * 5 < _goalPos.position.z; i++)
        {
            Vector3 insPos = new Vector3(_startPos.position.x, _startPos.position.y + _cube.transform.localScale.y, _startPos.position.z + i * 5);
            Instantiate(_cube, insPos, Quaternion.identity);
        }

    }

}
