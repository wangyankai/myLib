// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2015/03/12 16:03

using System;
using System.Collections.Generic;
using System.IO;
using DG.DOTweenEditor.Core;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

#if TEXTMESHPRO
    using TMPro;
#endif

namespace DG.DOTweenEditor
{
    [CustomEditor(typeof(DOTweenAnimation))]
    public class DOTweenAnimationInspector : Editor
    {
        static internal readonly Dictionary<DOTweenAnimationType, Type[]> AnimationTypeToComponent = new Dictionary<DOTweenAnimationType, Type[]>() {
            { DOTweenAnimationType.Move, new[] { typeof(Rigidbody), typeof(Rigidbody2D), typeof(Transform) } },
            { DOTweenAnimationType.LocalMove, new[] { typeof(Transform) } },
            { DOTweenAnimationType.Rotate, new[] { typeof(Rigidbody), typeof(Rigidbody2D), typeof(Transform) } },
            { DOTweenAnimationType.LocalRotate, new[] { typeof(Transform) } },
            { DOTweenAnimationType.Scale, new[] { typeof(Transform) } },
            { DOTweenAnimationType.Color, new[] { typeof(SpriteRenderer), typeof(Renderer), typeof(Image), typeof(Text) } },
            { DOTweenAnimationType.Fade, new[] { typeof(SpriteRenderer), typeof(Renderer), typeof(Image), typeof(Text) } },
            { DOTweenAnimationType.Text, new[] { typeof(Text) } },
            { DOTweenAnimationType.PunchPosition, new[] { typeof(Transform) } },
            { DOTweenAnimationType.PunchRotation, new[] { typeof(Transform) } },
            { DOTweenAnimationType.PunchScale, new[] { typeof(Transform) } },
            { DOTweenAnimationType.ShakePosition, new[] { typeof(Transform) } },
            { DOTweenAnimationType.ShakeRotation, new[] { typeof(Transform) } },
            { DOTweenAnimationType.ShakeScale, new[] { typeof(Transform) } }
        };

#if TK2D
        static internal readonly Dictionary<DOTweenAnimationType, Type[]> Tk2dAnimationTypeToComponent = new Dictionary<DOTweenAnimationType, Type[]>() {
            { DOTweenAnimationType.Color, new[] { typeof(tk2dBaseSprite), typeof(tk2dTextMesh) } },
            { DOTweenAnimationType.Fade, new[] { typeof(tk2dBaseSprite), typeof(tk2dTextMesh) } },
            { DOTweenAnimationType.Text, new[] { typeof(tk2dTextMesh) } }
        };
#endif
#if TEXTMESHPRO
        static internal readonly Dictionary<DOTweenAnimationType, Type[]> TMPAnimationTypeToComponent = new Dictionary<DOTweenAnimationType, Type[]>() {
            { DOTweenAnimationType.Color, new[] { typeof(TextMeshPro), typeof(TextMeshProUGUI) } },
            { DOTweenAnimationType.Fade, new[] { typeof(TextMeshPro), typeof(TextMeshProUGUI) } },
            { DOTweenAnimationType.Text, new[] { typeof(TextMeshPro), typeof(TextMeshProUGUI) } }
        };
#endif

        DOTweenAnimation _src;
        SerializedProperty _onStartProperty, _onStepCompleteProperty, _onCompleteProperty;

        // ===================================================================================
        // MONOBEHAVIOUR METHODS -------------------------------------------------------------

        void OnEnable()
        {
            _src = target as DOTweenAnimation;

            _onStartProperty = base.serializedObject.FindProperty("onStart");
            _onStepCompleteProperty = base.serializedObject.FindProperty("onStepComplete");
            _onCompleteProperty = base.serializedObject.FindProperty("onComplete");
        }

