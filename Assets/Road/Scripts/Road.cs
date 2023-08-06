using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public static float _speed = 20f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyRoad();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(-transform.forward * _speed * Time.fixedDeltaTime);
    }

    private void DestroyRoad()
    {
        if(transform.position.z < -33f) Destroy(gameObject); 
    }
}
