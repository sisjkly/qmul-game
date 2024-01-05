using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarGameManager : MonoBehaviour
{
    public static WarGameManager instance;
    private void Awake()
    {
           instance = this;
    }

    public List<Boss> bosses = new List<Boss>();    
    // Start is called before the first frame update
    void Start()
    {
        bosses= FindObjectsOfType<Boss>().ToList();  
    }

    // Update is called once per frame
    void Update()
    {
        bosses = bosses.Where(it=>it!=null).ToList();

        if(bosses.Count <= 0)
        {
            SceneManager.LoadScene("Win");
        }
    }
}
