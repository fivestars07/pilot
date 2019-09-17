using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.MODEL
{
    public class UserLogin : ICloneable
    {
        /// <summary>
        /// 회원 번호
        /// </summary>
        [Key]
        public int userNo { get; set; }

        /// <summary>
        /// 회원 아이디
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 회원 패스워드
        /// </summary>
        public string pwd { get; set; }

        /// <summary>
        /// 암호화시 사용할 salt
        /// </summary>
        public byte[] salt { get; set; }

        /// <summary>
        /// 회원 권한
        /// </summary>
        public string role { get; set; }

        /// <summary>
        /// 회원 로그인시 로컬 아이피
        /// </summary>
        public string ip { get; set; }

        /// <summary>
        /// 회원 로그인시 틀림방지 횟수
        /// </summary>
        public int changeCycle { get; set; }

        /// <summary>
        /// 회원 로그인시 틀린 횟수
        /// </summary>
        public int wrongCnt { get; set; }

        /// <summary>
        /// 로그인 시간
        /// </summary>
        public DateTime? loginDate { get; set; }

        /// <summary>
        /// 로그아웃 시간
        /// </summary>
        public DateTime? logoutDate { get; set; }

        /// <summary>
        /// 회원 탈퇴 여부
        /// </summary>
        [DefaultValue('Y')]
        public char activityYN { get; set; }

        /// <summary>
        /// 객체의 깊은 복사 (Deep Copy)
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            UserLogin obj = new UserLogin();
            obj.userNo = this.userNo;
            obj.id = this.id;
            obj.pwd = this.pwd;
            obj.salt = this.salt;
            obj.ip = this.ip;
            obj.changeCycle = this.changeCycle;
            obj.wrongCnt = this.wrongCnt;

            return obj;
        }
    }
}