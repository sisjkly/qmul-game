using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PawnController : IPawnBase
{
    public GameObject arrow;
    public Transform BowPoint;
    AudioEffect effect;
    public class TP
    {
        public Vector3 pos;
    }
    public  TP tp;
    // public TreeController tree;
    public PawnCreater creater;
    public Animator ani;
    public List<string> targets = new List<string>();
    /// final attack target
    public IPawnBase lastTar;

    public Mode mode = Mode.Defense;
    NavMeshAgent agent;
    /// Attack the target nearby
    public List<IPawnBase> pawnControllers = new List<IPawnBase>();
    
    /// Attack interval
    public float attackCool = 2f;

    public float timer = 0f;

    public float attackDistance = 2f;

    public bool IsArrow = false;

    public override void Hurt(int value, IPawnBase pawnBase)
    {
        StartCoroutine(HurtLater(pawnBase.Level*5+value, pawnBase));
    }
    IEnumerator HurtLater(int value, IPawnBase pawnBase)
    {
        yield return new WaitForSeconds(1f);
        Hp -= value;
        if (Hp <= 0)
        {
            pawnBase.Killed();
            Destroy(gameObject);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
     //   print(other.name);
        if (targets.Contains(other.tag))
        {
          var pawn = other.gameObject.GetComponent<IPawnBase>();
            if (pawn != null && !pawnControllers.Contains(pawn))
            {
                pawnControllers.Add(pawn);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.tag))
        {
            var pawn = other.gameObject.GetComponent<IPawnBase>();
            if (pawn != null && pawnControllers.Contains(pawn))
            {
                pawnControllers.Remove(pawn);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        effect = GetComponentInChildren<AudioEffect>();
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        var randX = Random.Range(creater.transform.position.x - 15f, creater.transform.position.x + 15f);
        var randZ = Random.Range(creater.transform.position.z - 15f, creater.transform.position.z + 15f);
        defensePos = new Vector3(randX, transform.position.y, randZ);

    }
    Vector3 defensePos;

    private bool IsCtrl = false;
    Coroutine co = null;
    public void MTP(TP tP)
    {
        if (co != null)
        {
            StopCoroutine(co);
            IsCtrl = false;

            co = StartCoroutine(MoveToTP(tP));
        }
        else
        {
            co = StartCoroutine(MoveToTP(tP));
        }
       /// Coroutine
    }
     IEnumerator MoveToTP(TP tP)
    {
        IsCtrl = true;
        while (Vector3.Distance(transform.position,tP.pos)>2f)
        {
            agent.SetDestination(tP.pos);
            agent.isStopped = false;
            ani.SetBool("run", true);
            yield return null;
        }
        agent.isStopped = true;
        ani.SetBool("run", false);
        IsCtrl = false;
        mode = Mode.Idle;
    }

    public void Throw(Transform target)
    {
        var arr = Instantiate(arrow);
        arr.transform.position = BowPoint.position;
        arr.GetComponent<Arrow>().target = target;
    }

    
    /// Heal
    float retimer;
    
    void ReCoverHp()
    {
        if (retimer<1) {
            retimer += Time.deltaTime;
        }
        else
        {
            retimer = 0;
            Hp += Level;
            if (Hp>MaxHp)
            {
                Hp = MaxHp;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (SettingPanel.instance.isShow)
        {
            return;
        }
        if (IsCtrl)
        {
            return;
        }
        ReCoverHp();
        switch (mode)
        {
            case Mode.Idle:
                pawnControllers = pawnControllers.Where(it => it != null).ToList();
                if (pawnControllers.Count > 0)
                {
                    pawnControllers.Sort((a, b) => {

                        return Vector3.Distance(transform.position, a.transform.position) - Vector3.Distance(transform.position, b.transform.position) > 0 ? 1 : -1;
                    });
                    lastTar = pawnControllers[0];
                }
                else
                {
                    lastTar = null;
                }
                if (lastTar != null)
                {
                    if (Vector3.Distance(transform.position, lastTar.transform.position) > attackDistance)
                    {
                        agent.SetDestination(lastTar.transform.position);
                        agent.isStopped = false;
                        ani.SetBool("run", true);
                    }
                    else
                    {
                        agent.SetDestination(transform.position);
                        agent.isStopped = true;
                        ani.SetBool("run", false);
                        if (timer <= attackCool)
                        {
                            timer += Time.deltaTime;
                        }
                        else
                        {
                            timer = 0f;
                            transform.LookAt(lastTar.transform);
                            ani.Play("Attack");
                            if (IsArrow)
                            {
                                Throw(lastTar.transform);
                            }
                            effect.Playeff();
                            lastTar.Hurt(AttackValue,this);
                        }
                    }

                }

                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    ani.SetBool("run", false);
                
                }
                break;
            case Mode.Defense:
                pawnControllers = pawnControllers.Where(it => it != null).ToList();
                if (pawnControllers.Count > 0)
                {
                    pawnControllers.Sort((a, b) => {

                        return Vector3.Distance(transform.position, a.transform.position) - Vector3.Distance(transform.position, b.transform.position) > 0 ? 1 : -1;
                    });
                    lastTar = pawnControllers[0];
                }
                else
                {
                    lastTar = null;
                }
                if (lastTar != null)
                {
                    if (Vector3.Distance(transform.position, lastTar.transform.position) > attackDistance)
                    {
                        agent.SetDestination(lastTar.transform.position);
                        agent.isStopped = false;
                        ani.SetBool("run", true);
                    }
                    else
                    {
                        agent.SetDestination(transform.position);
                        agent.isStopped = true;
                        ani.SetBool("run", false);
                        if (timer <= attackCool)
                        {
                            timer += Time.deltaTime;
                        }
                        else
                        {
                            timer = 0f;
                            transform.LookAt(lastTar.transform);
                            ani.Play("Attack");
                            if (IsArrow)
                            {
                                Throw(lastTar.transform);
                            }
                            effect.Playeff();
                            lastTar.Hurt(AttackValue, this);
                        }
                    }

                }

                else
                {
                    if (Vector3.Distance(transform.position, defensePos) > 2f)
                    {
                        agent.SetDestination(defensePos);
                        agent.isStopped = false;
                        ani.SetBool("run", true);
                    }
                    else
                    {
                        var randX = Random.Range(creater.transform.position.x - 10f, creater.transform.position.x + 10f);
                        var randZ = Random.Range(creater.transform.position.z-10f,creater.transform.position.z+10f);
                        defensePos = new Vector3 (randX,transform.position.y,randZ);
                       
                    }
                }


                break;
            case Mode.Attack:
                pawnControllers = pawnControllers.Where(it => it != null).ToList();
                if (pawnControllers.Count > 0)
                {
                    pawnControllers.Sort((a, b) => {

                        return Vector3.Distance(transform.position, a.transform.position) - Vector3.Distance(transform.position, b.transform.position) > 0 ? 1 : -1;
                    });
                    lastTar = pawnControllers[0];
                }
                else
                {
                    lastTar = null;
                }
                if ( lastTar == null)
                {
                    lastTar = GameObject.FindGameObjectWithTag(targets[0]).GetComponent<IPawnBase>();
                }
                if (lastTar != null)
                {
                    if (Vector3.Distance(transform.position, lastTar.transform.position) > attackDistance)
                    {
                        agent.SetDestination(lastTar.transform.position);
                        agent.isStopped = false;
                        ani.SetBool("run", true);
                    }
                    else
                    {
                        agent.SetDestination(transform.position);
                        agent.isStopped = true;
                        ani.SetBool("run", false);
                        if (timer <= attackCool)
                        {
                            timer += Time.deltaTime;
                        }
                        else
                        {
                            timer = 0f;
                            transform.LookAt(lastTar.transform);
                            ani.Play("Attack");
                            if (IsArrow)
                            {
                                Throw(lastTar.transform);
                            }
                            effect.Playeff();
                            lastTar.Hurt(AttackValue, this);
                        }
                    }

                }



                break;
            case Mode.Gold:
                pawnControllers = pawnControllers.Where(it => it != null).ToList();
                if (pawnControllers.Count > 0)
                {
                    pawnControllers.Sort((a, b) => {

                        return Vector3.Distance(transform.position, a.transform.position) - Vector3.Distance(transform.position, b.transform.position) > 0 ? 1 : -1;
                    });
                    lastTar = pawnControllers[0];
                }
                else
                {
                    lastTar = null; 
                }
                if (lastTar ==null)
                {

                    lastTar = creater.tree;
                    if (IsArrow)
                    {
                        return;
                    }
                }
             
                if (lastTar != null)
                {
                    if (Vector3.Distance(transform.position, lastTar.transform.position) > attackDistance)
                    {
                        agent.SetDestination(lastTar.transform.position);
                        agent.isStopped = false;
                        ani.SetBool("run", true);
                    }
                    else
                    {
                        agent.SetDestination(transform.position);
                        agent.isStopped = true;
                        ani.SetBool("run", false);
                        if (timer <= attackCool)
                        {
                            timer += Time.deltaTime;
                        }
                        else
                        {
                            timer = 0f;
                            transform.LookAt(lastTar.transform);
                            ani.Play("Attack");
                            if (IsArrow) {
                                Throw(lastTar.transform);
                            }
                            effect.Playeff();
                            lastTar.Hurt(AttackValue, this);
                        }
                    }

                }


                break;
            default:
                break;
        }
    }
    private int killNum=0;
    public override void Killed()
    {
        if (Level>=3)
        {
            return;
        }
       killNum++;
        if (killNum>=3*Level )
        {
            Level++;
        }
      
    }
}