        override public void OnInspectorGUI()
        {
            EditorGUIUtils.SetGUIStyles();

            GUILayout.BeginHorizontal();
            EditorGUIUtils.InspectorLogo();
            GUILayout.Label(_src.animationType.ToString().ToUpper() + (string.IsNullOrEmpty(_src.id) ? "" : " [" + _src.id + "]"), EditorGUIUtils.sideLogoIconBoldLabelStyle);
            // Up-down buttons
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("▲", EditorGUIUtils.btIconStyle)) UnityEditorInternal.ComponentUtility.MoveComponentUp(_src);
            if (GUILayout.Button("▼", EditorGUIUtils.btIconStyle)) UnityEditorInternal.ComponentUtility.MoveComponentDown(_src);
            GUILayout.EndHorizontal();

            if (Application.isPlaying) {
                GUILayout.Space(8);
                GUILayout.Label("Animation Editor disabled while in play mode", EditorGUIUtils.wordWrapLabelStyle);
                GUILayout.Space(4);
                GUILayout.Label("NOTE: when using DOPlayNext, the sequence is determined by the DOTweenAnimation Components order in the target GameObject's Inspector", EditorGUIUtils.wordWrapLabelStyle);
                GUILayout.Space(10);
                return;
            }

            Undo.RecordObject(_src, "DOTween Animation");

            _src.isValid = Validate();

            EditorGUIUtility.labelWidth = 120;

            bool hasManager = _src.GetComponent<DOTweenVisualManager>() != null;
            if (!hasManager) {
                if (GUILayout.Button(new GUIContent("Add Manager", "Adds a manager component which allows you to choose additional options for this gameObject"))) {
                    _src.gameObject.AddComponent<DOTweenVisualManager>();
                }
            }

            GUILayout.BeginHorizontal();
                DOTweenAnimationType prevAnimType = _src.animationType;
                _src.animationType = (DOTweenAnimationType)EditorGUILayout.EnumPopup(_src.animationType, EditorGUIUtils.popupButton);
                _src.autoPlay = EditorGUIUtils.ToggleButton(_src.autoPlay, new GUIContent("AutoPlay", "If selected, the tween will play automatically"));
                _src.autoKill = EditorGUIUtils.ToggleButton(_src.autoKill, new GUIContent("AutoKill", "If selected, the tween will be killed when it completes, and won't be reusable"));
            GUILayout.EndHorizontal();
            if (prevAnimType != _src.animationType) {
                // Set default optional values based on animation type
                switch (_src.animationType) {
                case DOTweenAnimationType.Move:
                case DOTweenAnimationType.LocalMove:
                case DOTweenAnimationType.Rotate:
                case DOTweenAnimationType.LocalRotate:
                case DOTweenAnimationType.Scale:
                    _src.endValueV3 = Vector3.zero;
                    _src.endValueFloat = 0;
                    _src.optionalBool0 = true;
                    break;
                case DOTweenAnimationType.Color:
                case DOTweenAnimationType.Fade:
                    _src.endValueFloat = 0;
                    break;
                case DOTweenAnimationType.Text:
                    _src.optionalBool0 = true;
                    break;
                case DOTweenAnimationType.PunchPosition:
                case DOTweenAnimationType.PunchRotation:
                case DOTweenAnimationType.PunchScale:
                    _src.endValueV3 = _src.animationType == DOTweenAnimationType.PunchRotation ? new Vector3(0,180,0) : Vector3.one;
                    _src.optionalFloat0 = 1;
                    _src.optionalInt0 = 10;
                    _src.optionalBool0 = false;
                    break;
                case DOTweenAnimationType.ShakePosition:
                case DOTweenAnimationType.ShakeRotation:
                case DOTweenAnimationType.ShakeScale:
                    _src.endValueV3 = _src.animationType == DOTweenAnimationType.ShakeRotation ? new Vector3(90,90,90) : Vector3.one;
                    _src.optionalInt0 = 10;
                    _src.optionalFloat0 = 90;
                    _src.optionalBool0 = false;
                    break;
                }
            }
            if (_src.animationType == DOTweenAnimationType.None) return;

            if (!_src.isValid) {
                GUI.color = Color.red;
                GUILayout.BeginVertical(GUI.skin.box);
                GUILayout.Label("No valid Component was found for the selected animation", EditorGUIUtils.wordWrapLabelStyle);
                GUILayout.EndVertical();
                GUI.color = Color.white;
                return;
            }

