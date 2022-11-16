using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    enum PowerupType 
    {
        TripleShot,
        Speed,
        Shield
    }

    [SerializeField]
    private float _speed = 3f;
    private float _endYPosition = -7f;
    [SerializeField]
    private PowerupType _type;

    void Update()
    {
        CalculateMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                switch(_type)
                {
                    case PowerupType.TripleShot:
                    player.ActivateTripleShotPowerup();
                    Destroy(this.gameObject);
                    return;
            
                    case PowerupType.Speed:
                    player.ActivateSpeedPowerup();    
                    Destroy(this.gameObject); 
                    return;

                    case PowerupType.Shield:
                    player.ActivateShieldPowerup();
                    Destroy(this.gameObject);
                    return;
                }   
            }
        } 
    }
    
    void CalculateMovement() 
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= _endYPosition)
        {
           Destroy(this.gameObject);
        }
    }

}
