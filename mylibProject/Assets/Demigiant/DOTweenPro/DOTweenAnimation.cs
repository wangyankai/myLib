// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2015/03/12 15:55

using System;
using System.Collections.Generic;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#if TEXTMESHPRO
	using TMPro;
#endif

#pragma warning disable 1591
namespace DG.Tweening
{
    /// <summary>
    /// Attach this to a GameObject to create a tween
    /// </summary>
    [AddComponentMenu("DOTween/DOTween Animation")]
    public class DOTweenAnimation : ABSDOTweenAnimationComponent
    {
        public float delay;
        public float duration = 1;
        public Ease easeType = Ease.OutQuad;
        public AnimationCurve easeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        public LoopType loopType = LoopType.Restart;
        public int loops = 1;
        public string id = "";
        public bool isRelative;
        public bool isFrom;
        public bool autoKill = true;

        public bool isValid;
        public DOTweenAnimationType animationType;
        public bool autoPlay = true;

        public float endValueFloat;
        public Vector3 endValueV3;
        public Color endValueColor = new Color(1, 1, 1, 1);
        public string endValueString = "";

        public bool optionalBool0;
        public float optionalFloat0;
        public int optionalInt0;
        public RotateMode optionalRotationMode = RotateMode.Fast;
        public ScrambleMode optionalScrambleMode = ScrambleMode.None;
        public string optionalString;

        public bool hasOnStart, hasOnStepComplete, hasOnComplete;
        public UnityEvent onStart, onStepComplete, onComplete;

        Tween _tween;
        int _playCount = -1; // Used when calling DOPlayNext

        #region Unity Methods

