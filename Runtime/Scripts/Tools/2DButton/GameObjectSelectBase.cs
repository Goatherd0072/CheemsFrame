using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Cheems
{
    public abstract class GameObjectSelectBase : MonoBehaviour,
                                                 IPointerEnterHandler, IPointerExitHandler,
                                                 IPointerDownHandler, IPointerUpHandler, IPointerClickHandler,
                                                 ISelectHandler, IDeselectHandler,
                                                 IMoveHandler
    {
        [FormerlySerializedAs("inInteractable")]
        public bool isInteractable = true;


        public virtual bool IsInteractable()
        {
            return isInteractable;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsInteractable())
            {
                return;
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (!IsInteractable())
            {
                return;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInteractable())
            {
                return;
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (!IsInteractable())
            {
                return;
            }
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!IsInteractable())
            {
                return;
            }
        }

        public virtual void OnSelect(BaseEventData eventData)
        {
            if (!IsInteractable())
            {
                return;
            }
        }

        public virtual void OnDeselect(BaseEventData eventData)
        {
            if (!IsInteractable())
            {
                return;
            }
        }

        public virtual void OnMove(AxisEventData eventData)
        {
            if (!IsInteractable())
            {
                return;
            }
        }
    }
}