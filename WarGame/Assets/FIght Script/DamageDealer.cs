using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private bool canDealDamage;
    private List<GameObject> hasDealtDamage;

    [SerializeField] private float weaponLength;

    [SerializeField] private float weaponDamage;
    // Start is called before the first frame update
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (canDealDamage)
        // {
        //     RaycastHit hit;
        //     int layerMask = 1;
        //     if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
        //     {
        //         if (!hasDealtDamage.Contains(hit.transform.gameObject))
        //         {
        //             print("damage");
        //             hasDealtDamage.Add(hit.transform.gameObject);
        //         }
        //     }
        // }
        
        if (canDealDamage)
        {
            RaycastHit hit;  
            int layerMask = 1 << 3;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                if (!hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    print("damage");
                    hasDealtDamage.Add(hit.transform.gameObject);
                    EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(weaponDamage);
                    }
                }
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}

