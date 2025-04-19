using LastBreakthrought.Infrustructure.Services.ConfigProvider;
using LastBreakthrought.NPC.Base;
using LastBreakthrought.Player;
using LastBreakthrought.UI.NPC.Enemy;
using LastBreakthrought.Util;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace LastBreakthrought.NPC.Enemy
{
    public abstract class EnemyBase : MonoBehaviour, IEnemy, IDamagable
    {
        [Header("Base:")]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyAttackIndecator _attackIndecator;
        [SerializeField] private EnemyHealthHandler _healthHandler;

        [Header("Setting:")]
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _attackUpOffset = 1f;
        [SerializeField] private float _attackForwardOffset = 1f;

        public IEnemyTarget Target { get; private set; }

        private readonly Collider[] _attackingTargetColliders = new Collider[2];
        private readonly Collider[] _targetColliders = new Collider[2];

        private float _visionRadious;
        private float _attakingRadious;
        private float _wanderingSpeed;
        private float _chassingSpeed;

        private float _attackAnimationTime;
        private float _dyingAnimationTime;

        private bool _isTargetInVisionRange = false;
        private bool _isTargetInAttakingRange = false;
        private bool _isDied = false;

        private NPCStateMachine _stateMachine;
        private PlayerHandler _playerHandler;
        private ICoroutineRunner _coroutineRunner;
        private BoxCollider _wanderingZone;
        private IConfigProviderService _configProvider;

        [Inject]
        private void Construct(PlayerHandler playerHandler, ICoroutineRunner coroutineRunner, IConfigProviderService configProviderService)
        {
            _playerHandler = playerHandler;
            _coroutineRunner = coroutineRunner;
            _configProvider = configProviderService;
        }

        public void OnSpawned(BoxCollider wanderingZone, string id)
        {
            _wanderingZone = wanderingZone;

            var enemyData = _configProvider.EnemyConfigHolderSO.GetEnemyDataById(id);
            ConfigurateEnemy(enemyData);

            _healthHandler.OnHealthGone += Died;

            _stateMachine = new NPCStateMachine();

            AddStates();
        }

        public Vector3 GetPosition() =>
            transform.position;

        public void ApplyDamage(float damage) => 
            _healthHandler.Health -= damage;

        private void Update() => _stateMachine.Tick();

        public bool TryToFindTarget()
        {
            Physics.OverlapSphereNonAlloc(transform.position + Vector3.up, _visionRadious, _targetColliders, _layerMask);

            if (_targetColliders.Length > 0)
            {
                for (int i = 0; i < _targetColliders.Length; i++)
                {
                    if (_targetColliders[i] != null)
                    {
                        if (_targetColliders[i].TryGetComponent(out IEnemyTarget enemyTarget))
                        {
                            Target = enemyTarget;
                            _targetColliders[i] = null;
                            _isTargetInVisionRange = true;
                            break;
                        }
                    }
                    else
                        _isTargetInVisionRange = false;
                }
            }
            else
            {
                _isTargetInVisionRange = false;
                Target = null;
            }

            return _isTargetInVisionRange;
        }
        
        public bool TryToAttackTarget()
        {
            Vector3 position = transform.position + transform.forward * _attackForwardOffset + Vector3.up * _attackUpOffset;
            var targets = Physics.OverlapSphereNonAlloc(position, _attakingRadious, _attackingTargetColliders, _layerMask);

            if (targets > 0)
                _isTargetInAttakingRange = true;
            else
                _isTargetInAttakingRange = false;

            return _isTargetInAttakingRange;
        }

        public bool IsDied() => _isDied;

        private void AddStates()
        {
            var wandering = new EnemyWanderingState(this, _coroutineRunner, _agent, _wanderingZone, _animator, _wanderingSpeed);
            var chassing = new EnemyChassingState(this, _coroutineRunner, _agent, _animator, _chassingSpeed);
            var attacking = new EnemyAttackingState(this, _coroutineRunner, _agent, _animator, _attackAnimationTime);
            var dying = new EnemyDyingState(this, _coroutineRunner, _agent, _animator, _dyingAnimationTime);

            _stateMachine.AddTransition(wandering, chassing, () => _isTargetInVisionRange);
            _stateMachine.AddTransition(chassing, wandering, () => !_isTargetInVisionRange);
            _stateMachine.AddTransition(chassing, attacking, () => _isTargetInAttakingRange);
            _stateMachine.AddTransition(attacking, chassing, () => !_isTargetInAttakingRange);
            _stateMachine.AddAnyTransition(dying, () => _isDied);

            _stateMachine.EnterInState(wandering);
        }

        private void ConfigurateEnemy(Configs.Enemy.EnemyConfigSO enemyData)
        {
            _visionRadious = enemyData.VitionRadious;
            _attakingRadious = enemyData.AttakingRadious;
            _wanderingSpeed = enemyData.WandaringSpeed;
            _chassingSpeed = enemyData.ChassingSpeed;
            _attackAnimationTime = enemyData.AttakAnimationTime;
            _dyingAnimationTime = enemyData.DyingAnimationTime;

            _attackIndecator.Init(enemyData.AttackDamage);
            _healthHandler.Init(enemyData.MaxHealth);
        }

        private void Died() => _isDied = true;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, _visionRadious);

            Vector3 position = transform.position + transform.forward * _attackForwardOffset + Vector3.up * _attackUpOffset;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position * 1f, _attakingRadious);
        }
    }
}
