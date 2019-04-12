using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;//
using System.Data;//
using System.Configuration;//

namespace ASP.NET_Work
{
    /// <summary>
    /// StudentService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]

    public class Student
    {
        public string Xm { get; set; }
        public int Xb { get; set; }
        public string Cssj { get; set; }
        public int Kcs { get; set; }
        public string Msg { get; set; }
    }
    public class StudentService : System.Web.Services.WebService
    {
        private string myConstr = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
        private string mySql = "";
        private SqlConnection myCon = null;
        private SqlDataReader myDr = null;
        private SqlCommand myCmd = null;

        public StudentService()
        {

            //如果使用设计的组件，请取消注释以下行 
            //InitializeComponent(); 
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public Student query_XS(string xm)
        {
            try
            {
                mySql = "select XM, XB, CSSJ, KCS from XS where XM='" + xm + "'";
                myCon = new SqlConnection(myConstr);
                myCon.Open();
                myCmd = new SqlCommand(mySql, myCon);
                myDr = myCmd.ExecuteReader();
                if (myDr.Read())
                {
                    string name = myDr["XM"].ToString();
                    int sex = bool.Parse(myDr["XB"].ToString()) ? 1 : 0;
                    string birth = DateTime.Parse(myDr["CSSJ"].ToString()).ToString("yyyy-MM-dd");
                    int num = int.Parse(myDr["KCS"].ToString());
                    Student student = new Student()
                    {
                        Xm = name,
                        Xb = sex,
                        Cssj = birth,
                        Kcs = num,
                        Msg = "该生的记录已存在。"
                    };
                    return student;
                }
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (myDr != null)
                {
                    myDr.Close();
                    myDr = null;
                }
                if (myCon.State == ConnectionState.Open) myCon.Close();
            }
        }
    }

}
