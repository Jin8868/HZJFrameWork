using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace HZJFrameWork
{
    public class ReporterRoot : MonoBehaviour
    {
        public GUISkin mBtnSkin;


        public Reporter mReporter;

        [HideInInspector]
        public bool mIsShowLogFrame;

        #region Rect
        private Rect mMiniWindowRect;

        private Rect mLogFrameRect;

        private Rect mMiniBtnRect;
        #endregion

        #region GUIStyle
        private GUIStyle mMiniWindowStyle;
        #endregion

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            mIsShowLogFrame = false;
            InitRectData();
            //InitGuiStyle();
        }

        void OnGUI()
        {
            GUI.skin = mBtnSkin;
            if (mIsShowLogFrame)
            {
                mLogFrameRect = GUI.Window(20, mLogFrameRect, SetLogWindow, "日志系统");
            }
            else
            {
                mMiniWindowRect = GUI.Window(10, mMiniWindowRect, SetMiniWindow, "FPS");
            }
        }

        private void InitGuiStyle()
        {
            mMiniWindowStyle = new GUIStyle();
            mMiniWindowStyle.fontSize = 15;
        }

        private void InitRectData()
        {
            float windowsScale = Screen.width / 1080.0f;

            Vector2 miniWindowSize = new Vector2(150.0f, 150.0f) * windowsScale;

            Vector2 miniWindowPos = new Vector2(10.0f, Screen.height / 4);

            mMiniWindowRect = new Rect(miniWindowPos, miniWindowSize);

            mLogFrameRect = new Rect(0.0f, 0.0f, Screen.width, Screen.height);

            mMiniBtnRect = new Rect(miniWindowSize.x * 0.1f, miniWindowSize.y * 0.2f, miniWindowSize.x * 0.8f, miniWindowSize.y * 0.6f);
        }

        private void SetLogWindow(int id)
        {
            mReporter.OnGUIDraw();
            GUI.DragWindow();
        }

        private void SetMiniWindow(int id)
        {
            if (GUI.Button(mMiniBtnRect, "100"))
            {
                if (mReporter)
                {
                    mIsShowLogFrame = true;
                    mReporter.show = true;
                    mReporter.SetReportRoot(this);
                }
            }
            GUI.DragWindow();
        }

    }
}
