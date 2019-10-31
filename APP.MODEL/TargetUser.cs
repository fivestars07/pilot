using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.MODEL
{
    public class TargetUser
    {
        [Key]
        public int seq { get; set; }

        public int companyCD { get; set; }

        public string id { get; set; }

        public string name { get; set; }

        public string email { get; set; }


        /// <summary>
        /// '|' 로 구분자로 부서 표시
        /// </summary>
        public string organize { get; set; }

        /// <summary>
        /// organize 의 data 값이 '|' 구분자 수
        /// </summary>
        public int orgDepth { get; set; }

        public string position { get; set; }

        public string onoff { get; set; }

        public string regUser { get; set; }

        public DateTime regdate { get; set; }
    }
}
