using doCore.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace do_Http.extdefine
{
    public abstract class do_Http_MAbstract : doMultitonModule
    {
        protected do_Http_MAbstract()
            : base()
        {

        }
        /// <summary>
        /// 初始化
        /// </summary>
        public override void OnInit()
        {
            base.OnInit();
            //注册属性
        }
    }
}
