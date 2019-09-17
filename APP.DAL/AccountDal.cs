using APP.DAL.APPContext;
using APP.IDAL;
using APP.MODEL;
using APP.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace APP.DAL
{
    public class AccountDal : IAccountDal
    {

        private readonly DataContext _dataContext;
        private UserAll _userAll;

        public AccountDal(DataContext dataContext, UserAll userAll)
        {
            _dataContext = dataContext;
            _userAll = userAll;
        }

        public bool SetInfo(UserInfo model)
        {
            if (model != null)
            {
                _dataContext.UserInfos.Add(model);
                if (_dataContext.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool SetLogin(UserLogin model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 유저 가입
        /// </summary>
        /// <param name="infoModel"></param>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public bool SetUser(UserInfo infoModel, UserLogin loginModel)
        {
            if (infoModel != null && loginModel != null)
            {
                // transaction begin
                _dataContext.Database.BeginTransaction();

                try
                {
                    _dataContext.UserInfos.Add(infoModel);
                    _dataContext.SaveChanges();

                    loginModel.userNo = infoModel.userNo;

                    _dataContext.UserLogins.Add(loginModel);
                    _dataContext.SaveChanges();

                    //transaction commit
                    _dataContext.Database.CommitTransaction();

                    return true;
                }
                catch (Exception)
                {
                    // transaction rollback
                    _dataContext.Database.RollbackTransaction();
                    return false;
                }
            }
            return false;
        }

        public bool SetUpdateUser(UserInfo infoModel, UserLogin loginModel)
        {
            if (infoModel != null && loginModel != null)
            {
                _dataContext.Database.BeginTransaction();

                try
                {
                    var tempInfo = _dataContext.UserInfos.Where(p => p.userNo == infoModel.userNo).SingleOrDefault();
                    if (tempInfo != null)
                    {
                        tempInfo.userName = infoModel.userName;
                        tempInfo.email = infoModel.email;
                        tempInfo.phone = infoModel.phone;
                        tempInfo.editDate = infoModel.editDate;
                        _dataContext.SaveChanges();
                    }

                    var tempLogin = _dataContext.UserLogins.Where(p => p.userNo == loginModel.userNo).SingleOrDefault();
                    if (tempLogin != null)
                    {
                        tempLogin.role = loginModel.role;
                        _dataContext.SaveChanges();
                    }

                    _dataContext.Database.CommitTransaction();

                    return true;
                } catch(Exception)
                {
                    _dataContext.Database.RollbackTransaction();
                    return false;
                }
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
            var query = _dataContext.UserLogins
                .Join(_dataContext.UserInfos,
                    t1 => t1.userNo,
                    t2 => t2.userNo,
                    (t1, t2) => new { t1, t2 })
                .Where(p => p.t1.id == id && p.t1.pwd == pwd && p.t1.activityYN == 'Y')
                .Select(p => new
                {
                    p.t2.userNo,
                    p.t2.userName,
                    p.t2.email,
                    p.t2.phone,
                    p.t2.regDate,
                    p.t1.id,
                    p.t1.salt,
                    p.t1.role,
                    p.t1.ip,
                    p.t1.changeCycle,
                    p.t1.wrongCnt,
                    p.t1.loginDate,
                    p.t1.logoutDate
                });

            //var query = from a in _dataContext.UserLogins
            //            join b in _dataContext.UserInfos on a.userNo equals b.userNo
            //            where a.id == id && a.pwd == pwd
            //            select new { b.userNo, b.userName, b.email, a.id, a.role, a.ip };

            foreach (var n in query)
            {
                _userAll.userNo = n.userNo;
                _userAll.userName = n.userName;
                _userAll.email = n.email;
                _userAll.phone = n.phone;
                _userAll.regDate = n.regDate;
                _userAll.id = n.id;
                _userAll.salt = n.salt;
                _userAll.role = n.role;
                _userAll.ip = n.ip;
                _userAll.changeCycle = n.changeCycle;
                _userAll.wrongCnt = n.wrongCnt;
                _userAll.loginDate = n.loginDate;
                _userAll.logoutDate = n.logoutDate;
            }

            return _userAll;
        }

        public UserAll GetSelectUser(int userNo)
        {
            var query = _dataContext.UserLogins
                .Join(_dataContext.UserInfos,
                    t1 => t1.userNo,
                    t2 => t2.userNo,
                    (t1, t2) => new { t1, t2 })
                .Where(p => p.t1.userNo == userNo && p.t1.activityYN == 'Y')
                .Select(p => new
                {
                    p.t2.userNo,
                    p.t2.userName,
                    p.t2.email,
                    p.t2.phone,
                    p.t2.regDate,
                    p.t1.id,
                    p.t1.salt,
                    p.t1.role,
                    p.t1.ip,
                    p.t1.changeCycle,
                    p.t1.wrongCnt,
                    p.t1.loginDate,
                    p.t1.logoutDate
                });

            //var query = from a in _dataContext.UserLogins
            //            join b in _dataContext.UserInfos on a.userNo equals b.userNo
            //            where a.id == id && a.pwd == pwd
            //            select new { b.userNo, b.userName, b.email, a.id, a.role, a.ip };

            foreach (var n in query)
            {
                _userAll.userNo = n.userNo;
                _userAll.userName = n.userName;
                _userAll.email = n.email;
                _userAll.phone = n.phone;
                _userAll.regDate = n.regDate;
                _userAll.id = n.id;
                _userAll.salt = n.salt;
                _userAll.role = n.role;
                _userAll.ip = n.ip;
                _userAll.changeCycle = n.changeCycle;
                _userAll.wrongCnt = n.wrongCnt;
                _userAll.loginDate = n.loginDate;
                _userAll.logoutDate = n.logoutDate;
            }

            return _userAll;
        }

        public UserLogin GetSelectUserLogin(string id)
        {
            return _dataContext.UserLogins.SingleOrDefault(p => p.id == id);
        }

        public void SetLoginDate(UserAll model)
        {
            var tmpRow = _dataContext.UserLogins.Where(p => p.userNo == model.userNo && p.activityYN == 'Y').SingleOrDefault();
            if (tmpRow != null)
            {
                tmpRow.loginDate = model.loginDate;
                tmpRow.logoutDate = model.logoutDate;
                _dataContext.SaveChanges();
            }
        }

    }
}