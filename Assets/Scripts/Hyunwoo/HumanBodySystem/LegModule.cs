using Hyunwoo.CoreLib.ModuleSystem;
using UnityEngine;

namespace Hyunwoo.HumanBodySystem
{
    public class LegModule : AbstractHumanBody, IModule
    {
        [SerializeField] private float wantGauge;
        
        protected override void Work()
        {
            // 게이지가 작으면 return
            if (Gauge <= wantGauge) return;
            
            // 코드부 구현
        }

        public void Initialize(ModuleOwner owner)
        {
        }
    }
}