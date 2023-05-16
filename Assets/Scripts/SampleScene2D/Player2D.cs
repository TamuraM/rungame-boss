using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2D : MonoBehaviour
{
    private InputAction _bangAction, _moveAction, _jumpAction;
    [SerializeField] private GameObject _bullet = default;

    [SerializeField] private float _speed = 3;

    //[SerializeField] private bool _isJumping = false;
    [SerializeField] private byte _isJumping = 0b0000_0000;
    private float _minY = default;
    [SerializeField] private float _maxJumpPower = 2;
    [SerializeField] private float _nowJumpPower = 2;
    [SerializeField] private float _antiJumpPower = 2;

    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        var actionMap = playerInput.currentActionMap;
        _bangAction = actionMap["Bang"];
        _moveAction = actionMap["MoveTrigger"];
        _jumpAction = actionMap["Jump"];
        _bangAction.performed += Bang;
        _jumpAction.performed += Jump;

        _nowJumpPower = _maxJumpPower;
        _minY = transform.position.y;
    }

    private void Update()
    {
        float moveX = transform.position.x + _moveAction.ReadValue<float>() * _speed * Time.deltaTime;
        float moveY = transform.position.y;

        if (_isJumping > 0)
        {
            //�Q�l�Fhttps://www.youtube.com/watch?v=zxOBCjqP8-Y&list=PLi8SA3sbzYVSQmkQUa-zz7BcKL8YWAt8a&index=3
            moveY += _nowJumpPower * Time.deltaTime;
            _nowJumpPower -= _antiJumpPower * Time.deltaTime;

            if (transform.position.y < _minY)
            {
                moveY = _minY;
                _nowJumpPower = _maxJumpPower;
                _isJumping = 0;
            }

        }

        transform.position = new Vector3(moveX, moveY, 0);
    }

    private void Bang(InputAction.CallbackContext context)
    {
        Vector3 bulletShotPos = new Vector3(this.transform.position.x + this.transform.localScale.x, this.transform.position.y, 0);
        Instantiate(_bullet, bulletShotPos, Quaternion.identity);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if((_isJumping & 1) == 0) //�܂��W�����v���ĂȂ�������
        {
            _isJumping = 1; //��x�W�����v�������Ƃɂ���
            return;
        }

        if((_isJumping & 3) == 1) //���łɈ�x�W�����v���Ă�����
        {
            _isJumping = 3; //��i�W�����v���Ă邱�Ƃɂ���
            _nowJumpPower = _maxJumpPower;
            return;
        }

    }

}
