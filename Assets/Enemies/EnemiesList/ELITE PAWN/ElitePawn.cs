using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ElitePawn : EnemyBase
{
   // [SerializeField] private IdleSOBase _idleLogic;
    [SerializeField] private MoveSOBase _moveLogic;
    [SerializeField] private MoveSOBase _chasePlayerLogic;
    [SerializeField] private AttackSOBase _attackLogic;

   // private IdleSOBase IdleBaseInstance;// { get; set; }
    private MoveSOBase MoveBaseInstance;// { get; set; }
    private MoveSOBase MoveBaseInstance2;// { get; set; }
    private AttackSOBase AttackBaseInstance;// { get; set; } 

    [SerializeField] private int _attackDistance;

    private NavMeshAgent _agent;

    private void OnEnable()
    {
        MoveAnim = AnimsController.Anims.Single(MoveAnim => MoveAnim.AnimKey == "MOVE").AnimName;
        ChaseAnim = AnimsController.Anims.Single(ChaseAnim => ChaseAnim.AnimKey == "CHASE").AnimName;
        AttackAnim = AnimsController.Anims.Single(AttackAnim => AttackAnim.AnimKey == "ATTACK").AnimName;
        DieAnim = AnimsController.Anims.Single(DieAnim => DieAnim.AnimKey == "DIE").AnimName;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _healthManager = GetComponent<EnemyHealthManager>();
       // if (_idleLogic != null) { IdleBaseInstance = Instantiate(_idleLogic); }
        if (_moveLogic != null) { MoveBaseInstance = Instantiate(_moveLogic); }
        if (_chasePlayerLogic != null) { MoveBaseInstance2 = Instantiate(_chasePlayerLogic); }
        if (_attackLogic != null) { AttackBaseInstance = Instantiate(_attackLogic); }
    }

    // Start is called before the first frame update
    void Start()
    {
       // IdleBaseInstance.Initialize(gameObject, this, gameObject.GetComponent<NavMeshAgent>());
        MoveBaseInstance.Initialize(gameObject, this, _agent);
        MoveBaseInstance2.Initialize(gameObject, this,_agent);
        AttackBaseInstance.Initialize(gameObject, this, _agent);
        ChangeState(State.Move);
    }

    void Update()
    {
        StateMachine();      
    }

    protected override void Move()
    {
        if (_healthManager.CurrentHealth > 0)
        {
            if (!AnimsController.ISAnimationPlaying(_animator, AttackAnim))
            {
                if (_healthManager.CurrentHealth < _healthManager.MaxHealth)
                {
                    AnimsController.Playanimation(_animator, ChaseAnim);
                    if (_chasePlayerLogic != null) { MoveBaseInstance2.MoveLogic(); }
                }
                else
                {
                    AnimsController.Playanimation(_animator, MoveAnim);
                    if (_moveLogic != null) { MoveBaseInstance.MoveLogic(); }
                }
            }
        }else { ChangeState(State.Die); }
    }

    protected override void Attack()
    {
        if (_healthManager.CurrentHealth > 0)
        {
            if (_attackLogic != null) { AttackBaseInstance.AttackLogic(_attackDistance, _animator, AttackAnim); }
        }else { ChangeState(State.Die); }
    }


    protected override void Die()
    {
        _agent.isStopped = true;
    }
}