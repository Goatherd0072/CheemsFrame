using System;
using Cheems;
using Cheems.UI;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(UIWindowBase))]
public class UIPanelTransition : MonoBehaviour
{
    public float moveInDuring  = 1.0f;
    public float moveOutDuring = 0.3f;

    [SerializeField]
    private CanvasGroup _panelCG;

    [SerializeField]
    private RectTransform _panelRectTrans;

    /// <summary>
    /// x,y 为左下角坐标 z,w 为右上角坐标
    /// </summary>
    [SerializeField]
    private Vector4 _outsidePos;

    private Vector2 _originPos;

    public static  float         offset = 50f;
    private static RectTransform _UISystemRectTrans;

    public static RectTransform UISystemRectTrans
    {
        get
        {
            if (_UISystemRectTrans == null)
            {
                _UISystemRectTrans = UISystem.Instance.GetComponent<RectTransform>();
            }

            return _UISystemRectTrans;
        }
    }

    private void Awake()
    {
        _UISystemRectTrans = UISystem.Instance.GetComponent<RectTransform>();
    }

    public void InitData(CanvasGroup cg, RectTransform rect)
    {
        _panelCG = cg;
        _panelRectTrans = rect;

        CalculateOutsidePos();
    }

    public void MoveToOutside(bool isShow, EUITransitionDir dir, Ease ease = Ease.InOutQuint, Action onComplete = null)
    {
        if (DOTween.IsTweening(_panelRectTrans))
        {
            _panelRectTrans.DOKill();
        }

        var targetPos = GetTargetPos(dir);

        if (dir == EUITransitionDir.None)
        {
            _panelRectTrans.anchoredPosition = _originPos;
            _panelCG.SetCanvasGroupActive(isShow);
            onComplete?.Invoke();
            return;
        }

        _panelRectTrans.DOAnchorPos(targetPos, moveOutDuring).SetEase(ease).OnComplete(() =>
        {
            _panelCG.SetCanvasGroupActive(isShow, 0f, (() =>
                                                       {
                                                           onComplete?.Invoke();

                                                           _panelRectTrans.anchoredPosition =
                                                               _originPos;
                                                       }));
        });
    }

    public void MoveIn(bool isShow, EUITransitionDir dir, Ease ease = Ease.Linear, Action onComplete = null)
    {
        if (DOTween.IsTweening(_panelRectTrans))
        {
            _panelRectTrans.DOKill();
        }

        var targetPos = GetTargetPos(dir);
        if (dir == EUITransitionDir.None)
        {
            _panelRectTrans.anchoredPosition = _originPos;
            _panelCG.SetCanvasGroupActive(isShow);
            onComplete?.Invoke();
            return;
        }


        _panelRectTrans.anchoredPosition = targetPos;
        _panelCG.SetCanvasGroupActive(isShow);

        _panelRectTrans.DOAnchorPos(_originPos, moveInDuring).SetEase(ease).OnComplete(() =>
        {
            onComplete?.Invoke();
            // _panelCG.SetCanvasGroupActive(isShow, onComplete: (() => { onComplete?.Invoke(); }));
        });
    }

    private void CalculateOutsidePos()
    {
        _originPos = _panelRectTrans.anchoredPosition;
        Vector2 canvasSize = UISystemRectTrans.sizeDelta;
        _outsidePos = _panelRectTrans.anchoredPosition;

        _outsidePos.x = -canvasSize.x / 2 - _panelRectTrans.sizeDelta.x / 2 - offset;
        _outsidePos.y = -canvasSize.y / 2 - _panelRectTrans.sizeDelta.y / 2 - offset;
        _outsidePos.z = canvasSize.x / 2 + _panelRectTrans.sizeDelta.x / 2 + offset;
        _outsidePos.w = canvasSize.y / 2 + _panelRectTrans.sizeDelta.y / 2 + offset;
    }

    private Vector2 GetTargetPos(EUITransitionDir dir)
    {
        Vector2 targetPos = _originPos;

        if (dir == EUITransitionDir.None)
        {
            return targetPos;
        }

        if (dir == EUITransitionDir.Left || dir == EUITransitionDir.Right)
        {
            targetPos.x = dir == EUITransitionDir.Left ? _outsidePos.x : _outsidePos.z;
        }

        if (dir == EUITransitionDir.Up || dir == EUITransitionDir.Down)
        {
            targetPos.y = dir == EUITransitionDir.Down ? _outsidePos.y : _outsidePos.w;
        }

        return targetPos;
    }
}

public enum EUITransitionDir
{
    None,
    Left,
    Right,
    Up,
    Down
}