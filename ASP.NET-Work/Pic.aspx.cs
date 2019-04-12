using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;//
using System.Data.SqlClient;//

namespace ASP.NET_Work
{
    public partial class Pic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)                           //判断是否第一次加载页面
            {
                byte[] picData;                             //以字节数组的方式存储获取的图片数据
                string id = Request.QueryString["id"];      //获取传入的参数
                if (!CheckParameter(id, out picData))       //参数验证
                {
                    Response.Write("<script>alert('没有可以显示的照片。')</script>");
                }
                else
                {
                    Response.ContentType = "application/octet-stream";      //设置页面的输出类型
                    Response.BinaryWrite(picData);          //以二进制输出图片数据
                    Response.End();                         //清空缓冲，停止页面执行
                }
            }
        }

        private bool CheckParameter(string id, out byte[] picData)
        {
            picData = null;
            if (string.IsNullOrEmpty(id))                   //判断传入参数是否为空
            {
                return false;
            }
            //从配置文件中获取连接字符串，此字符串可以由数据源控件自动生成，在下面的页面中介绍
            string connStr = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            string query = string.Format("select ZP from XS where XM ='{0}'", id);
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                object data = cmd.ExecuteScalar();          //根据参数获取数据
                if (Convert.IsDBNull(data) || data == null) //如果照片字段为空或者无返回值
                {
                    return false;
                }
                else
                {
                    picData = (byte[])data;
                    return true;
                }
            }
            finally
            {
                conn.Close();                               //关闭数据库连接
            }
        }
    }
}