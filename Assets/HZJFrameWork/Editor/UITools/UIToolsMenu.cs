//=====================================================
// - FileName:      UIToolsMenu.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/08/06 13:33:23
// - Description:   UI工具菜单
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

namespace HZJFrameWork
{
    public class UIToolsMenu : MonoBehaviour
    {
        private const string defaultUIName = "New UI";

        private const string tipsTitle = "HZJ-UI系统";
        private const string tipsOK = "OK";

        private const string UIScriptPath = "Assets/Script/UI";

        [MenuItem("GameObject/★UI拓展★/创建UI", false, -100)]
        private static void CreateUI()
        {
            GameObject newUI = new GameObject(defaultUIName);
            GameObject canvas = GameObject.Find("UICanvas");
            newUI.transform.parent = canvas.transform;
            newUI.layer = LayerMask.NameToLayer("UI");
            RectTransform rectTransform = newUI.AddComponent<RectTransform>();
            //修改位置、大小、旋转
            rectTransform.anchoredPosition3D = Vector3.zero;
            rectTransform.localScale = Vector3.one;
            rectTransform.eulerAngles = Vector3.zero;
            //修改锚点为全屏
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            //修改偏移为0
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            newUI.AddComponent<UIOutsideInfos>();
        }

        [MenuItem("GameObject/★UI拓展★/创建UI脚本", false, -100)]
        private static void CreateUIScript()
        {
            GameObject selectObject = Selection.activeGameObject;

            if (!selectObject.name.EndsWith("UI"))
            {
                EditorUtility.DisplayDialog(tipsTitle, "请选中正确的UI物体！", tipsOK);
                return;
            }

            if (selectObject.name.Equals(defaultUIName))
            {
                EditorUtility.DisplayDialog(tipsTitle, "请修改New UI名字！", tipsOK);
                HZJLog.LogError("请修改New UI名字！");
                return;
            }

            UIOutsideInfos uiOutsideInfo = selectObject.GetComponent<UIOutsideInfos>();
            string getInfo = string.Empty;
            string componentInfo = string.Empty;
            for (int i = 0; i < uiOutsideInfo.UIInfoList.Count; ++i)
            {
                OutSideItemData itemData = uiOutsideInfo.UIInfoList[i];
                if (itemData == null || itemData.gameObject == null)
                {
                    HZJLog.LogError($"{selectObject.name}节点的第{i + 1}个节点为空,请检查！");
                    continue;
                }
                componentInfo += $"    private {itemData.GetShortTypeName()} {itemData.objectName};\n";
                if (itemData.GetShortTypeName().Equals("GameObject"))
                {

                    getInfo += $"        {itemData.objectName} = Get(\"{itemData.objectName}\");\n";
                }
                else
                {
                    getInfo += $"         {itemData.objectName} = Get<{itemData.GetShortTypeName()}>(\"{itemData.objectName}\");\n";
                }

            }
            CreateUIViewScript(selectObject.name, getInfo, componentInfo);
            CreateUIScript(selectObject.name);
            AssetDatabase.Refresh();
        }

        private static void CreateUIViewScript(string uiName, string getInfo,string componentInfo)
        {
            string viewPath = UIScriptPath + "/UIView/" + uiName;
            string fileName = viewPath + "View";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            StreamWriter sw = File.CreateText(fileName + ".cs");
            sw.WriteLine("using UnityEngine;");
            sw.WriteLine("using UnityEngine.UI;");
            sw.WriteLine("using HZJFrameWork;");
            sw.WriteLine("//////////////////////");
            sw.WriteLine("///代码生成，请勿修改///");
            sw.WriteLine("//////////////////////");
            sw.WriteLine($"public partial class {uiName} : UIBase");
            sw.WriteLine("{");
            sw.WriteLine(componentInfo);
            sw.WriteLine(@"
    protected override void InitComponent()
    {");
            sw.WriteLine(getInfo);
            sw.WriteLine(@"    }
}");
            sw.Close();
        }

        private static void CreateUIScript(string uiName)
        {
            string scriptPath = UIScriptPath + "/UIEntity/" + uiName + ".cs";
            if (File.Exists(scriptPath))
            {
                HZJLog.LogWithGreen($"UI脚本自动化:{uiName}已经存在，不重新生成！");
            }
            else
            {
                StreamWriter sw = File.CreateText(scriptPath);
                sw.WriteLine(@"
using UnityEngine;
using HZJFrameWork;");
                sw.WriteLine($"public partial class {uiName} : UIBase");

                sw.WriteLine(@"{");
                sw.WriteLine($"    public {uiName}()");
                sw.WriteLine(@"    {
        UIOrder = HZJFrameWorkDefine.UIOrder.UIWindows;");
                //sw.WriteLine($"        mUIBundleName = \"{uiName}\";");
                sw.WriteLine(@"
    }");
                sw.WriteLine(@"
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Refresh()
    {
        base.Refresh();
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}");
                sw.Close();
            }
        }
    }
}

