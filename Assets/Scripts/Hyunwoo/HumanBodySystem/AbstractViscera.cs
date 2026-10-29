using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Hyunwoo.CoreLib;
using Hyunwoo.CoreLib.ModuleSystem;
using UnityEngine;

namespace Hyunwoo.HumanBodySystem
{
    public abstract class AbstractViscera : AbstractHumanBody, IModule, IAfterInitModule
    {
        [SerializeField] protected float updateTime = 0.5f;
        [SerializeField] protected float reduceGauge = 1f;
        
        [SerializeField] protected List<float> wantGaugeList;
        
        protected ModuleOwner _owner;
        protected HeartModule _heartModule;

        public void InsertBlood(float value)
        {
            BloodGauge += value;
        }

        public void PullOut(float value, out float blood)
        {
            blood = Mathf.Clamp(BloodGauge - value, 0, BloodGauge);
            
            BloodGauge = blood;
        }
        
        private void Start()
        {
            UpdateReduceGauge().Forget();
        }
        
        private async UniTask UpdateReduceGauge()
        {
            ReduceGauge();
            await UniTask.Delay((int)(updateTime * 1000));
            UpdateReduceGauge().Forget();
        }

        private void ReduceGauge()
        {
            BloodGauge -= reduceGauge;
            //Debug.Log("감소 중입니다. 현재 게이지 : " +  Gauge);
        }

        private void OnHandleFillGauge(float value)
        {
            BloodGauge += value;
            Debug.Log($"{gameObject.name}: Gauge: {BloodGauge}");
        }
        
        #region Implementation
        
        public void Work()
        {
            for (int i = 0; i < wantGaugeList.Count; i++)
            {
                if (BloodGauge <= wantGaugeList[i])
                {
                    WorkFunction(i);
                    return;
                }
            }
            
            // 리스트에 있는 값보다 높은 값이 들어왔을때 예외 처리
            WorkFunction(wantGaugeList.Count);
        }
        
        protected abstract void WorkFunction(int index);

        public void Initialize(ModuleOwner owner)
        {
            _owner = owner;
        }
        
        public void AfterInit()
        {
            _heartModule = _owner.GetModule<HeartModule>();
        }

        public override void ConnectionBlood()
        {
            _heartModule.OnHeartRate += OnHandleFillGauge;
            _heartModule.ConnectCount++;
        }

        public override void DisConnectionBlood()
        {
            _heartModule.OnHeartRate -= OnHandleFillGauge;            
            _heartModule.ConnectCount--;
        }
        #endregion
    }
}