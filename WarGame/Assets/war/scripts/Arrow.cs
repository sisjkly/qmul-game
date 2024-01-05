using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class Arrow : MonoBehaviour
{

    public Transform target;

    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
       // var distance = Vector3.Distance(transform.position, target.position);
        Speed = 10/1;
        Destroy(gameObject,1.2f);

    }

    // Update is called once per frame
    void Update()
    {
        if (target==null)
        {
            Destroy(gameObject);
            return;
        }
        transform.LookAt(target.position);
        transform .position = Vector3.MoveTowards(transform.position,target.position+new Vector3(0,1,0),Speed*Time.deltaTime);
    }
}
