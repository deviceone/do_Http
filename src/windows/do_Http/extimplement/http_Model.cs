using doCore.Object;
using do_Http.extdefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace do_Http.extimplement
{
    /// <summary>
    /// 自定义扩展组件Model实现，继承@TYPEID_MAbstract抽象类；
    /// </summary>
    public class http_Model : http_MAbstract
    {
        public http_Model()
            : base()
        {
        }
        public override void OnInit()
        {
            base.OnInit();
            this.RegistProperty(new doProperty("text", PropertyDataType.String, "", false));
            this.RegistProperty(new doProperty("fontColor", PropertyDataType.String, "000000FF", false));
            this.RegistProperty(new doProperty("textAlign", PropertyDataType.String, "left", true));
            this.RegistProperty(new doProperty("fontStyle", PropertyDataType.String, "normal", false));
            this.RegistProperty(new doProperty("fontSize", PropertyDataType.Number, "9", false));
            this.RegistProperty(new doProperty("autoSize", PropertyDataType.Bool, "false", true));
            this.RegistProperty(new doProperty("maxWidth", PropertyDataType.Number, "100", true));
            this.RegistProperty(new doProperty("maxHeight", PropertyDataType.Number, "100", true));
            this.RegistProperty(new doProperty("maxLines", PropertyDataType.Number, "3", true));
        
        }
        public override async Task<bool> InvokeAsyncMethod(string _methodName, doCore.Helper.JsonParse.doJsonNode _dictParas, doCore.Interface.doIScriptEngine _scriptEngine, string _callbackFuncName)
        {
            if (await base.InvokeAsyncMethod(_methodName, _dictParas, _scriptEngine, _callbackFuncName)) return true;
            return false;
        }
        public override bool InvokeSyncMethod(string _methodName, doCore.Helper.JsonParse.doJsonNode _dictParas, doCore.Interface.doIScriptEngine _scriptEngine, doCore.Object.doInvokeResult _invokeResult)
        {
            if (base.InvokeSyncMethod(_methodName, _dictParas, _scriptEngine, _invokeResult)) return true;
            return false;
        }
    }
}
