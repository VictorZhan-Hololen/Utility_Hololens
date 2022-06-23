using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Video.Utility
{
    public class MathUtility
    {
        public static string ToFullNumberString(string value, int lenOfString, bool isLeftSide = true, string addStr = "0")
        {
            string result = value.ToString();
            while (result.Length < lenOfString)
            {
                if (isLeftSide)
                {
                    result = addStr + result;
                }
                else
                {
                    result += addStr;
                }
            }
            return result;

        }
    }
}
