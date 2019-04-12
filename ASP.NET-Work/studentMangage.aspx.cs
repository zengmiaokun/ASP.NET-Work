using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;//
using System.Data;//
using System.IO;//
using System.Configuration;//

namespace ASP.NET_Work
{
    public partial class studentMangage : System.Web.UI.Page
    {
        private string myConstr = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
        private string mySql = "";
        private SqlConnection myCon = null;
        private SqlDataReader myDr = null;
        private SqlCommand myCmd = null;
        //protected string name = ""; 
        protected void Page_Load(object sender, EventArgs e)
        {
            myCon = new SqlConnection(myConstr);
        }
        protected void Button_AddXs_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox_XM.Text == "") return;
                mySql = "insert into XS values(@name,@sex,@birth,0,NULL)";
                myCon.Open();
                myCmd = new SqlCommand(mySql, myCon);//新建操作数据库的命令对象
                myCmd.Parameters.Add("@name", SqlDbType.Char, 8).Value = TextBox_XM.Text.Trim();//姓名
                myCmd.Parameters.Add("@sex", SqlDbType.Bit).Value = RadioButtonList_XB.SelectedValue;//性别
                myCmd.Parameters.Add("@birth", SqlDbType.Date).Value = TextBox_CSSJ.Text.Trim();//出生日期
                myCmd.ExecuteNonQuery();
                if (!string.IsNullOrEmpty(FileUpload_Path.FileName)) upload_ZP();
                myCon.Close();
                Button_AddXs.Enabled = false;
                Button_QueXs_Click(null, null);//查询后回显该生信息
                Label_MSG.Text = "录入成功！";
            }
            catch (Exception ex)
            {
                string errMsg = ex.ToString();
                int errLen = ex.ToString().Length;
                Label_MSG.Text = "发生故障！异常信息如下:\r\n" + errMsg.Substring(0, 150) + "..." + errMsg.Substring(errLen - 15, 15);
                return;
            }
            finally
            {
                if (myCon.State == ConnectionState.Open) myCon.Close();
            }
        }

        protected void Button_DelXs_Click(object sender, EventArgs e)
        {
            try
            {
                mySql = "delete from XS where XM='" + TextBox_XM.Text + "'";
                myCon.Open();
                myCmd = new SqlCommand(mySql, myCon);
                myCmd.ExecuteNonQuery();
                myCon.Close();
                Button_QueXs_Click(null, null);
                Label_MSG.Text = "学生 " + TextBox_XM.Text + "的记录已删。";
            }
            catch
            {
                return;
            }
            finally
            {
                if (myCon.State == ConnectionState.Open) myCon.Close();
            }
        }

        protected void Button_UptXs_Click(object sender, EventArgs e)
        {
            try
            {
                mySql = "update XS set XM='" + TextBox_XM.Text + "',XB='" + RadioButtonList_XB.SelectedValue + "',CSSJ='" + TextBox_CSSJ.Text.Trim() + "' where XM='" + Session["name"] + "'";
                //Label_MSG.Text = mySql;
                myCon.Open();
                myCmd = new SqlCommand(mySql, myCon);
                myCmd.ExecuteNonQuery();
                //Label_MSG.Text = mySql;
                if (!string.IsNullOrEmpty(FileUpload_Path.FileName)) upload_ZP();
                myCon.Close();
                Button_QueXs_Click(null, null);
                Label_MSG.Text = "更新成功！";
            }
            catch
            {
                return;
            }
            finally
            {
                if (myCon.State == ConnectionState.Open) myCon.Close();
            }
        }

        protected void Button_QueXs_Click(object sender, EventArgs e)
        {
            try
            {
                RadioButtonList_XB.SelectedValue = "True";
                TextBox_CSSJ.Text = DateTime.Parse("2000-05-02").ToString("yyyy-MM-dd");
                TextBox_KCS.Text = "0";
                Image_ZP.ImageUrl = "";
                mySql = "select XM, XB, CSSJ, KCS from XS where XM='" + TextBox_XM.Text + "'";
                myCon.Open();
                myCmd = new SqlCommand(mySql, myCon);
                myDr = myCmd.ExecuteReader();
                if (myDr.Read())
                {
                    TextBox_XM.Text = myDr["XM"].ToString();
                    if (Boolean.Parse(myDr["XB"].ToString())) RadioButtonList_XB.SelectedValue = "True";
                    else RadioButtonList_XB.SelectedValue = "False";
                    TextBox_CSSJ.Text = DateTime.Parse(myDr["CSSJ"].ToString()).ToString("yyyy-MM-dd");
                    TextBox_KCS.Text = myDr["KCS"].ToString();
                    Image_ZP.ImageUrl = "Pic.aspx?id=" + TextBox_XM.Text.Trim();
                    Label_MSG.Text = "";
                }
                else
                    TextBox_XM.Text = "";
                //name = TextBox_XM.Text;
                Session["name"] = TextBox_XM.Text;
            }
            catch
            {
                return;
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

        private void upload_ZP()
        {
            mySql = "update XS set ZP=@photo where XM='" + TextBox_XM.Text + "'";
            myCmd = new SqlCommand(mySql, myCon);
            myCmd.Parameters.Add("@photo", SqlDbType.Image).Value = FileUpload_Path.FileBytes;
            myCmd.ExecuteNonQuery();
        }
    }
}