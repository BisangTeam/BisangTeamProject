using System.Reflection;
using UnityEngine;

namespace Hyunwoo.HumanBodySystem
{
    public abstract class AbstractHumanBody : MonoBehaviour
    {
        private float _bloodGauge;
        
        protected float BloodGauge
        {
            get => _bloodGauge;
            set => _bloodGauge = Mathf.Clamp(value, 0f, 100f);
        } 
        // 나중에 프로퍼티로 만들어서 최솟값, 최댓값 제한하기

        public abstract void ConnectionBlood();

        public abstract void DisConnectionBlood();
    }
}