            _src.duration = EditorGUILayout.FloatField("Duration", _src.duration);
            if (_src.duration < 0) _src.duration = 0;
            _src.delay = EditorGUILayout.FloatField("Delay", _src.delay);
            if (_src.delay < 0) _src.delay = 0;
            _src.easeType = EditorGUIUtils.FilteredEasePopup(_src.easeType);
            if (_src.easeType == Ease.INTERNAL_Custom) {
                _src.easeCurve = EditorGUILayout.CurveField("   Ease Curve", _src.easeCurve);
            }
            _src.loops = EditorGUILayout.IntField(new GUIContent("Loops", "Set to -1 for infinite loops"), _src.loops);
            if (_src.loops < -1) _src.loops = -1;
            if (_src.loops > 1 || _src.loops == -1)
                _src.loopType = (LoopType)EditorGUILayout.EnumPopup("   Loop Type", _src.loopType);
            _src.id = EditorGUILayout.TextField("ID", _src.id);

            bool canBeRelative = true;
            // End value and eventual specific options
            switch (_src.animationType) {
            case DOTweenAnimationType.Move:
            case DOTweenAnimationType.LocalMove:
                GUIEndValueV3();
                _src.optionalBool0 = EditorGUILayout.Toggle("    Snapping", _src.optionalBool0);
                break;
            case DOTweenAnimationType.Rotate:
            case DOTweenAnimationType.LocalRotate:
                if (_src.GetComponent<Rigidbody2D>()) GUIEndValueFloat();
                else {
                    GUIEndValueV3();
                    _src.optionalRotationMode = (RotateMode)EditorGUILayout.EnumPopup("    Rotation Mode", _src.optionalRotationMode);
                }
                break;
            case DOTweenAnimationType.Scale:
                if (_src.optionalBool0) GUIEndValueFloat();
                else GUIEndValueV3();
                _src.optionalBool0 = EditorGUILayout.Toggle("Uniform Scale", _src.optionalBool0);
                break;
            case DOTweenAnimationType.Color:
                GUIEndValueColor();
                canBeRelative = false;
                break;
            case DOTweenAnimationType.Fade:
                GUIEndValueFloat();
                if (_src.endValueFloat < 0) _src.endValueFloat = 0;
                if (_src.endValueFloat > 1) _src.endValueFloat = 1;
                canBeRelative = false;
                break;
            case DOTweenAnimationType.Text:
                GUIEndValueString();
                _src.optionalBool0 = EditorGUILayout.Toggle("Rich Text Enabled", _src.optionalBool0);
                _src.optionalScrambleMode = (ScrambleMode)EditorGUILayout.EnumPopup("Scramble Mode", _src.optionalScrambleMode);
                _src.optionalString = EditorGUILayout.TextField(new GUIContent("Custom Scramble", "Custom characters to use in case of ScrambleMode.Custom"), _src.optionalString);
                break;
            case DOTweenAnimationType.PunchPosition:
            case DOTweenAnimationType.PunchRotation:
            case DOTweenAnimationType.PunchScale:
                GUIEndValueV3();
                canBeRelative = false;
                _src.optionalInt0 = EditorGUILayout.IntSlider(new GUIContent("    Vibrato", "How much will the punch vibrate"), _src.optionalInt0, 1, 50);
                _src.optionalFloat0 = EditorGUILayout.Slider(new GUIContent("    Elasticity", "How much the vector will go beyond the starting position when bouncing backwards"), _src.optionalFloat0, 0, 1);
                if (_src.animationType == DOTweenAnimationType.PunchPosition) _src.optionalBool0 = EditorGUILayout.Toggle("    Snapping", _src.optionalBool0);
                break;
            case DOTweenAnimationType.ShakePosition:
            case DOTweenAnimationType.ShakeRotation:
            case DOTweenAnimationType.ShakeScale:
                GUIEndValueV3();
                canBeRelative = false;
                _src.optionalInt0 = EditorGUILayout.IntSlider(new GUIContent("    Vibrato", "How much will the shake vibrate"), _src.optionalInt0, 1, 50);
                _src.optionalFloat0 = EditorGUILayout.Slider(new GUIContent("    Randomness", "The shake randomness"), _src.optionalFloat0, 0, 90);
                if (_src.animationType == DOTweenAnimationType.ShakePosition) _src.optionalBool0 = EditorGUILayout.Toggle("    Snapping", _src.optionalBool0);
                break;
            }

