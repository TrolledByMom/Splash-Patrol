using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;
using static AnimsList;
using TMPro;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/AttackLogic/ProjectileAttack", order = 2)]
public class ProjectileAttack : AttackSOBase
{
    public static ObjectPool<GameObject> Pool;

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
        GameObject projectille = Instantiate(base.projectile, bulletPoint.position, Quaternion.identity);
        projectille.GetComponent<IPooled>().SetPool(Pool);
        ApplyForce(projectille);
       // projectille.GetComponent<IProjectile>().SetProjectileForce(projectileForce,enemyObject);

        return projectille;
    }

    private void ApplyForce(GameObject go)
    {
        Vector3 newVelocity = enemyObject.transform.forward * projectileForce;

        var rb = go.GetComponent<Rigidbody>();

        rb.velocity = newVelocity;
        //rb.AddForce(newVelocity, ForceMode.Impulse);
    }
}
