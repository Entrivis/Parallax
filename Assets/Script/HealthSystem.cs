
public class HealthSystem
{
    public float healthMax;
    private float damage;
    public float currentHealth;
    public HealthSystem(float healthMax){
        this.healthMax = healthMax;
        currentHealth = healthMax;
    }
    public void Damage(int damageAmount){
        currentHealth -= damageAmount;
        if(currentHealth < 0){
            currentHealth = 0;
        }
    }
    public float GetHealthBar(){
        return currentHealth/healthMax;
    }
}