        void Awake()
        {
            if (!isValid) return;

            Component c;
            switch (animationType) {
            case DOTweenAnimationType.None:
                break;
            case DOTweenAnimationType.Move:
                c = this.GetComponent<Rigidbody2D>();
                if (c != null) {
                    _tween = ((Rigidbody2D)c).DOMove(endValueV3, duration, optionalBool0);
                    goto SetupTween;
                }
                c = this.GetComponent<Rigidbody>();
                if (c != null) {
                    _tween = ((Rigidbody)c).DOMove(endValueV3, duration, optionalBool0);
                    goto SetupTween;
                }
                _tween = transform.DOMove(endValueV3, duration, optionalBool0);
                break;
            case DOTweenAnimationType.LocalMove:
                _tween = transform.DOLocalMove(endValueV3, duration, optionalBool0);
                break;
            case DOTweenAnimationType.Rotate:
                c = this.GetComponent<Rigidbody2D>();
                if (c != null) {
                    _tween = ((Rigidbody2D)c).DORotate(endValueFloat, duration);
                    goto SetupTween;
                }
                c = this.GetComponent<Rigidbody>();
                if (c != null) {
                    _tween = ((Rigidbody)c).DORotate(endValueV3, duration, optionalRotationMode);
                    goto SetupTween;
                }
                _tween = transform.DORotate(endValueV3, duration, optionalRotationMode);
                break;
            case DOTweenAnimationType.LocalRotate:
                _tween = transform.DOLocalRotate(endValueV3, duration, optionalRotationMode);
                break;
            case DOTweenAnimationType.Scale:
                _tween = transform.DOScale(optionalBool0 ? new Vector3(endValueFloat, endValueFloat, endValueFloat) : endValueV3, duration);
                break;
            case DOTweenAnimationType.Color:
                isRelative = false;
#if TK2D
                c = this.GetComponent<tk2dBaseSprite>();
                if (c != null) {
                   _tween = ((tk2dBaseSprite)c).DOColor(endValueColor, duration);
                    goto SetupTween;
                }
#endif
#if TEXTMESHPRO
                c = this.GetComponent<TextMeshPro>();
                if (c != null) {
                   _tween = ((TextMeshPro)c).DOColor(endValueColor, duration);
                    goto SetupTween;
                }
                c = this.GetComponent<TextMeshProUGUI>();
                if (c != null) {
                   _tween = ((TextMeshProUGUI)c).DOColor(endValueColor, duration);
                    goto SetupTween;
                }
#endif
                c = this.GetComponent<SpriteRenderer>();
                if (c != null) {
                    _tween = ((SpriteRenderer)c).DOColor(endValueColor, duration);
                    goto SetupTween;
                }
                c = this.GetComponent<Renderer>();
                if (c != null) {
                    _tween = ((Renderer)c).material.DOColor(endValueColor, duration);
                    goto SetupTween;
                }
                c = this.GetComponent<Image>();
                if (c != null) {
                    _tween = ((Image)c).DOColor(endValueColor, duration);
                    goto SetupTween;
                }
                c = this.GetComponent<Text>();
                if (c != null) {
                    _tween = ((Text)c).DOColor(endValueColor, duration);
                    goto SetupTween;
                }
                break;
            case DOTweenAnimationType.Fade:
                isRelative = false;
#if TK2D
                c = this.GetComponent<tk2dBaseSprite>();
                if (c != null) {
                   _tween = ((tk2dBaseSprite)c).DOFade(endValueFloat, duration);
                    goto SetupTween;
                }
#endif
#if TEXTMESHPRO
                c = this.GetComponent<TextMeshPro>();
                if (c != null) {
                   _tween = ((TextMeshPro)c).DOFade(endValueFloat, duration);
                    goto SetupTween;
                }
                c = this.GetComponent<TextMeshProUGUI>();
                if (c != null) {
                   _tween = ((TextMeshProUGUI)c).DOFade(endValueFloat, duration);
                    goto SetupTween;
                }
#endif
                c = this.GetComponent<SpriteRenderer>();
                if (c != null) {
                    _tween = ((SpriteRenderer)c).DOFade(endValueFloat, duration);
                    goto SetupTween;
                }
                c = this.GetComponent<Renderer>();
                if (c != null) {
                    _tween = ((Renderer)c).material.DOFade(endValueFloat, duration);
                    goto SetupTween;
                }
                c = this.GetComponent<Image>();
                if (c != null) {
                    _tween = ((Image)c).DOFade(endValueFloat, duration);
                    goto SetupTween;
                }
                c = this.GetComponent<Text>();
                if (c != null) {
                    _tween = ((Text)c).DOFade(endValueFloat, duration);
                    goto SetupTween;
                }
                break;
            case DOTweenAnimationType.Text:
                c = this.GetComponent<Text>();
                if (c != null) {
                    _tween = ((Text)c).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                    goto SetupTween;
                }
#if TK2D
                c = this.GetComponent<tk2dTextMesh>();
                if (c != null) {
                   _tween = ((tk2dTextMesh)c).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                    goto SetupTween;
                }
#endif
#if TEXTMESHPRO
                c = this.GetComponent<TextMeshPro>();
                if (c != null) {
                   _tween = ((TextMeshPro)c).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                    goto SetupTween;
                }
                c = this.GetComponent<TextMeshProUGUI>();
                if (c != null) {
                   _tween = ((TextMeshProUGUI)c).DOText(endValueString, duration, optionalBool0, optionalScrambleMode, optionalString);
                    goto SetupTween;
                }
#endif
                break;
            case DOTweenAnimationType.PunchPosition:
                _tween = transform.DOPunchPosition(endValueV3, duration, optionalInt0, optionalFloat0, optionalBool0);
                break;
            case DOTweenAnimationType.PunchScale:
                _tween = transform.DOPunchScale(endValueV3, duration, optionalInt0, optionalFloat0);
                break;
            case DOTweenAnimationType.PunchRotation:
                _tween = transform.DOPunchRotation(endValueV3, duration, optionalInt0, optionalFloat0);
                break;
            case DOTweenAnimationType.ShakePosition:
                _tween = transform.DOShakePosition(duration, endValueV3, optionalInt0, optionalFloat0, optionalBool0);
                break;
            case DOTweenAnimationType.ShakeScale:
                _tween = transform.DOShakeScale(duration, endValueV3, optionalInt0, optionalFloat0);
                break;
            case DOTweenAnimationType.ShakeRotation:
                _tween = transform.DOShakeRotation(duration, endValueV3, optionalInt0, optionalFloat0);
                break;
            }

        SetupTween:
            if (_tween == null) return;

            if (isFrom) {
                ((Tweener)_tween).From(isRelative);
            } else {
                _tween.SetRelative(isRelative);
            }
            _tween.SetTarget(this.gameObject).SetDelay(delay).SetLoops(loops, loopType).SetAutoKill(autoKill)
                .OnKill(()=> _tween = null);
            if (easeType == Ease.INTERNAL_Custom) _tween.SetEase(easeCurve);
            else _tween.SetEase(easeType);
            if (!string.IsNullOrEmpty(id)) _tween.SetId(id);

            if (hasOnStart) {
                if (onStart != null) _tween.OnStart(onStart.Invoke);
            } else onStart = null;
            if (hasOnStepComplete) {
                if (onStepComplete != null) _tween.OnStepComplete(onStepComplete.Invoke);
            } else onStepComplete = null;
            if (hasOnComplete) {
                if (onComplete != null) _tween.OnComplete(onComplete.Invoke);
            } else onComplete = null;

            if (autoPlay) _tween.Play();
            else _tween.Pause();
        }

