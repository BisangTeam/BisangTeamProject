using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hyunwoo.HumanBodySystem
{
    public class HeartModule : MonoBehaviour
    {
        [SerializeField] private EyeModule eyeModule;
        [SerializeField] private EarModule earModule;
        
        // 심박수 맞춰 혈액 생산
        public float GetBloodValue { get; set; }

        public void Update()
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
                eyeModule.ConnectionBlood(this);
            
            if (Keyboard.current.wKey.wasPressedThisFrame)
                earModule.ConnectionBlood(this);
        }
    }
}