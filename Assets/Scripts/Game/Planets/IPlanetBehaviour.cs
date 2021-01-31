public interface IPlanetBehaviour
{
    void Initialize(float cooldown, RocketType rocketType);
    void UpdateCooldown();
    void Shoot();
}
