using APP.DAL.APPContext;
using APP.IDAL;
using APP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APP.DAL
{
    public class TargetUserDal : ITargetUserDal
    {

        private readonly DataContext _dataContext;
        private readonly TargetUser _targetUser;

        public TargetUserDal(DataContext dataContext, TargetUser targetUser)
        {
            _dataContext = dataContext;
            _targetUser = targetUser;
        }

        public List<TargetUser> GetList_TargetUser(int companyCD)
        {
            return _dataContext.TargetUsers.Where(p => p.companyCD == companyCD).ToList();
        }

        public TargetUser Get_TargetUser(int seq)
        {
            return _dataContext.TargetUsers.Where(p => p.seq == seq).SingleOrDefault();
        }

        public int SetJson_TargetUser(string json)
        {
            throw new NotImplementedException();
        }

        public bool Set_TargetUser(TargetUser model)
        {
            _dataContext.TargetUsers.Add(model);
            if (_dataContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public int Up_TargetUser(TargetUser model)
        {
            throw new NotImplementedException();
        }

        public int Del_TargetUser(int seq)
        {
            throw new NotImplementedException();
        }
    }
}
