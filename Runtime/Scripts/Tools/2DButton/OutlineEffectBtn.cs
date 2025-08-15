// using DG.Tweening;
// using EPOOutline;
// using UnityEngine;
// using UnityEngine.Events;
// using UnityEngine.EventSystems;
//
// namespace Cheems
// {
//     public class OutlineEffectBtn : GameObjectSelectBase
//     {
//         [SerializeField]
//         protected Outlinable _outlinable;
//
//         public UnityEvent onPointerClick;
//
//         protected virtual void Awake()
//         {
//             _outlinable.enabled = false;
//         }
//
//         public override void OnPointerEnter(PointerEventData eventData)
//         {
//             if (!IsInteractable())
//             {
//                 return;
//             }
//
//             base.OnPointerEnter(eventData);
//             _outlinable.enabled = true;
//             _outlinable.OutlineParameters.DODilateShift(0.5f, 0.3f);
//         }
//
//         public override void OnPointerExit(PointerEventData eventData)
//         {
//             base.OnPointerExit(eventData);
//             _outlinable.OutlineParameters.DODilateShift(0f, 0.3f);
//             _outlinable.enabled = false;
//         }
//
//         public override void OnPointerClick(PointerEventData eventData)
//         {
//             if (eventData.button != PointerEventData.InputButton.Left)
//             {
//                 return;
//             }
//
//             if (!IsInteractable())
//             {
//                 return;
//             }
//
//             onPointerClick.Invoke();
//             base.OnPointerClick(eventData);
//             transform.DOScale(0.9f, 0.15f).SetEase(Ease.OutQuad).OnComplete(() =>
//                                                                             {
//                                                                                 transform.DOScale(1f, 0.15f)
//                                                                                     .SetEase(Ease.InQuad);
//                                                                             });
//         }
//     }
// }