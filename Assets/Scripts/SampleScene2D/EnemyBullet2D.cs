using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet2D : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    private Transform _target = default;

    private void Start()
    {
        _target = GameObject.FindObjectOfType<Player2D>().transform;
    }

    void Update()
    {
        //�v���C���[����`�A����(�e)��_�Ƃ��Ĕ���
        //�v���C���[���~�ł�������
        bool isHitX = Mathf.Abs(transform.position.x - _target.position.x)
            <= _target.localScale.x / 2; //x���W���d�Ȃ��Ă��邩
        bool isHitY = Mathf.Abs(transform.position.y - _target.position.y)
            <= _target.localScale.y / 2; //y���W���d�Ȃ��Ă��邩

        if(isHitX && isHitY) //x���W��y���W�ǂ�����d�Ȃ��Ă��玩����j��
        {
            _target.gameObject.GetComponent<IDamageable2D>().Damage(1);
            Destroy(gameObject);
        }

        transform.position += new Vector3(-_speed * Time.deltaTime, 0, 0);
    }
}
