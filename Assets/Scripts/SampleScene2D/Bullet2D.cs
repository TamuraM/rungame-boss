using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    [SerializeField] float _speed = 1;

    void Update()
    {
        this.transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);
    }

}
