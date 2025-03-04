using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/AttackLogic/ProjectileAttack", order = 2)]
public class ProjectileAttack : AttackSOBase
{
    private void OnEnable()
    {
        Pool = new ObjectPool<GameObject>(CreateProjectile);
    }


    public override void AttackLogic(Animator animator)
    {
        animator.SetFloat("AttackSpeed", 1 * attackSpeed);
        enemyAgent.isStopped = true;
        GetProjectile();
       
    }

    public void GetProjectile()
    {
        var instance = Pool.Get();       
        instance.transform.position = bulletPoint.position;
        instance.SetActive(true);
        ApplyForce(instance);
    }



    private GameObject CreateProjectile()
    {
        GameObject projectile = Instantiate(base.projectile, bulletPoint.position, Quaternion.identity);
        projectile.transform.parent = enemyObject.transform;    
        projectile.GetComponent<IPooled>().SetPool(Pool);
        ApplyForce(projectile);

        return projectile;
    }

    private void ApplyForce(GameObject go)
    {
        Vector3 newVelocity = enemyObject.transform.forward * projectileForce;

        var rb = go.GetComponent<Rigidbody>();

        rb.velocity = newVelocity;
    }
}
