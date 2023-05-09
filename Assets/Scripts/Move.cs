using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField, Header("移動スピード")] private float _speed = 5f;
    [SerializeField] private Rigidbody _rigidbody;
    private bool _isStop = false;
    [SerializeField, Header("ゴールのオブジェクト")] private Transform _goalObj;
    private Vector3 _forward;

    private void Start()
    {
        _forward = transform.forward;
        _rigidbody.velocity = _forward * _speed;
    }

    void Update()
    {

        if (_isStop) return;

        if (gameObject.transform.position.z > _goalObj.position.z)
        {
            _rigidbody.velocity = Vector3.zero;
            transform.position = new Vector3(_goalObj.position.x, transform.position.y, _goalObj.position.z);
            _isStop = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        //キューブに当たったら消す
        if(other.gameObject.tag == "Target")
        {
            Destroy(other.gameObject);
        }

    }

}
