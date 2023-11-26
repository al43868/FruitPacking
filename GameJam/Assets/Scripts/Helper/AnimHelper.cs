#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

public class AnimHelper : OdinMenuEditorWindow
{
    protected override OdinMenuTree BuildMenuTree()
    {
        OdinMenuTree tree = new(supportsMultiSelect: true)
        {
            { "Home", this, EditorIcons.House },
        };
        return tree;
    }
    [MenuItem("Tools/UnitAnimHelper")]
    private static void OpenWindow()
    {
        var window = GetWindow<AnimHelper>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
    }
    [ReadOnly]
    public AnimationClip baseIdle, baseAtk, baseHurt,baseDead,baseMove;// ԭʼ��������
    [ReadOnly]
    public AnimatorController animatorController;
    [ReadOnly]
    public List<Sprite> newSprites;
    [ReadOnly]
    public string animatorName;
    [ReadOnly]
    public List<KeyValuePair<AnimationClip, AnimationClip>> newclips;
    [ReadOnly]
    public Texture2D texture;
    public string folderName;
    public void GetBaseAnim()
    {
        baseAtk= AssetDatabase.LoadAssetAtPath("Assets/Anim/BaseUnit/Atk.anim",
           typeof(AnimationClip)) as AnimationClip;
        baseIdle = AssetDatabase.LoadAssetAtPath("Assets/Anim/BaseUnit/Idle.anim",
           typeof(AnimationClip)) as AnimationClip;
        baseDead = AssetDatabase.LoadAssetAtPath("Assets/Anim/BaseUnit/Dead.anim",
            typeof(AnimationClip)) as AnimationClip;
        baseHurt = AssetDatabase.LoadAssetAtPath("Assets/Anim/BaseUnit/Hurt.anim",
           typeof(AnimationClip)) as AnimationClip;
        baseMove = AssetDatabase.LoadAssetAtPath("Assets/Anim/BaseUnit/Move.anim",
          typeof(AnimationClip)) as AnimationClip;
        animatorController = AssetDatabase.LoadAssetAtPath("Assets/Anim/BaseUnit/BaseUnit.controller",
            typeof(AnimatorController)) as AnimatorController;
    }
    [Button(ButtonHeight = 30)]
    public void CopyTowerAnim(Texture2D texture)
    {
        this.texture = texture;
        GetBaseAnim();
        GetSprites();
        animatorName = texture.name;
        if (!AssetDatabase.IsValidFolder("Assets/Anim/Unit/" + folderName))
        {
            AssetDatabase.CreateFolder("Assets/Anim/Unit", folderName);
        }
        newclips=new();
        newclips.Add(AnimationClipCopier(baseIdle, 0));
        newclips.Add(AnimationClipCopier(baseAtk, 4));
        newclips.Add(AnimationClipCopier(baseHurt, 7));
        newclips.Add(AnimationClipCopier(baseDead, 9));
        newclips.Add(AnimationClipCopier(baseMove, 0));

        CreatAnimatorController();
    }
    public void CreatAnimatorController()
    {
        AnimatorOverrideController controller = new (animatorController);
        controller.name=texture.name;
        controller.ApplyOverrides(newclips);

        AssetDatabase.CreateAsset(controller, "Assets/Anim/Unit/" + folderName + "/" + animatorName + ".controller");
        AssetDatabase.SaveAssets();
    }
    public void GetSprites()
    {
        newSprites = new();
        Object[] objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(texture));
        Sprite[] sprites = Array.ConvertAll(objects, obj => obj as Sprite);
        sprites = sprites.Where(val => val != null).ToArray();
        // ʹ��Array.Sort������������������
        Array.Sort(sprites, (a, b) => CompareNumbers(a.name, b.name));
        foreach (var item in sprites)
        {
            newSprites.Add(item);
        }
        int CompareNumbers(string a, string b)
        {
            // ʹ��������ʽ��ƥ������
            Regex regex = new(@"\d+");
            // ��ȡa��b�еĵ�һ������
            int numA = int.Parse(regex.Match(a).Value);
            int numB = int.Parse(regex.Match(b).Value);
            // �Ƚ����ֵĴ�С
            return numA - numB;
        }
    }
    public KeyValuePair<AnimationClip, AnimationClip> AnimationClipCopier(AnimationClip baseClip, int startIndex)
    {
        // �����µĶ�������
        AnimationClip newClip = new();
        newClip.name = baseClip.name;
        // ����ԭʼ������������������
        EditorUtility.CopySerialized(baseClip, newClip);
        // ��ȡ AnimationClip �е����йؼ�֡
        //AnimationUtility.GetCurveBindings ����ֻ�ܻ�ȡ float���� �İ󶨣������ܻ�ȡ ObjectReference���� �İ�
        //EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(newClip);
        EditorCurveBinding[] curveBindings = AnimationUtility.GetObjectReferenceCurveBindings(newClip);
        // ����ÿ���ؼ�֡
        foreach (EditorCurveBinding curveBinding in curveBindings)
        {
            // ���ؼ�֡�Ƿ�Ϊ Sprite ����
            if (curveBinding.type == typeof(SpriteRenderer))
            {
                // ��ȡ�ؼ�֡����Ӧ�Ķ���
                ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve
                    (newClip, curveBinding);
                for (int i = 0; i < keyframes.Length; i++)
                {
                    // ��ȡ�ؼ�֡�� sprite
                    Sprite sprite = keyframes[i].value as Sprite;
                    if (sprite != null)
                    {
                        // �����µ� sprite
                        keyframes[i].value = newSprites[startIndex + i];
                    }
                }
                AnimationUtility.SetObjectReferenceCurve(newClip, curveBinding, keyframes);
            }
        }
        AssetDatabase.CreateAsset(newClip, "Assets/Anim/Unit/" + folderName + "/" + baseClip.name + ".anim");
        AssetDatabase.SaveAssets();
        return new KeyValuePair<AnimationClip, AnimationClip>(baseClip, newClip);
    }
    //���� ����AnimatorController�ķ������ᵼ��animationclip������ͬ
    //AnimatorController animator = new();
    //animator.name = texture.name;
    //EditorUtility.CopySerialized(animatorController, animator);
    //// ʹ��AnimatorController.layers��������ȡ���ƺ��animatorController�����в㼶
    //AnimatorControllerLayer[] layers = animator.layers;
    //// ����ÿ���㼶
    //foreach (AnimatorControllerLayer layer in layers)
    //{
    //    // ʹ��AnimatorControllerLayer.stateMachine��������ȡÿ���㼶��״̬��
    //    AnimatorStateMachine stateMachine = layer.stateMachine;
    //    // ʹ��AnimatorStateMachine.states��������ȡÿ��״̬��������״̬
    //    ChildAnimatorState[] states = stateMachine.states;
    //    // ����ÿ��״̬
    //    foreach (ChildAnimatorState state in states)
    //    {
    //        // ʹ��AnimatorState.motion��������ȡ������ÿ��״̬�Ķ�������
    //        Motion motion = state.state.motion;
    //        // �ж�motion�Ƿ���AnimationClip����
    //        if (motion is AnimationClip)
    //        {
    //            // ǿ��ת��ΪAnimationClip����
    //            AnimationClip clip = motion as AnimationClip;
    //            // �ж�clip�Ƿ�������Ҫ�滻�Ķ�����������������Ը������ֻ��������������ж�
    //            var listClip = newclips.Where((x) => { return x.name == clip.name; });
    //            if (listClip != null)
    //            {
    //                // ���¶���������ֵ����Ӧ��״̬��motion����
    //                state.state.motion = listClip.ToArray()[0];
    //            }
    //        }
    //    }
    //}
}
#endif