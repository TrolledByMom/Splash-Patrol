using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pawn : EnemyBase
{
    [SerializeField] private IdleSOBase _idleLogic;
    [SerializeField] private MoveSOBase _moveLogic;

    private IdleSOBase IdleBaseInstance;// { get; set; }
    private MoveSOBase MoveBaseInstance;// { get; set; }


    private void Awake()
    {
        _healthManager = GetComponent<EnemyHealthManager>();
        if (_idleLogic != null) { IdleBaseInstance = Instantiate(_idleLogic); }
        if (_moveLogic != null) { MoveBaseInstance = Instantiate(_moveLogic); }
    }

    // Start is called before the first frame update
    void Start()
    {
        IdleBaseInstance.Initialize(gameObject, this, gameObject.GetComponent<NavMeshAgent>());
        MoveBaseInstance.Initialize(gameObject, this, gameObject.GetComponent<NavMeshAgent>());
    }

    void Update()
    {
        Move();
    }

    protected override void Idle()
    {
        if (IdleBaseInstance != null) { IdleBaseInstance.IdleLogic(); }
    }

    protected override void Move()
    {
        if (_moveLogic != null) { MoveBaseInstance.MoveLogic(); }

        //transform.position = Vector3.MoveTowards(transform.position, base._moveTarget.position, base._movSpeed* Time.deltaTime);
    }
}
