using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField] private GunType _gunType;
    [SerializeField] private MeleeWeaponType _meleeType;
    [SerializeField] private Transform _gunParent;

    [Header("Guns SO")]
    [SerializeField] private List<GunsSO> _guns;

    [Header("Melees SO")]
    [SerializeField] private List<MeleeSO> _melees;

    [Header("Runtime Filled")]
    public GunsSO ActiveGun;
    public MeleeSO ActiveMelee;

    private void Start()
    {

        GunsSO gun = _guns.Find(gun => gun.Type == _gunType);

        if (gun == null)
        {
            Debug.Log("error ... no GunSO found");
            return;
        }

       SetupGun(gun);
       // ActiveGun._model.SetActive(false);

        // melle spawning

      /*  MeleeSO melee = _melees.Find(melee => melee.MelleType == _meleeType);

        if (melee == null)
        {
            Debug.Log("error ... no GunSO found");
            return;
        }

        ActiveMelee = melee;
        ActiveMelee.Spawn(_gunParent, this);*/



    }

    private void SetupGun(GunsSO GunSO)
    {
        ActiveGun = GunSO.Clone() as GunsSO;
        ActiveGun.Spawn(_gunParent, this);
    }

    public void DespawnActiveGun()
    {
        ActiveGun.Despawn();
        Destroy(ActiveGun);
    }

    public void PickupGun(GunsSO GunSO)
    {
        DespawnActiveGun();
        SetupGun(GunSO);
    }
}
