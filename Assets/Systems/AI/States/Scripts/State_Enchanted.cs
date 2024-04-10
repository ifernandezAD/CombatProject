using UnityEngine;

public class State_Enchanted : StateBase
{
    [Header("EnchantmentAnimations")]
    private readonly int enchantmentHash = Animator.StringToHash("EnchantmentType");

    [SerializeField] float enchantedDuration = 10f;
    float currentTime;

    private void OnEnable()
    {
        currentTime = enchantedDuration;
        PlayEnchantedAnimation();
        ai.StopMovement();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            StopEnchantedAnimation();
            ai.RestoreSpeed();
            ai.SetEnchanted(false);
        }
    }

    void PlayEnchantedAnimation()
    {
        if ((ai.senseable.allegiance == "Gangster") || (ai.senseable.allegiance == "Player")) //Player means a skin changed enemy
        {
            int randomAnim = UnityEngine.Random.Range(1, 5);
            ai.animator.SetInteger(enchantmentHash, randomAnim);
        }
        else
        {
            ai.animator.SetInteger(enchantmentHash, 1);
        }
    }

    void StopEnchantedAnimation()
    {
        ai.animator.SetInteger(enchantmentHash, 0);
    }
}
