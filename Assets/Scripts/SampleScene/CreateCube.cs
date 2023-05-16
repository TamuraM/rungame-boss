using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    [SerializeField, Header("��������I�u�W�F�N�g")] private GameObject _cube = default;
    [SerializeField, Header("�X�^�[�g�̈ʒu")] private Transform _startPos = default;
    [SerializeField, Header("�S�[���̈ʒu")] private Transform _goalPos = default;
    [SerializeField] private Transform[] _blockInsPos = new Transform[5];

    void Start()
    {
        int num = (int)(_goalPos.position.z - _startPos.position.z) / 5;
        int blockIndex = 1;
        Vector3 insPos = new Vector3(_blockInsPos[blockIndex].position.x, _startPos.position.y + _cube.transform.localScale.y, _startPos.position.z + 5);
        Instantiate(_cube, insPos, Quaternion.identity, this.transform); //�q�I�u�W�F�N�g�ɂ���

        for (int i = 2; i < num; i++)
        {
            blockIndex += Random.Range(0, 2) == 0 ? -1 : 1;
            if (blockIndex < 0) blockIndex = 1;
            if(blockIndex > _blockInsPos.Length - 1) blockIndex = _blockInsPos.Length - 2;
            insPos = new Vector3(_blockInsPos[blockIndex].position.x, _startPos.position.y + _cube.transform.localScale.y, _startPos.position.z + i * 5);
            Instantiate(_cube, insPos, Quaternion.identity, this.transform); //�q�I�u�W�F�N�g�ɂ���
        }

        //�q�I�u�W�F�N�g�ɂ��郁���b�g:�e�I�u�W�F�N�g(����͂��̃I�u�W�F�N�g)�������΂��ׂď�����
    }

}
