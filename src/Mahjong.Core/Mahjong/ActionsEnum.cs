using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Mahjong
{
    public class ActionsEnum
    {
        public static readonly string 出明槓 = "出明槓";
        public static readonly string 自明槓 = "自明槓";
        public static readonly string 暗槓 = "暗槓";
        public static readonly string 打出 = "打出";
        public static readonly string 齋摸 = "齋摸";
        public static readonly string 碼糊 = "碼糊";
        public static readonly string 炸糊 = "炸糊";
        public static readonly string 搶槓 = "搶槓";
        public static readonly string 摸和 = "摸和";

        public static readonly string[] All = new string[] { 出明槓, 自明槓, 暗槓, 打出, 齋摸, 碼糊, 炸糊, 搶槓, 摸和 };
        public static readonly string[] RoundEndActions = new string[] { 打出, 齋摸, 碼糊, 炸糊, 搶槓, 摸和 };

        public static readonly string 出明槓AmountLevel = "出明槓AmountLevel";
        public static readonly string 自明槓AmountLevel = "自明槓AmountLevel";
        public static readonly string 暗槓AmountLevel = "暗槓AmountLevel";
        public static readonly string 打出AmountLevel = "打出AmountLevel";
        public static readonly string 齋摸AmountLevel = "齋摸AmountLevel";
        public static readonly string 碼糊AmountLevel = "碼糊AmountLevel";
        public static readonly string 炸糊AmountLevel = "炸糊AmountLevel";
        public static readonly string 搶槓AmountLevel = "搶槓AmountLevel";
        public static readonly string 摸和AmountLevel = "摸和AmountLevel";
    }                                                                                            
}                                                                                                
