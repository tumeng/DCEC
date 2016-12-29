using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rmes.DA.Base;
using Rmes.DA.Dal;
using Rmes.DA.Entity;
using Rmes.DA.Factory;

namespace Rmes.DA.Base
{
    public static class CsGlobalClass
    {
        static string _oldsn;
        static string _oldso;
        static string _oldjx;
        static string _fdjxl;
        static string _newsn;
        static bool _needveps;
        static string _wjlj;
        static string _gwmc;
        static string _newplancode;
        static bool _dgsm;//返修点 顶岗操作线上站点，不记录顶岗的模块完成情况也不进行判断，防止影响线上扫描
        static bool _qxsjsm;//BOM二次扫描弹出窗体如果取消不再记录到本地文本中
        public static bool QXSJSM
        {
            get { return _qxsjsm; }
            set { _qxsjsm = value; }
        }
        public static bool NEEDVEPS
        {
            get { return _needveps; }
            set { _needveps = value; }
        }
        public static bool DGSM
        {
            get { return _dgsm; }
            set { _dgsm = value; }
        }
        public static string OLDSN
        {
            get { return _oldsn; }
            set { _oldsn = value; }
        }
        public static string OLDSO
        {
            get { return _oldso; }
            set { _oldso = value; }
        }
        public static string OLDJX
        {
            get { return _oldjx; }
            set { _oldjx = value; }
        }
        public static string FDJXL
        {
            get { return _fdjxl; }
            set { _fdjxl = value; }
        }
        public static string NEWSN
        {
            get { return _newsn; }
            set { _newsn = value; }
        }
        public static string NEWPLANCODE
        {
            get { return _newplancode; }
            set { _newplancode = value; }
        }
        public static string WJLJ
        {
            get { return _wjlj; }
            set { _wjlj = value; }
        }
        public static string GWMC
        {
            get { return _gwmc; }
            set { _gwmc = value; }
        }
    }
}
