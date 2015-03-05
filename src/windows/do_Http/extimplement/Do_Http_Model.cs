using doCore.Helper.JsonParse;
using doCore.Interface;
using doCore.Object;
using do_Http.extdefine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doCore;
using System.Net.Http;
using doCore.Helper;

namespace do_Http.extimplement
{
    public class do_Http_Model : do_Http_MAbstract, do_Http_IMethod
    {
        public do_Http_Model():base()
        {

        }
        public override void OnInit()
        {
            base.OnInit();
			this.RegistProperty(new doProperty("body", PropertyDataType.String, "", true));
			this.RegistProperty(new doProperty("contentType", PropertyDataType.String, "text/html", true));
			this.RegistProperty(new doProperty("method", PropertyDataType.String, "get", true));
			this.RegistProperty(new doProperty("timeout", PropertyDataType.Number, "5000", true));
			this.RegistProperty(new doProperty("url", PropertyDataType.String, "", true));

        }
       private void doRequest(doInvokeResult _invokeResult)
        {
            string method = GetPropertyValue("method");
            if (null == method || "".Equals(method))
            {
                throw new Exception("请求类型方式失败，method：" + method);
            }
            String url = GetPropertyValue("url");
            if (null == url || "".Equals(url))
            {
                throw new Exception("请求地址错误，url：" + url);
            }
            int timeout = doTextHelper.StrToInt(GetPropertyValue("timeout"), 5000);
            if ("post".Equals(method))
            {
                String contentType = GetPropertyValue("contentType");
                if (null == contentType || "".Equals(contentType))
                {
                    contentType = "text/html";
                }
                String body = GetPropertyValue("body");
                doPost(url, body, contentType, timeout, _invokeResult);
            }
            else if ("get".Equals(method))
            {
                doGet(url, timeout, _invokeResult);
            }

        }
        private async void doGet(string url, int timeout, doInvokeResult _invokeResult)
        {
            try
            {

                string data = "";
                System.Net.Http.HttpClient _request = new System.Net.Http.HttpClient();
                _request.Timeout = new TimeSpan(0, 0, timeout / 1000);
                System.Net.Http.HttpResponseMessage _response = await _request.GetAsync(url);
                if (_response.IsSuccessStatusCode)
                {
                    data = await _response.Content.ReadAsStringAsync();
                    _invokeResult.SetResultText(data);
                }
                else
                {
                    _invokeResult.SetError(_response.StatusCode.ToString());
                }
                EventCenter.FireEvent("response", _invokeResult);
            }
            catch (Exception _err)
            {
                doServiceContainer.LogEngine.WriteError("doHttp doGet \n", _err);
            }
        }
        private async void doPost(string url, string body, string datatype, int timeout, doInvokeResult _invokeResult)
        {
            try
            {

                string data = "";

                System.Net.Http.HttpClient _request = new System.Net.Http.HttpClient();
                _request.Timeout = new TimeSpan(0, 0, timeout / 1000);
                StringContent str;
                if (datatype.Equals("json"))
                {
                    str = new StringContent(body, Encoding.UTF8, "application/json");
                    _request.DefaultRequestHeaders.Add("Accept", "application/json");
                }
                else if (datatype.Equals("text"))
                {
                    str = new StringContent(body, Encoding.UTF8, "text/plain");
                    _request.DefaultRequestHeaders.Add("Accept", "text/plain");
                }
                else
                {
                    str = new StringContent(body, Encoding.UTF8, datatype);
                    _request.DefaultRequestHeaders.Add("Accept", datatype);
                }
                System.Net.Http.HttpResponseMessage _response = await _request.PostAsync(url, str);
                if (_response.IsSuccessStatusCode)
                {
                    data = await _response.Content.ReadAsStringAsync();
                    _invokeResult.SetResultText(data);
                }
                else
                {
                    _invokeResult.SetError(_response.StatusCode.ToString());
                }
                EventCenter.FireEvent("response", _invokeResult);
            }
            catch (Exception _err)
            {
                doServiceContainer.LogEngine.WriteError("doHttp doPost \n", _err);
            }
        }
        public override async Task<bool> InvokeAsyncMethod(string _methodName, doCore.Helper.JsonParse.doJsonNode _dictParas, doCore.Interface.doIScriptEngine _scriptEngine, string _callbackFuncName)
        {
            doInvokeResult _invokeResult = _scriptEngine.CreateInvokeResult(this.UniqueKey);
            return await base.InvokeAsyncMethod(_methodName, _dictParas, _scriptEngine, _callbackFuncName);
        }
        public override bool InvokeSyncMethod(string _methodName, doCore.Helper.JsonParse.doJsonNode _dictParas, doCore.Interface.doIScriptEngine _scriptEngine, doInvokeResult _invokeResult)
        {
            if ("request".Equals(_methodName))
            {
                this.doRequest(_invokeResult);
                return true;
            }
            return base.InvokeSyncMethod(_methodName, _dictParas, _scriptEngine, _invokeResult);
        }
        public override void Dispose()
        {
            base.Dispose();

        }
    }
    
}
