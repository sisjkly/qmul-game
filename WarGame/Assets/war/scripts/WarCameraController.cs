using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarCameraController : MonoBehaviour
{
    public float maxZ;

    public float minZ;

    public float maxX;

    public float minX;
    public float moveSpeed = 5f;

    public float minCameraHight = 8;

    public float maxCameraHight = 32;
    public Vector3 defPos;
    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingPanel.instance.isShow)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = defPos;    
        }
        var p = Input.mousePosition;
       // print(p);
        var w = Screen.width;

        var h = Screen.height;
        var speed = 1;
        if (p.x>=0.95*w && transform.position .x <maxX)
        {
            transform.Translate(Vector3.right*moveSpeed*Time.deltaTime *speed,Space.World);
        }
        if (p.y>= 0.95 * h &&transform.position.z<maxZ)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime *speed, Space.World);
        }

        if(p.x<=0 && transform.position.x >minX) { transform.Translate(Vector3.left * moveSpeed * Time.deltaTime*speed, Space.World); }


        if (p.y <=  0 && transform.position.z > minZ) { transform.Translate(Vector3.back * moveSpeed * Time.deltaTime * speed, Space.World); }
        var wheel = Input.GetAxis("Mouse ScrollWheel");
       
     

        if (wheel>0 &&transform.position.y<maxCameraHight)
        {
            transform.Translate(transform.forward*Time.deltaTime*-moveSpeed*5 , Space.Self);
        }

        if (wheel < 0 && transform.position.y > minCameraHight)
        {
            transform.Translate(transform.forward * Time.deltaTime * moveSpeed *5,Space.Self);
        }
    }
}
