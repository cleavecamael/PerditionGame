public interface IEnemy
{
    //Called during each movement
    void CheckMove();
    void TakeDamage(float damage);
    void Die();
    
}
