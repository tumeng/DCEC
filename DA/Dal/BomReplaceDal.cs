using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Entity;

namespace Rmes.DA.Dal
{
    internal class BomReplaceDal : BaseDalClass
    {
        public void MW_MODIFY_SJBOMTHSET(string func1, string in_oldpart, string in_newpart, string in_so, string in_pefile, string in_site, string in_createuser, string in_usetime, string in_endtime, string in_group, string in_sl, string in_xl)
        {
            db.Execute("call MW_MODIFY_SJBOMTHSET(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11)", func1, in_oldpart, in_newpart, in_so, in_pefile, in_site, in_createuser, in_usetime, in_endtime, in_group, in_sl, in_xl);
        }
        public void MW_INSERT_SJBOMSOTHMUTI(string FUNC1, string SO1, string LJDM11, string LJDM22, string YGMC1, string ZDMC1, string JHDM1, string GW1, string GW2, string BZ1, string GZDD1, string thgroup1, string num1, string gxmc11)
        {
            db.Execute("call MW_INSERT_SJBOMSOTHMUTI(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13)", FUNC1, SO1, LJDM11, LJDM22, YGMC1, ZDMC1, JHDM1, GW1, GW2, BZ1, GZDD1, thgroup1, num1, gxmc11);
        }
        public void MW_MODIFY_SJBOMTHMUTICFM(string FUNC1, string JHDM1, string so1, string thegroup1, string tjyh1, string gzdd1)
        {
            db.Execute("call MW_MODIFY_SJBOMTHMUTICFM(@0,@1,@2,@3,@4,@5)", FUNC1, JHDM1, so1, thegroup1, tjyh1, gzdd1);
        }
        public void MW_MODIFY_SJBOMTHMUTICFM_DEL(string FUNC1, string JHDM1, string so1, string thegroup1, string tjyh1, string gzdd1)
        {
            db.Execute("call MW_MODIFY_SJBOMTHMUTICFM_DEL(@0,@1,@2,@3,@4,@5)", FUNC1, JHDM1, so1, thegroup1, tjyh1, gzdd1);
        }
        public void MW_MODIFY_SJBOMTHCFM_DEL(string FUNC1, string JHDM1, string so1, string ljdm11, string ljdm22, string tjyh1, string gwdm1, string gzdd1)
        {
            db.Execute("call MW_MODIFY_SJBOMTHCFM_DEL(@0,@1,@2,@3,@4,@5,@6,@7)", FUNC1, JHDM1, so1, ljdm11, ljdm22, tjyh1, gwdm1, gzdd1);
        }
        public void PL_INSERT_SJBOMSOTH(string FUNC1, string SO1, string LJDM11, string LJDM22, string RQSJ1, string BCMC1, string BZMC1, string YGMC1, string ZDMC1, string JHDM1, string LRSJ1, string GWMC1, string THSL1, string BZ1, string ISLIMIT1, string GZDD1)
        {
            db.Execute("call PL_INSERT_SJBOMSOTH(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15)", FUNC1, SO1, LJDM11, LJDM22, RQSJ1, BCMC1, BZMC1, YGMC1, ZDMC1, JHDM1, LRSJ1, GWMC1, THSL1, BZ1, ISLIMIT1, GZDD1);
        }
        public void MW_MODIFY_SJBOMTHCFM(string FUNC1, string JHDM1, string so1, string ljdm11, string ljdm22, string tjyh1, string gwdm1, string gzdd1)
        {
            db.Execute("call MW_MODIFY_SJBOMTHCFM(@0,@1,@2,@3,@4,@5,@6,@7)", FUNC1, JHDM1, so1, ljdm11, ljdm22, tjyh1, gwdm1, gzdd1);
        }
        public void QAD_CREATE_PRTPART(string FUNC1, string GZDD1, string JHSO1, string JHDM1, string SL1, string THISUSER1)
        {
            db.Execute("call QAD_CREATE_PRTPART(@0,@1,@2,@3,@4,@5)", FUNC1, GZDD1, JHSO1, JHDM1, SL1, THISUSER1);
        }
        public void QAD_CREATE_MOVEPART(string GZDD1, string JHSO1, string JHDM1, string FDJSL1)
        {
            db.Execute("call QAD_CREATE_MOVEPART(@0,@1,@2,@3)", GZDD1, JHSO1, JHDM1, FDJSL1);
        }
        public void QAD_CREATE_MOVEGZ(string GZDD1, string JHSO1, string JHDM1, string FDJSL1)
        {
            db.Execute("call QAD_CREATE_MOVEGZ(@0,@1,@2,@3)", GZDD1, JHSO1, JHDM1, FDJSL1);
        }
        public void QAD_CREATE_PRTPART_LQLL(string FUNC1, string GZDD1, string JHSO1, string JHDM1, string SL1, string THISUSER1)
        {
            db.Execute("call QAD_CREATE_PRTPART_LQLL(@0,@1,@2,@3,@4,@5)", FUNC1, GZDD1, JHSO1, JHDM1, SL1, THISUSER1);
        }
        public void MW_CREATE_BOMCOMPRST(string FUNC1, string SO1, string JHDM1, string SO2, string JHDM2, string USER1)
        {
            db.Execute("call MW_CREATE_BOMCOMPRST(@0,@1,@2,@3,@4,@5)", FUNC1, SO1, JHDM1, SO2, JHDM2, USER1);
        }
        public void QAD_CREATE_MOVEPART_LQ1(string GZDD1, string BILLCODE1)
        {
            db.Execute("call QAD_CREATE_MOVEPART_LQ1(@0,@1)", GZDD1, BILLCODE1);
        }
        public void QAD_CREATE_MOVEPART_LQ2(string GZDD1, string JHSO1, string JHDM1, string FDJSL1)
        {
            db.Execute("call QAD_CREATE_MOVEPART_LQ2(@0,@1,@2,@3)", GZDD1, JHSO1, JHDM1, FDJSL1);
        }
        public void QAD_CREATE_MOVEPARTCAL(string GZDD1, string JHSO1, string JHDM1, string FDJSL1, string MACHINENAME1)
        {
            db.Execute("call QAD_CREATE_MOVEPARTCAL(@0,@1,@2,@3,@4)", GZDD1, JHSO1, JHDM1, FDJSL1, MACHINENAME1);
        }
        public void QAD_CREATE_PRTPART_LQ(string FDJSL1, string MACHINENAME1)
        {
            db.Execute("call QAD_CREATE_PRTPART_LQ(@0,@1)", FDJSL1, MACHINENAME1);
        }
    }
}