            // Final settings
            if (canBeRelative) _src.isRelative = EditorGUILayout.Toggle("    Relative", _src.isRelative);

            // Events
            GUILayout.Space(4);
            GUILayout.Label("EVENTS", EditorGUIUtils.boldLabelStyle);
            GUILayout.BeginHorizontal();
            _src.hasOnStart = EditorGUIUtils.ToggleButton(_src.hasOnStart, new GUIContent("OnStart", "Event called the first time the tween starts, after any eventual delay"));
            _src.hasOnStepComplete = EditorGUIUtils.ToggleButton(_src.hasOnStepComplete, new GUIContent("OnStepComplete", "Event called at the end of each loop"));
            _src.hasOnComplete = EditorGUIUtils.ToggleButton(_src.hasOnComplete, new GUIContent("OnComplete", "Event called at the end of the tween, all loops included"));
            GUILayout.EndHorizontal();
            base.serializedObject.Update();
            if (_src.hasOnStart) EditorGUILayout.PropertyField(_onStartProperty, new GUILayoutOption[0]);
            if (_src.hasOnStepComplete) EditorGUILayout.PropertyField(_onStepCompleteProperty, new GUILayoutOption[0]);
            if (_src.hasOnComplete) EditorGUILayout.PropertyField(_onCompleteProperty, new GUILayoutOption[0]);
            base.serializedObject.ApplyModifiedProperties();
        }

        // Checks if a Component that can be animated with the given animationType is attached to the src
        bool Validate()
        {
            // First check for regular stuff
            if (AnimationTypeToComponent.ContainsKey(_src.animationType)) {
                foreach (Type t in AnimationTypeToComponent[_src.animationType]) {
                    if (_src.GetComponent(t) != null) return true;
                }
            }
            // Then check for external plugins
#if TK2D
            if (Tk2dAnimationTypeToComponent.ContainsKey(_src.animationType)) {
                foreach (Type t in Tk2dAnimationTypeToComponent[_src.animationType]) {
                    if (_src.GetComponent(t) != null) return true;
                }
            }
#endif
#if TEXTMESHPRO
            if (TMPAnimationTypeToComponent.ContainsKey(_src.animationType)) {
                foreach (Type t in TMPAnimationTypeToComponent[_src.animationType]) {
                    if (_src.GetComponent(t) != null) return true;
                }
            }
#endif
            return false;
        }

        void GUIEndValueFloat()
        {
            GUILayout.BeginHorizontal();
            GUIToFromButton();
            _src.endValueFloat = EditorGUILayout.FloatField(_src.endValueFloat);
            GUILayout.EndHorizontal();
        }

        void GUIEndValueColor()
        {
            GUILayout.BeginHorizontal();
            GUIToFromButton();
            _src.endValueColor = EditorGUILayout.ColorField(_src.endValueColor);
            GUILayout.EndHorizontal();
        }

        void GUIEndValueV3()
        {
            GUILayout.BeginHorizontal();
            GUIToFromButton();
            _src.endValueV3 = EditorGUILayout.Vector3Field("", _src.endValueV3, GUILayout.Height(16));
            GUILayout.EndHorizontal();
        }

        void GUIEndValueString()
        {
            GUILayout.BeginHorizontal();
            GUIToFromButton();
            _src.endValueString = EditorGUILayout.TextArea(_src.endValueString, EditorGUIUtils.wordWrapTextArea);
            GUILayout.EndHorizontal();
        }

        void GUIToFromButton()
        {
            if (GUILayout.Button(_src.isFrom ? "FROM" : "TO", EditorGUIUtils.sideBtStyle, GUILayout.Width(100))) _src.isFrom = !_src.isFrom;
            GUILayout.Space(16);
        }
    }
}