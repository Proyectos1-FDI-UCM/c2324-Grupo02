using StatusSystem;

namespace DamageSystem.Damager
{
    public interface IDamager
    {
        bool TryDamage(IStatusParameter status);
        
    }
}