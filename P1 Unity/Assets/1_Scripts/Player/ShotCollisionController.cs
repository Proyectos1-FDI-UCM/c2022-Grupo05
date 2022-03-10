
using UnityEngine;

public class ShotCollisionController : MonoBehaviour
{
    [SerializeField] private bool _isUpgraded = false;

    #region references 
    private EnemyLifeComponent enemy;
    private BossLifeComponent boss;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.GetComponent<EnemyLifeComponent>();
        boss = collision.GetComponentInParent < BossLifeComponent>();
        if (enemy != null) enemy.Damage(_isUpgraded);
        else if (boss != null) boss.Damage(_isUpgraded);
        Destroy(gameObject);
    }
    #endregion
}
