using UnityEngine;
using UnityEngine.EventSystems;

namespace Hyunwoo.BloodPack
{
    public class MouseDragAndMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // Begin : 시작했음을 알림
        // Drag : 위치만 계속해서 반환 + 현재 위치가 기관이면 확대
        // End : 끝났음을 알림 + 현재 위치가 기관이면 사라지고 AbstractHumanBody.ConnectionBlood 실행시켜주기
        
        public void OnBeginDrag(PointerEventData eventData)
        {
                        
        }

        public void OnDrag(PointerEventData eventData)
        {
            //eventData.position;
            //eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }
    }
}
