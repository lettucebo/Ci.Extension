using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Ci.Extension
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
        /// 取得當前文化特性名稱
        /// </summary>
        /// <returns>文化特性名稱</returns>
        public static string CultureName()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
        }

        /// <summary>
        /// 將字串進行SHA256雜湊運算
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>雜湊結果</returns>
        public static string ToSha256(this string str)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider(); // 建立一個SHA256
            var source = Encoding.Default.GetBytes(str); // 將字串轉為Byte[]
            var crypto = sha256.ComputeHash(source); // 進行SHA256加密
            var result = Convert.ToBase64String(crypto); // 把加密後的字串從Byte[]轉為字串
            return result; // 輸出結果
        }

        /// <summary>
        /// 將字串進行SHA256雜湊運算
        /// </summary>
        /// <param name="str">要雜湊的原始字串</param>
        /// <param name="salt">將原始字串複雜化的「鹽」</param>
        /// <returns>雜湊結果</returns>
        public static string ToSha256(this string str, string salt)
        {
            return (str + salt).ToSha256();
        }

        /// <summary>
        /// 將字串進行MD5雜湊運算
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>雜湊結果</returns>
        public static string ToMd5(this string str)
        {
            // create new instance of md5
            var md5 = MD5.Create();

            // convert the input text to array of bytes
            var hashData = md5.ComputeHash(Encoding.Default.GetBytes(str));

            // create new instance of StringBuilder to save hashed data
            var returnValue = new StringBuilder();

            // loop for each byte and add it to StringBuilder
            foreach (var t in hashData)
            {
                returnValue.Append(t.ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
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
        /// AES區塊加密
        /// </summary>
        /// <param name="originalString">要加密的字串</param>
        /// <param name="key">金鑰</param>
        /// <returns>加密結果，執行失敗時回傳Null</returns>
        public static string ToAesEncryptBase64(this string originalString, string key)
        {
            string encrypt = null;
            try
            {
                var aes = new AesCryptoServiceProvider();
                var md5 = new MD5CryptoServiceProvider();
                var sha256 = new SHA256CryptoServiceProvider();
                var keyBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                var iv = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                aes.Key = keyBytes;
                aes.IV = iv;

                var dataByteArray = Encoding.UTF8.GetBytes(originalString);
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return encrypt;
        }

        /// <summary>
        /// AES區塊解密
        /// </summary>
        /// <param name="originalString">要解密的字串</param>
        /// <param name="key">金鑰</param>
        /// <returns>解密結果，執行失敗時回傳Null</returns>
        public static string ToAesDecryptBase64(this string originalString, string key)
        {
            string decrypt = null;
            try
            {
                var aes = new AesCryptoServiceProvider();
                var md5 = new MD5CryptoServiceProvider();
                var sha256 = new SHA256CryptoServiceProvider();
                var keyBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                var iv = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                aes.Key = keyBytes;
                aes.IV = iv;

                var dataByteArray = Convert.FromBase64String(originalString);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dataByteArray, 0, dataByteArray.Length);
                        cs.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return decrypt;
        }
    }
}
