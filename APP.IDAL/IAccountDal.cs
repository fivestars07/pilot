using APP.MODEL;

namespace APP.IDAL
{
    public interface IAccountDal
    {

        bool SetInfo(UserInfo model);

        bool SetLogin(UserLogin model);

        bool SetUser(UserInfo infoModel, UserLogin loginModel);

        bool SetUpdateUser(UserInfo infoModel, UserLogin loginModel);

        /// <summary>
        /// 아이디, 패스워드로 유저 조회
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        UserAll GetSelectUser(string id, string pwd);

        UserAll GetSelectUser(int userNo);

        UserLogin GetSelectUserLogin(string id);

        void SetLoginDate(UserAll model);
    }
}
