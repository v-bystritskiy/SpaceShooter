using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _startYPosition = 6f;
    private float _endYPosition = -6f;
    private float _xBounds = 8f;
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _animator;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }
        if (_animator == null)
        {
            Debug.LogError("Animator is NULL");
        }
    }

    void Update()
    {
        CalculateMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) player.Damage();

            _animator.SetTrigger("OnEnemyDead");
            _speed = 0f;
            Destroy(this.gameObject, 2.8f);
        } 
        else if (other.tag == "Laser")
        {
            if (_player != null) _player.IncrementScore(Random.Range(1,15));

            Destroy(other.gameObject);
            _animator.SetTrigger("OnEnemyDead");
            _speed = 0f;
            Destroy(this.gameObject, 2.8f);
        }
    }

    void CalculateMovement() 
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= _endYPosition)
        {
           MoveToStartPosition();
        }
    }

    void MoveToStartPosition()
    {
        float randomX = Random.Range(_xBounds * -1f, _xBounds);
        transform.position = new Vector3(randomX, _startYPosition, 0);
    }
}