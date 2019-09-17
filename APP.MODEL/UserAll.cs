using System;
using System.Collections.Generic;
using System.Text;

namespace APP.MODEL
{
    public class UserAll
    {
        public int userNo { get; set; }

        public string userName { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public DateTime? regDate { get; set; }

        public string id { get; set; }

        public byte[] salt { get; set; }

        public string role { get; set; }

        public string ip { get; set; }

        public int changeCycle { get; set; }

        public int wrongCnt { get; set; }

        public DateTime? loginDate { get; set; }

        public DateTime? logoutDate { get; set; }

    }
}
