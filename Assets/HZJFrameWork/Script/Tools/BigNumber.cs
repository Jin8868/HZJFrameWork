//=====================================================
// - FileName:      BigNumber.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/05/13 17:36:24
// - Description:   高精度数处理类，处理超过long long类型的大数运算
//======================================================
using System.Text;

namespace HZJFrameWork
{
    public class BigNumber
    {
        /// <summary>
        /// 处理的最大位数
        /// </summary>
        private const int MAXNUMBER = 100;

        private char[] mNumber;

        private int mDigit;

        public BigNumber(int number)
        {
            int tempNumber = number;
            int digit = 0;
            char[] temp = new char[MAXNUMBER];
            while (tempNumber > 0)
            {
                digit = tempNumber % 10;
                tempNumber = tempNumber / 10;

                temp[mDigit] = (char)('0' + digit);
                mDigit++;
            }

            mNumber = new char[mDigit];
            for (int i = 0; i < mDigit; ++i)
            {
                mNumber[i] = temp[mDigit - i - 1];
            }
        }

        public BigNumber(long number)
        {
            long tempNumber = number;
            int digit = 0;
            char[] temp = new char[MAXNUMBER];
            while (tempNumber > 0)
            {
                digit = (int)(tempNumber % 10);
                tempNumber = tempNumber / 10;

                temp[mDigit] = (char)('0' + digit);
                mDigit++;
            }

            mNumber = new char[mDigit];
            for (int i = 0; i < mDigit; ++i)
            {
                mNumber[i] = temp[mDigit - i - 1];
            }
        }

        public BigNumber(string number)
        {
            if (number.Length > MAXNUMBER)
            {
                return;
            }
            mNumber = new char[number.Length];
            mDigit = number.Length;
            for (int i = 0; i < number.Length; ++i)
            {
                mNumber[i] = number[i];
            }
        }

