using APP.FUNC;
using APP.IDAL;
using APP.MODEL;
using APP.VIEWMODEL;
using System;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace APP.BLL
{
    public class AccountBll
    {
        private readonly UserInfo _userInfo;
        private readonly UserLogin _userLogin;
        private UserAll _userAll;
        private readonly IAccountDal _accountDal;
        private readonly AccountFunc _func;

        public AccountBll(UserInfo userInfo, UserLogin userLogin, UserAll userAll, IAccountDal accountDal)
        {
            _userInfo = userInfo;
            _userLogin = userLogin;
            _userAll = userAll;
            _accountDal = accountDal;
            _func = new AccountFunc();
        }

        public bool SetJoin(JoinView model)
        {
            if (model != null)
            {
                _userInfo.userName = model.userName;
                _userInfo.email = model.email;
                _userInfo.phone = model.phone;
                _userInfo.regDate = DateTime.Now;
                //=======================================================================
                _userLogin.id = model.id;
                _userLogin.role = model.role;
                _userLogin.ip = _func.GetLocalIP();
                _userLogin.salt = _func.GetSalt();
                _userLogin.pwd = _func.GetPwdEncryt(_userLogin.salt, model.pwd, 10000);
                _userLogin.activityYN = 'Y';
                //=======================================================================
                return _accountDal.SetUser(_userInfo, _userLogin);
            }

            return false;
        }

        public bool SetUpdateUser(JoinView model)
        {
            if (model != null)
            {
                _userInfo.userNo = model.userNo;
                _userInfo.userName = model.userName;
                _userInfo.email = model.email;
                _userInfo.phone = model.phone;
                _userInfo.editDate = DateTime.Now;
                //=======================================================================
                _userLogin.userNo = model.userNo;
                _userLogin.role = model.role;
                //=======================================================================

                return _accountDal.SetUpdateUser(_userInfo, _userLogin);
            }
            return false;
        }

        /// <summary>
        /// 아이디, 패스워드로 유저 조회
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public UserAll GetSelectUser(string id, string pwd)
        {
            byte[] salt = null;
            string password = "";

            if (!string.IsNullOrWhiteSpace(id))
            {
                salt = _accountDal.GetSelectUserLogin(id).salt;
                password = _func.GetPwdEncryt(salt, pwd, 10000);
            }

            if (salt != null && !string.IsNullOrWhiteSpace(password))
            {
                return _accountDal.GetSelectUser(id, password);
            }

            return null;
        }

        public UserAll GetSelectUser(int UserNo)
        {
            return _accountDal.GetSelectUser(UserNo);
        }

        public void SetLoginDate(UserAll model)
        {
            // loginDate 가 null 인경우 => loginDate에 현재 날짜 입력
            // loginDate 가 null 이 아닌 경우 => loginDate에 현재 날짜 입력 / logoutDate에 쿠키 expiredate add 값 입력
            // 로그인 후 바로 쿠키 삭제 후 재로그인 시 else 부분에서 logoutDate가 loginDate 보다 이후 일수 있음
            // => 이경우 같은 날짜로 셋팅

            var now = DateTime.Now;

            if (model.loginDate == null)
            {
                model.loginDate = now;
            }
            else
            {
                DateTime login = model.loginDate ?? now;
                DateTime logout = model.logoutDate ?? now;
                DateTime tempLogout = login + TimeSpan.FromMinutes(5);

                model.loginDate = now;

                if (DateTime.Compare(now, tempLogout) > 0)
                {
                    model.logoutDate = tempLogout;
                }
                else
                {
                    model.logoutDate = now;
                }
            }
            _accountDal.SetLoginDate(model);
        }
    }
}
