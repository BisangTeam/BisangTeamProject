using Hyunwoo.CoreLib.ModuleSystem;
using UnityEngine;

namespace Hyunwoo.HumanBodySystem
{
    public class EyeModule : AbstractViscera
    {
        protected override void WorkFunction(int index)
        {
            Debug.Log("Eye : " + index);
        }
    }
}