using DG.Tweening;
using UnityEngine;

public abstract class MeleeWeapon : MonoBehaviour
{
    [SerializeField] public RuntimeAnimatorController animatorForWeapon;
    [SerializeField] protected float hitDuration = 0.5f;

    private void Start(){InternalStart();}
    protected abstract void InternalStart();

    public abstract void NotifyAttack(string collidersToActivate);

}
