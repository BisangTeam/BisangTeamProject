using System;
using Hyunwoo.CoreLib;
using Hyunwoo.CoreLib.ModuleSystem;
using Hyunwoo.HumanBodySystem;
using UnityEngine;

namespace Hyunwoo.Player
{
    public class Player : ModuleOwner
    {
        [SerializeField] private PlayerInputSO playerInput;

        private IModule _eyeModule;
        private IModule _earModule;

        protected override void Awake()
        {
            base.Awake();
            _eyeModule = GetModule<EyeModule>();
            _earModule = GetModule<EarModule>();
        }

        private void OnEnable()
        {
            playerInput.OnUpKeyPressed += OnHandleUp;
            playerInput.OnLeftKeyPressed += OnHandleLeft;
        }

        private void Update()
        {
            foreach (var moduleDictValue in _moduleDict.Values)
            {
                moduleDictValue.Work();
            }
        }

        private void OnHandleLeft()
        {
            //(EyeModule)_eyeModule.
        }

        private void OnHandleUp()
        {
            
        }
    }
}