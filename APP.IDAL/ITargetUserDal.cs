using APP.MODEL;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.IDAL
{
    public interface ITargetUserDal
    {
        /// <summary>
        /// companyCD로 리스트 조회
        /// </summary>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        List<TargetUser> GetList_TargetUser(int companyCD);
        /// <summary>
        /// seq로 해당 TargetUser 조회
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        TargetUser Get_TargetUser(int seq);
        /// <summary>
        /// TargetUser 등록
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Set_TargetUser(TargetUser model);
        /// <summary>
        /// 둘이상의 TargetUser 등록(json <-> mssql)
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        int SetJson_TargetUser(string json);
        /// <summary>
        /// TargetUser 수정
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Up_TargetUser(TargetUser model);
        /// <summary>
        /// TargetUser onoff 값 Y -> N
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        int Del_TargetUser(int seq);

    }
}