        void OnDestroy()
        {
            if (_tween != null && _tween.IsActive()) _tween.Kill();
            _tween = null;
        }

        #endregion

        #region Public Methods
        // These methods are here so they can be called directly via Unity's UGUI event system

        public override void DOPlay()
        {
            DOTween.Play(this.gameObject);
        }
        public void DOPlayById(string id)
        {
            DOTween.Play(this.gameObject, id);
        }
        public void DOPlayAllById(string id)
        {
            DOTween.Play(id);
        }

        public void DOPlayNext()
        {
            DOTweenAnimation[] anims = this.GetComponents<DOTweenAnimation>();
            while (_playCount < anims.Length - 1) {
                _playCount++;
                DOTweenAnimation anim = anims[_playCount];
                if (anim != null && anim._tween != null && !anim._tween.IsPlaying() && !anim._tween.IsComplete()) {
                    anim._tween.Play();
                    break;
                }
            }
        }

        public override void DOPause()
        {
            DOTween.Pause(this.gameObject);
        }

        public override void DOTogglePause()
        {
            DOTween.TogglePause(this.gameObject);
        }

        public override void DORewind()
        {
            DOTween.Rewind(this.gameObject);
        }

        /// <summary>
        /// Restarts the tween
        /// </summary>
        /// <param name="fromHere">If TRUE, re-evaluates the tween's start and end values from its current position.
        /// Set it to TRUE when spawning the same DOTweenAnimation in different positions (like when using a pooling system)</param>
        public override void DORestart(bool fromHere = false)
        {
            if (_tween == null) {
                if (Debugger.logPriority > 1) Debugger.LogNullTween(_tween); return;
            }
            if (fromHere && isRelative) ReEvaluateRelativeTween();
            _tween.Restart();
        }
        public void DORestartById(string id)
        {
            DOTween.Restart(this.gameObject, id);
        }
        public void DORestartAllById(string id)
        {
            DOTween.Restart(id);
        }

        public override void DOComplete()
        {
            DOTween.Complete(this.gameObject);
        }

        public override void DOKill()
        {
            DOTween.Kill(this.gameObject);
            _tween = null;
        }

        #endregion

        #region Private

        // Re-evaluate relative position of path
        void ReEvaluateRelativeTween()
        {
            if (animationType == DOTweenAnimationType.Move) {
                ((Tweener)_tween).ChangeEndValue(transform.position + endValueV3, true);
            } else if (animationType == DOTweenAnimationType.LocalMove) {
                ((Tweener)_tween).ChangeEndValue(transform.localPosition + endValueV3, true);
            }
        }

        #endregion
    }
}