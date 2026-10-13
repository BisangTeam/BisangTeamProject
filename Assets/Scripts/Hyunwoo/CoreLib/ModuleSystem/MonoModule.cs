using UnityEngine;

namespace Hyunwoo.CoreLib.ModuleSystem
{
    public abstract class MonoModule : MonoBehaviour, IModule
    {
        public ModuleOwner Owner { get; private set; }

        public virtual void Initialize(ModuleOwner owner)
        {
            Owner = owner;
        }
    }
}