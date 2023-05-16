using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Move : MonoBehaviour
{
    [SerializeField, Header("移動スピード")] private float _speed = 5f;
    [SerializeField] private Rigidbody _rigidbody;
    private bool _isStop = false;
    [SerializeField, Header("ゴールの位置")] private Transform _goalObj;
    private Vector3 _forward;
    private InputAction _moveAction = default;
    [SerializeField] private const float MOVE_RANGE = 8;
    [SerializeField] private Score _score = default;
    [SerializeField] private Transform[] _lanes = new Transform[3];
    private int _laneIndex = 1;

    private void Awake()
    {
        //--InputSystem使ってみた--//
        var playerInput = GetComponent<PlayerInput>();
        var actionMap = playerInput.currentActionMap;
        _moveAction = actionMap["MoveTrigger"];
        _moveAction.started += MoveLane;
        //--InputSystem使ってみた--//
        //参考:https://gamedev65535.com/entry/unity_inputsystem_howtouse/#i-2
        //https://logmi.jp/tech/articles/326468
    }

    //private void OnEnable()
    //{
    //    _moveAction.started += MoveLane;
    //}

    //private void OnDisable()
    //{
    //    _moveAction.started -= MoveLane;
    //}

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
            transform.position = new Vector3(transform.position.x, transform.position.y, _goalObj.position.z);
            _isStop = true;
            return;
        }

        //transform.position += new Vector3(0, 0, _speed);

        //--自由に移動できる時--//
        //var move = _moveAction.ReadValue<float>();
        //_rigidbody.velocity = new Vector3(move * _speed, _rigidbody.velocity.y, _rigidbody.velocity.z);

        ////プレイヤーが外側に出れないように
        //if(transform.position.x < -MOVE_RANGE) //左端
        //{
        //    transform.position = new Vector3(-MOVE_RANGE, transform.position.y, transform.position.z);
        //}
        //else if(transform.position.x > MOVE_RANGE) //右端
        //{
        //    transform.position = new Vector3(MOVE_RANGE, transform.position.y, transform.position.z);
        //}
        //--自由に移動できる時--//
    }

    public void MoveLane(InputAction.CallbackContext context)
    {
        if(_isStop) return;

        _laneIndex += (int)_moveAction.ReadValue<float>();

        if (_laneIndex < 0) { _laneIndex = 0; return; }
        if (_laneIndex > 2) { _laneIndex = 2; return; }

        transform.DOMoveX(_lanes[_laneIndex].position.x, 0.2f).SetEase(Ease.Linear).SetAutoKill();
    }

    private void OnTriggerEnter(Collider other)
    {

        //キューブに当たったら消す
        if (other.gameObject.tag == "Target")
        {
            Destroy(other.gameObject);
            _score.AddScore(1);
        }

    }

}
