namespace Hyunwoo.CoreLib.ModuleSystem
{
    public interface IModule
    {
        void Initialize(ModuleOwner owner);
        void Work();
        
    }
}