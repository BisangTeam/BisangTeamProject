using UnityEngine;

namespace Hyunwoo.HumanBodySystem
{
    public abstract class AbstractHumanBody : MonoBehaviour
    {
        // 10/13
        // 1. 장기는 계속해서 능력이 수행되기 때문에 UpdateState 만들기
        // 2. Gauge를 채워놓는 그 로직 짜기
        
        protected float Gauge; // 나중에 프로퍼티로 만들어서 최솟값, 최댓값 제한하기
        protected float UpdateFillGauge;
        
        public void SetUpdateFillGauge(float setUpdateFillGauge)
        {
            UpdateFillGauge = setUpdateFillGauge;
        }

        public virtual void UpdateState()
        {
            FillGauge(); 
            Work();
        }

        protected abstract void Work();

        private void FillGauge() // 나중에 async로 바꿔보기
        {
            // 시간 텀을 둬야 되는데..
            Gauge += UpdateFillGauge;
        }
    }
}