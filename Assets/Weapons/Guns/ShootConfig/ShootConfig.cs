using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Config", menuName = "Weapons/Guns/Shoot Config", order = 2)]
public class ShootConfig : ScriptableObject, System.ICloneable
{
    public LayerMask HitMask;
    public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.1f);
    public float FireRate = 0.25f;

    [Header("Recoil Values")]
    public float _recoilx;
    public float _recoily;
    public float _recoilz;

    public float _kickBackz;

    public float _snappiness, _returnAmount;

    public object Clone()
    {
        ShootConfig shootConfig = CreateInstance<ShootConfig>();
        Utilities.CopyValues(this, shootConfig);
        return shootConfig;
    }
}
