using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable)
        {
           bool wasHeal= damageable.Heal(healthRestore);
            if(wasHeal)
              Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
