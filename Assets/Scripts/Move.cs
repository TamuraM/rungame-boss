using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField, Header("移動スピード")] private float _speed = 5f;
    [SerializeField] private Rigidbody _rigidbody;
    private bool _isStop = false;
    [SerializeField, Header("ゴールの位置")] private Transform _goalObj;
    private Vector3 _forward;
    private InputAction _moveAction = default;
    [SerializeField] private const float MOVE_RANGE = 8;

    private void Start()
    {
        //--InputSystem使ってみた--//
        var playerInput = GetComponent<PlayerInput>();
        var actionMap = playerInput.currentActionMap;
        _moveAction = actionMap["Move"];
        //--InputSystem使ってみた--// 参考:https://gamedev65535.com/entry/unity_inputsystem_howtouse/#i-2
        _forward = transform.forward;
        _rigidbody.velocity = _forward * _speed;
    }

    void Update()
    {

        if (_isStop) return;

        if (gameObject.transform.position.z > _goalObj.position.z)
        {
            _rigidbody.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, transform.position.y, _goalObj.position.z);
            _isStop = true;
            return;
        }

        var move = _moveAction.ReadValue<float>();
        _rigidbody.velocity = new Vector3(move * _speed, _rigidbody.velocity.y, _rigidbody.velocity.z);

        //プレイヤーが外側に出れないように
        if(transform.position.x < -MOVE_RANGE) //左端
        {
            transform.position = new Vector3(-MOVE_RANGE, transform.position.y, transform.position.z);
        }
        else if(transform.position.x > MOVE_RANGE) //右端
        {
            transform.position = new Vector3(MOVE_RANGE, transform.position.y, transform.position.z);
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
