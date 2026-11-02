using Hyunwoo.CoreLib.ModuleSystem;
using UnityEngine;

namespace Hyunwoo.HumanBodySystem
{
    public class EyeModule : AbstractHumanBody, IModule
    {
        [SerializeField] private float wantGauge;
        
        public void Initialize(ModuleOwner owner)
        {
            
        }

        protected override void Work()
        {
            // 게이지가 작으면 return
            if (Gauge <= wantGauge) return;
            
            // 코드부 구현
        }
    }
}