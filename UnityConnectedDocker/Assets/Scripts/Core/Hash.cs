using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace ConnectServer
{
    public class Hash
    {
        /// <summary>
        /// Make hashed string
        /// </summary>
        /// <param name="origin">
        /// Original string
        /// </param>
        /// <returns>
        /// Hashed string
        /// </returns>
        public static string HashString(string origin)
        {
            var salt = "fjaKfaK9jvalfaok";
            var targetBytes = Encoding.UTF8.GetBytes(origin + salt);

            // MD5ハッシュを計算
            var csp = new MD5CryptoServiceProvider();
            var hashBytes = csp.ComputeHash(targetBytes);

            // バイト配列を文字列に変換
            var hash = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                hash.Append(hashByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}