using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace APP.FUNC
{
    public class AccountFunc
    {
        /// <summary>
        /// 회원 사설(Local or Internal) 아이피 조회
        /// </summary>
        /// <returns></returns>
        public string GetLocalIP()
        {
            var Host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in Host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "";
        }

        /// <summary>
        /// 회원 사설(Public or External) 아이피 조회
        /// </summary>
        /// <returns></returns>
        public string GetPublicIP()
        {
            string externalIP = new WebClient().DownloadString("http://ipinfo.io/ip").Trim(); // or http://icanhazip.com

            if (String.IsNullOrWhiteSpace(externalIP))
            {
                externalIP = "";
            }

            return externalIP;
        }

        /// <summary>
        /// salting을 만들기 전 salt 값 만들기
        /// </summary>
        /// <returns></returns>
        public byte[] GetSalt()
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        /// <summary>
        /// pbkdf2 암호화
        /// </summary>
        /// <returns></returns>
        public string GetPwdEncryt(byte[] salt, string key, int iterationCnt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: key,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: iterationCnt,
                numBytesRequested: 256 / 8
                ));
        }

    }
}
