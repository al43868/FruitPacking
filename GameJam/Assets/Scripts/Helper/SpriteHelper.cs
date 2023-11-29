#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;
public class SpriteHelper : OdinMenuEditorWindow
{
    protected override OdinMenuTree BuildMenuTree()
    {
        OdinMenuTree tree = new(supportsMultiSelect: true)
        {
            { "Home", this, EditorIcons.House },
        };
        return tree;
    }
    [MenuItem("Tools/SpriteHelper")]
    private static void OpenWindow()
    {
        var window = GetWindow<SpriteHelper>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
    }
    public Texture2D spriteSheet;
    public string fileName;
    [Button]
    private void Split()
    {
        // 选择切片图集
        //Texture2D spriteSheet = Selection.activeObject as Texture2D;
        if (spriteSheet == null)
        {
            Debug.Log("Please select a sprite sheet.");
            return;
        }

        // 获取切片信息
        Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(spriteSheet));
        foreach (Object sprite in sprites)
        {
            if (sprite is Sprite)
            {
                // 创建新的Texture2D保存切片图片
                Texture2D individualSprite = new Texture2D((int)((Sprite)sprite).rect.width, (int)((Sprite)sprite).rect.height);
                var pixels = ((Sprite)sprite).texture.GetPixels((int)((Sprite)sprite).rect.x,
                                                                (int)((Sprite)sprite).rect.y,
                                                                (int)((Sprite)sprite).rect.width,
                                                                (int)((Sprite)sprite).rect.height);
                individualSprite.SetPixels(pixels);
                individualSprite.Apply();

                // 保存切片图片
                byte[] bytes = individualSprite.EncodeToPNG();
                System.IO.File.WriteAllBytes("Assets/Sprites/Items/"
                    +fileName+"/"+ sprite.name + ".png", bytes);
            }
        }
        AssetDatabase.Refresh();

        Debug.Log("Sprite sheet has been split into individual images.");
    }
}
#endif