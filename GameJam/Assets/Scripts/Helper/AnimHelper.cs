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
    public AnimationClip baseIdle, baseAtk, baseHurt,baseDead,baseMove;// 原始动画剪辑
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
        // 使用Array.Sort方法来按照名字排序
        Array.Sort(sprites, (a, b) => CompareNumbers(a.name, b.name));
        foreach (var item in sprites)
        {
            newSprites.Add(item);
        }
        int CompareNumbers(string a, string b)
        {
            // 使用正则表达式来匹配数字
            Regex regex = new(@"\d+");
            // 获取a和b中的第一个数字
            int numA = int.Parse(regex.Match(a).Value);
            int numB = int.Parse(regex.Match(b).Value);
            // 比较数字的大小
            return numA - numB;
        }
    }
    public KeyValuePair<AnimationClip, AnimationClip> AnimationClipCopier(AnimationClip baseClip, int startIndex)
    {
        // 创建新的动画剪辑
        AnimationClip newClip = new();
        newClip.name = baseClip.name;
        // 复制原始动画剪辑的所有属性
        EditorUtility.CopySerialized(baseClip, newClip);
        // 获取 AnimationClip 中的所有关键帧
        //AnimationUtility.GetCurveBindings 方法只能获取 float曲线 的绑定，而不能获取 ObjectReference曲线 的绑定
        //EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(newClip);
        EditorCurveBinding[] curveBindings = AnimationUtility.GetObjectReferenceCurveBindings(newClip);
        // 遍历每个关键帧
        foreach (EditorCurveBinding curveBinding in curveBindings)
        {
            // 检查关键帧是否为 Sprite 类型
            if (curveBinding.type == typeof(SpriteRenderer))
            {
                // 获取关键帧所对应的对象
                ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve
                    (newClip, curveBinding);
                for (int i = 0; i < keyframes.Length; i++)
                {
                    // 获取关键帧的 sprite
                    Sprite sprite = keyframes[i].value as Sprite;
                    if (sprite != null)
                    {
                        // 设置新的 sprite
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
    //遗弃 复制AnimatorController的方法，会导致animationclip引用相同
    //AnimatorController animator = new();
    //animator.name = texture.name;
    //EditorUtility.CopySerialized(animatorController, animator);
    //// 使用AnimatorController.layers属性来获取复制后的animatorController的所有层级
    //AnimatorControllerLayer[] layers = animator.layers;
    //// 遍历每个层级
    //foreach (AnimatorControllerLayer layer in layers)
    //{
    //    // 使用AnimatorControllerLayer.stateMachine属性来获取每个层级的状态机
    //    AnimatorStateMachine stateMachine = layer.stateMachine;
    //    // 使用AnimatorStateMachine.states属性来获取每个状态机的所有状态
    //    ChildAnimatorState[] states = stateMachine.states;
    //    // 遍历每个状态
    //    foreach (ChildAnimatorState state in states)
    //    {
    //        // 使用AnimatorState.motion属性来获取或设置每个状态的动画剪辑
    //        Motion motion = state.state.motion;
    //        // 判断motion是否是AnimationClip类型
    //        if (motion is AnimationClip)
    //        {
    //            // 强制转换为AnimationClip类型
    //            AnimationClip clip = motion as AnimationClip;
    //            // 判断clip是否是你想要替换的动画剪辑，这里你可以根据名字或者其他条件来判断
    //            var listClip = newclips.Where((x) => { return x.name == clip.name; });
    //            if (listClip != null)
    //            {
    //                // 把新动画剪辑赋值给对应的状态的motion属性
    //                state.state.motion = listClip.ToArray()[0];
    //            }
    //        }
    //    }
    //}
}
#endif