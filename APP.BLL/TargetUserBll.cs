using System;
using System.Collections.Generic;
using System.Text;
using APP.IDAL;
using APP.MODEL;

namespace APP.BLL.TargetUserBll
{
    public class TargetUserBll
    {
        private readonly TargetUser _targetUser;
        private readonly ITargetUserDal _targetUserDal;

        public TargetUserBll(TargetUser targetUser, ITargetUserDal targetUserDal)
        {
            _targetUser = targetUser;
            _targetUserDal = targetUserDal;
        }


        public int Del_TargetUser(int seq)
        {
            throw new NotImplementedException();
        }

        public List<TargetUser> GetList_TargetUser(int companyCD)
        {
            return _targetUserDal.GetList_TargetUser(companyCD);
        }

        public TargetUser Get_TargetUser(int seq)
        {
            return _targetUserDal.Get_TargetUser(seq);
        }

        public int SetJson_TargetUser(string json)
        {
            throw new NotImplementedException();
        }

        public bool Set_TargetUser(TargetUser model)
        {
            throw new NotImplementedException();
        }

        public int Up_TargetUser(TargetUser model)
        {
            throw new NotImplementedException();
        }
    }
}
