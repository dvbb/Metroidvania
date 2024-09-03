using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Major stats")]
    public Stat strength;
    public Stat agility;
    public Stat intelligence;
    public Stat vitality;

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower ;

    public int currentHealth;

    public System.Action onHealthChanged;

    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
        critPower.SetDefaultValue(150);
    }

    public virtual void DoDamage(CharacterStats targetStats)
    {
        if (CanAvoidAttack(targetStats))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();
        targetStats.TakeDamage(totalDamage);
    }

    private bool CanAvoidAttack(CharacterStats targetStats)
    {
        int totalEvasion = targetStats.evasion.GetValue() + targetStats.agility.GetValue();
        if (Random.Range(0, 100) < totalEvasion)
        {
            Debug.Log("avoid this attack");
            return true;
        }
        return false;
    }

    public virtual void TakeDamage(int _damage)
    {
        DecreaseHealthBy(_damage);
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;
        if(onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void Die()
    {
    }
}
