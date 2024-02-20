using StatusSystem;

namespace DamageSystem.Locator
{
    public interface IStatusParameterLocator
    {
        IStatusParameter[] TryGetStatus(); 
    }
}
