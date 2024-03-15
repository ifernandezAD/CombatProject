using UnityEngine;

public class State_Enchanted : StateBase
{
    [Header("EnchantmentAnimations")]
    private readonly int gangsterEnchantmentHash = Animator.StringToHash("EnchantmentType");

    [SerializeField] float enchantedDuration = 10f;
    float currentTime;

    private void OnEnable()
    {
        currentTime = enchantedDuration;
        PlayEnchantedAnimation();
        ai.StopEntity(true);
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            StopEnchantedAnimation();
            ai.StopEntity(false);
            ai.SetEnchanted(false);
        }
    }

    void PlayEnchantedAnimation()
    {
        if (ai.senseable.allegiance == "Gangster")
        {
            int randomAnim = UnityEngine.Random.Range(1, 6);
            ai.animator.SetInteger(gangsterEnchantmentHash, randomAnim);
        }
    }

    void StopEnchantedAnimation()
    {
        if (ai.senseable.allegiance == "Gangster")
        {
            ai.animator.SetInteger(gangsterEnchantmentHash, 0);
        }
    }
}
