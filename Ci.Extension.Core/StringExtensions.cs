using System;
using System.Text.RegularExpressions;

namespace Ci.Extension.Core
{
    /// <summary>
    /// String Extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 字串null也去空白
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToTrim(this string value)
        {
            return value == null ? string.Empty : value.Trim();
        }

        /// <summary>
        /// check string is numeric or not
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string input)
        {
            return int.TryParse(input, out int number);
        }

        /// <summary>
        /// 是否有此值
        /// </summary>
        /// <param name="sourceValue">來源值</param>
        /// <param name="value">檢查值</param>
        /// <returns>檢查結果</returns>
        public static bool HasValue(this int sourceValue, int value)
        {
            return (value & sourceValue) == value;
        }

        /// <summary>
        /// 與<code>string.IsNullOrWhiteSpace()</code>相同，純粹為了Coding時可以順一點而寫的擴充
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>IsNullOrWhiteSpace</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 與<code>string.IsNullOrEmpty()</code>相同，純粹為了Coding時可以順一點而寫的擴充
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>IsNullOrEmpty</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 過濾含有Html標籤的文字內容
        /// </summary>
        /// <param name="content">原始字串</param>
        /// <returns>過濾結果</returns>
        public static string RemoveHtmlTag(this string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                var regex = new System.Text.RegularExpressions.Regex(@"<[^>]*>");
                content = regex.Replace(content, string.Empty);
                content = content.Replace("\r\n", string.Empty).Replace("&nbsp;", string.Empty).Replace("　", string.Empty).Replace(" ", string.Empty).Trim();
            }
            else
            {
                content = string.Empty;
            }

            return content;
        }

        /// <summary>
        /// 將字串中無法轉為xml編碼的字元移除
        /// </summary>
        /// <param name="txt">要進行處理的字串</param>
        /// <returns>處理後的字串</returns>
        public static string ReplaceHexadecimalSymbols(this string txt)
        {
            return Regex.Replace(txt, "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]", string.Empty, RegexOptions.Compiled);
        }

        /// <summary>
        /// 將字串中的最後一個字取代為指定字串
        /// </summary>
        /// <param name="value">原字串</param>
        /// <param name="newString">要取代上去的新字串</param>
        /// <returns>取代後的結果字串</returns>
        public static string ReplaceLast(this string value, string newString)
        {
            return value.Remove(value.Length - 1) + newString;
        }

        /// <summary>
        /// 檢查傳入字串的長度，並在字串長度超出限制時，將多餘字串以三個點"..."代替。
        /// <remarks><para>注意: 替代結果將依然滿足長度限制，如果需要特殊需求，請使用其他多載</para></remarks>
        /// </summary>
        /// <param name="originalString">原始字串</param>
        /// <param name="maximumLength">最長限制</param>
        /// <returns>結果值</returns>
        public static string ConstraintStringLength(this string originalString, int maximumLength)
        {
            return originalString.ConstraintStringLength(maximumLength, true, "...");
        }

        /// <summary>
        /// 檢查串入字串的長度，並將超出的部分以替代字串代替。
        /// </summary>
        /// <param name="originalString">原字串</param>
        /// <param name="maximumLength">最長限制</param>
        /// <param name="replacementString">要替代的字串</param>
        /// <returns>結果值</returns>
        public static string ConstraintStringLength(this string originalString, int maximumLength, string replacementString)
        {
            return originalString.ConstraintStringLength(maximumLength, false, replacementString);
        }

        /// <summary>
        /// 限制字串最大長度，當原始字串長度小於等於限制時，將不進行任何處理。
        /// </summary>
        /// <param name="originalString">原始字串</param>
        /// <param name="maximumLength">限制最大長度</param>
        /// <param name="includeReplaceString">限制值是否包含替代字串
        /// <remarks><para>例如某字串限制最長20個字，並以"..."替換多餘字串，當此參數設為true時，則表示替代後的字串依然會滿足限制，此例中即為20個字。</para></remarks>
        /// </param>
        /// <param name="replacementString">要替代超出部分的字串</param>
        /// <returns>執行結果</returns>
        public static string ConstraintStringLength(this string originalString, int maximumLength, bool includeReplaceString, string replacementString)
        {
            // 傳入空字串 或 長度限制不合法
            if (originalString.IsNullOrEmpty() || maximumLength <= 0) return originalString;

            // 在長度限制包含替代字串的請求下，傳入的替代字串大於限制長度
            if (includeReplaceString && replacementString.Length > maximumLength) return originalString;

            // 原字串長度符合限制
            if (originalString.Length <= maximumLength) return originalString;

            if (!includeReplaceString)
            {
                return originalString.Substring(0, maximumLength - 1) + replacementString;
            }

            return originalString.Substring(0, maximumLength - replacementString.Length - 1) + replacementString;
        }

        /// <summary>
        /// SubString and check  length is enough, if not retrun full string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStringLength(this string source, int startIndex, int length)
        {
            return source.Substring(startIndex, Math.Min(source.Length - startIndex, length));
        }

        /// <summary>
        /// Check input string is valid email format
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmail(this string source)
        {
            return Regex.IsMatch(source,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}
