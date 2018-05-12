using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace www.asm32.net
{
    public class IPv4Util
    {
        public const string sRegExpIPv4 = "^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$";
        public const string sRegExpIPv4Rule = "^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$|^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}-\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$";

        /// <summary>
        /// IPv4Util
        /// </summary>
        public IPv4Util() {
        }

        /// <summary>
        /// 转换 IPv4 地址为 32 位无符号整型数，比如 inet_aton("127.0.0.1") 得到 2130706433
        /// </summary>
        /// <param name="ip">string类型的 IPv4 地址</param>
        /// <returns>UInt32 类型的无符号32位整数对应的IP</returns>
        public UInt32 inet_aton(string ip)
        {
            Match m = Regex.Match(ip, sRegExpIPv4);
            UInt32 uintIP = 0;
            if (m.Success)
            {
                string[] digits = ip.Split('.');
                int l = 0;
                if ((l = digits.Length) == 4)
                {
                    for (int i = 0; i < l; i++)
                    {
                        UInt32 n = Convert.ToUInt32(digits[i]);
                        if (n > 255) return 0;
                        uintIP = (uintIP << 8) | n;
                    }
                }
            }
            return uintIP;
        }

        /// <summary>
        /// 验证IPv4规则是不是有效
        /// </summary>
        /// <param name="rule">IPv4规则数据</param>
        /// <returns>有效返回true，无效返回false</returns>
        public bool VerifyIpRules(string rule)
        {
            if (rule == null) return false;
            string[] list = rule.Split(',');
            char[] delimiter = { '.', '-' };
            Match m = null;
            for (int i = 0, l = list.Length; i < l; i++)
            {
                m = Regex.Match(list[i], sRegExpIPv4Rule);
                if (!m.Success) return false;
                string[] values = list[i].Split(delimiter, 8);
                for (int n = 0, lv = values.Length; n < lv; n++)
                {
                    if (Convert.ToUInt32(values[n]) > 254) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 验证 IPv4 的 IP 是不是符合 这个 IPv4 的规则
        /// </summary>
        /// <param name="rule">IPv4的规则</param>
        /// <param name="userIp">IPv4的IP</param>
        /// <returns>IP符合规则返回true 不符合返回false</returns>
        public bool VerifyIpAvailable(string rule, string userIp)
        {
            if (rule == null || userIp == null) return false;
            if (VerifyIpRules(rule) == false) return false;
            string[] rules = rule.Split(',');
            UInt32 uiIpAddress = inet_aton(userIp);
            Match m = null;
            for (int i = 0, l = rules.Length; i < l; i++)
            {
                string[] address = rules[i].Split('-');
                int al = address.Length;
                if (al == 1)
                {
                    Console.WriteLine("address[0]=" + address[0] + " : " + Convert.ToString(inet_aton(address[0])) +
                        "\tuserIp=" + userIp + " : " + Convert.ToString(uiIpAddress));

                    if (uiIpAddress == inet_aton(address[0]))
                    {
                        return true;
                    }
                }
                else if (al == 2)
                {
                    UInt32 min = inet_aton(address[0]);
                    UInt32 max = inet_aton(address[1]);
                    if (min > max)
                    {
                        UInt32 t = min;
                        min = max;
                        max = t;
                    }
                    if (uiIpAddress >= min && uiIpAddress <= max)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