        public BigNumber(char[] number)
        {
            if (number.Length > MAXNUMBER)
            {
                return;
            }
            mNumber = new char[number.Length];
            mDigit = 0;

            while (number[mDigit] != '\0')
            {
                mNumber[mDigit] = number[mDigit];
                ++mDigit;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(mDigit);
            if (mNumber != null && mDigit > 0)
            {
                for (int i = 0; i < mDigit; ++i)
                {
                    stringBuilder.Append(mNumber[i]);
                }

            }
            return stringBuilder.ToString();
        }


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 获得当前的位数
        /// </summary>
        /// <returns></returns>
        public int GetDigits()
        {
            return mDigit;
        }

        private static char[] Reverse(char[] number)
        {
            if (number == null || number.Length <= 0)
            {
                return number;
            }
            int length = number.Length - 1;
            char[] newNumber = new char[length + 1];
            for (int i = 0; i < length; ++i)
            {
                newNumber[i] = number[length - i - 1];
            }
            return newNumber;
        }
        #region 加法
        public static BigNumber operator +(BigNumber num1, BigNumber num2)
        {
            string num1Str = num1.ToString();
            string num2Str = num2.ToString();

            int maxLength = num1Str.Length > num2Str.Length ? num1Str.Length + 1 : num2Str.Length + 1;
            if (maxLength > MAXNUMBER)
            {
                return null;
            }
            char[] newBigNumber = new char[maxLength];

            int index1 = num1Str.Length - 1;
            int index2 = num2Str.Length - 1;

            int add = 0;
            int curIndex = 0;
            while (index1 >= 0 || index2 >= 0 || add > 0)
            {
                int x = index1 >= 0 ? num1Str[index1] - '0' : 0;
                int y = index2 >= 0 ? num2Str[index2] - '0' : 0;

                int num = x + y + add;
                add = num / 10;
                newBigNumber[curIndex] = (char)(num % 10 + '0');
                curIndex++;

                index1--;
                index2--;
            }
            newBigNumber = Reverse(newBigNumber);
            return new BigNumber(newBigNumber);
        }
        #endregion

        #region  减法
        public static BigNumber operator -(BigNumber num1, BigNumber num2)
        {
            if (num1.GetDigits() > num2.GetDigits())
            {
                return Sub(num1, num2,false);
            }
            else
            {
                if (num1.GetDigits() == num2.GetDigits())
                {
                    string num1Str = num1.ToString();
                    string num2Str = num2.ToString();
                    
                    for (int i = 0;i< num1Str.Length;++i)
                    {
                        if (num1Str[i] > num2Str[i])
                        {
                            return Sub(num1, num2,false);
                        }
                        else
                        {
                            return Sub(num2, num1,true);
                        }
                    }
                    return new BigNumber('0');
                }
                return Sub(num2, num1,true);
            }
        }

        private static BigNumber Sub(BigNumber num1, BigNumber num2,bool isNegative)
        {
            string num1Str = num1.ToString();
            string num2Str = num2.ToString();

            char[] tempNumber1 = num1Str.ToCharArray();
            char[] tempNumber2 = num2Str.ToCharArray();

            int maxLength = num1Str.Length > num2Str.Length ? num1Str.Length : num2Str.Length;
            char[] returnNumber = new char[maxLength + 2];

            int index1 = num1Str.Length - 1;
            int index2 = num2Str.Length - 1;
            int curIndex = maxLength - 1;
            while (index1 >= 0 || index2 >= 0)
            {
                int tempNum1 = index1 >= 0 ? tempNumber1[index1] - '0' : 0;
                int tempNum2 = index2 >= 0 ? tempNumber2[index2] - '0' : 0;

                if (tempNum1 < tempNum2)
                {
                    tempNumber1[index1 - 1] -= (char)1;
                    tempNum1 += 10;
                }
                returnNumber[curIndex] = (char)((tempNum1 - tempNum2) + '0');
                --curIndex;

                --index1;
                --index2;
            }


            if (isNegative)
            {
                if (returnNumber[0] == '0')
                {
                    returnNumber[0] = '-'; 
                }
                else
                {
                    for (int i = 0; i < maxLength; ++i)
                    {
                        returnNumber[i + 1] = returnNumber[i];
                    }

                    returnNumber[0] = '-';
                }
            }
            else
            {
                if (returnNumber[0] == '0')
                {
                    for (int i = 1;i< maxLength;++i)
                    {
                        returnNumber[i - 1] = returnNumber[i];
                    }
                }
            }

            returnNumber[maxLength] = '\0';
            return new BigNumber(returnNumber);
        }
        #endregion

        #region  乘法
        public static BigNumber operator *(BigNumber num1, BigNumber num2)
        {
            string num1Str = num1.ToString();
            string num2Str = num2.ToString();

            int num1Length = num1Str.Length;
            int num2Length = num2Str.Length;

            int maxLength = num1Length + num2Length + 1;
            char[] tempNumber = new char[maxLength + 1];
            //乘法核心代码
            for (int i = num2Length - 1; i >= 0; --i)
            {
                int tempNum1 = num2Str[i] - '0';
                for (int j = num1Length - 1; j >= 0; --j)
                {
                    int tempNum2 = num1Str[j] - '0';
                    int curNum = tempNumber[i + j + 1] != '\0' ? tempNumber[i + j + 1] - '0' : 0;
                    int sum = tempNum1 * tempNum2 + curNum;
                    tempNumber[i + j + 1] = (char)((sum % 10) + '0');
                    tempNumber[i + j] += tempNumber[i + j] >= '0' ? (char)(sum / 10) : (char)((sum / 10) + '0');
                }

            }
            tempNumber[maxLength] = '\0';//添加结束符
            if (maxLength > 0 && tempNumber[0] == '0')
            {
                for (int i = 1; i < maxLength; ++i)
                {
                    tempNumber[i - 1] = tempNumber[i];
                }
            }
            return new BigNumber(tempNumber);
        }
        #endregion

        #region 除法
        public static BigNumber operator /(BigNumber num1, BigNumber num2)
        {
            return null;
        }
        #endregion

        #region  重载==和!=
        public static bool operator ==(BigNumber num1, BigNumber num2)
        {
            return false;
        }

        public static bool operator !=(BigNumber num1, BigNumber num2)
        {
            return false;
        }
        #endregion

    }
}

