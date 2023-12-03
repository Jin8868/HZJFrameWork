using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

namespace HZJFrameWork
{
    public class HZJLog
    {
        private static string mLogHead = "HZJ|:";

        public string logHead { set { mLogHead = value; } }

        private static bool mIsOpenLog = true;

        public bool IsOpenLog { get { return mIsOpenLog; } set { mIsOpenLog = value; } }

        private static StringBuilder mTempStringBuilder;

        private const string colorFormat = "<color=#{0}>{1}</color>";

        public static void Log(params object[] log)
        {
            string logText = GetLogText(log);

            OutPutLog(logText);
        }

        public static void LogWithRed(params object[] log)
        {
            string logText = GetLogText(log);

            string logWithColor = GetLogTextWithColor(Color.red, logText);
            OutPutLog(logWithColor);
        }

        public static void LogError(params object[] log)
        {
            string logText = GetLogText(log, string.Empty);
            Debug.LogError(logText);
        }

        public static void LogWithGreen(params object[] log)
        {
            string logText = GetLogText(log);

            string logWithColor = GetLogTextWithColor(Color.green, logText);
            OutPutLog(logWithColor);
        }

        public static void LogWithYellow(params object[] log)
        {
            string logText = GetLogText(log);

            string logWithColor = GetLogTextWithColor(Color.yellow, logText);
            OutPutLog(logWithColor);
        }

        public static void LogWithNetwork(params object[] log)
        {
            string logText = GetLogText(log,"NetWork:");

            string logWithColor = string.Format(colorFormat, "99CCFF", logText);
            OutPutLog(logWithColor);
        }

        public static void LogWithIap(params object[] log)
        {
            string logText = GetLogText(log, "Iap:");

            string logWithColor = string.Format(colorFormat, "CC33FF", logText);
            OutPutLog(logWithColor);
        }

        private static void OutPutLog(string logText)
        {
            if (mIsOpenLog)
            {
                Debug.Log(logText);
            }
        }

        private static string GetLogTextWithColor(Color logColor, string logText)
        {
            string hexColor = ColorUtility.ToHtmlStringRGB(logColor);

            string tempText = string.Format(colorFormat, hexColor, logText);

            return tempText;
        }

        private static string GetLogText(object[] log,string sTitle = null)
        {
            if (mTempStringBuilder == null)
            {
                mTempStringBuilder = new StringBuilder();
            }
            mTempStringBuilder.Clear();
            if (sTitle == null)
            {
                mTempStringBuilder.Append(mLogHead);
            }
            else
            {
                mTempStringBuilder.Append(sTitle);
            }


            for (int i = 0; i < log.Length; i++)
            {
                if (log[i] == null)
                {
                    mTempStringBuilder.Append("Null");

                }
                else
                {
                    mTempStringBuilder.Append(log[i].ToString());
                }
                if (i < log.Length - 1)
                {
                    mTempStringBuilder.Append(", ");
                }
            }

            return mTempStringBuilder.ToString();
        }

    }
}

