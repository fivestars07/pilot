using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.MODEL
{
    public class UserInfo
    {
        /// <summary>
        /// 회원 번호
        /// </summary>
        [Key]
        public int userNo { get; set; }

        /// <summary>
        /// 회원 이름
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 회원 메일
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 회원 전화번호
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 회원정보 수정일
        /// </summary>
        public DateTime? editDate { get; set; }

        /// <summary>
        /// 가입일
        /// </summary>
        public DateTime? regDate { get; set; }

    }
}