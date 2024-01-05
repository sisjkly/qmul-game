using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RayReason : MonoBehaviour
{
    public static RayReason Instance;

    private void Awake()
    {
        Instance = this;
    }

    public   List< PawnController> controllers = new List<PawnController>() ;


    private Vector2 boxStartPos;   // Frame selection starting position
    private bool isSelecting = false;  // Whether frame selection is in progress
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public LayerMask mask;
    public LayerMask mask2;

    public PawnCreater creater;
    public Vector3 p1;

    public Vector3 p2;

    void OnGUI()
    {
        if (SettingPanel.instance.isShow)
        {
            return;
        }
        if (isSelecting)
        {
            // Get the frame selection area
            Rect boxRect = new Rect(boxStartPos.x, Screen.height - boxStartPos.y, Input.mousePosition.x - boxStartPos.x, -Input.mousePosition.y + boxStartPos.y);

            // Draw a selection rectangle
            GUI.Box(boxRect, GUIContent.none);

            // Detect objects within the frame selection area
          
        }
    }
    public GameObject eff;
    // Update is called once per frame
    void Update()
    {
        if (SettingPanel.instance.isShow)
        {
            return;
        }
      
        if (Input.GetMouseButtonDown(0))
        {
            boxStartPos = Input.mousePosition;
            isSelecting = true;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 9999, mask2))
            {
               p1= hit.point;
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 9999, mask2))
            {
                p2 = hit.point;
                float maxX = p1.x > p2.x ? p1.x : p2.x;
                float maxZ = p1.z > p2.z ? p1.z : p2.z;
                float minX = p1.x < p2.x ? p1.x : p2.x;
                float minZ = p1.z < p2.z ? p1.z : p2.z;

                creater.pawnControllers = creater.pawnControllers.Where(it => it != null).ToList();
                controllers = creater.pawnControllers.FindAll(it => it.transform.position.x >= minX && it.transform.position.x <= maxX && it.transform.position.z >= minZ && it.transform.position.z <= maxZ); ;

            }
        }
      

        /*
                if (Input.GetMouseButtonDown(0))
                {
                    var ray =  Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray.origin,ray.direction,out RaycastHit hit,9999,mask)) 
                    {
                        print(hit);
                        controller = hit.collider.GetComponent<PawnController>();
                    }
                    else
                    {
                        controller = null;
                    }
                }
        */
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 9999, mask2))
            {
                controllers = controllers.FindAll(it=>it!=null);
                var e = Instantiate(eff, hit.point,Quaternion.identity);
                Destroy(e,1.5f);
               foreach (var controller in controllers)
                {
                    controller.MTP(new PawnController.TP()
                    {
                        pos= hit.point
                    });
                }
            }
        }
    }
}
