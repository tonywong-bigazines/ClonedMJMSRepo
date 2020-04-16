using System;
using System.Collections.Generic;
using System.Text;

namespace Mahjong.Mahjong
{ 
    public class CardTypes
    {
        public static readonly string Table = "Table";
        public static readonly string Staff = "Staff";
        public static readonly string Client = "Client";
        public static readonly string FakeClient = "FakeClient";

        public static readonly string[] All = new string[] { Table, Staff, Client, FakeClient };
    }
}
