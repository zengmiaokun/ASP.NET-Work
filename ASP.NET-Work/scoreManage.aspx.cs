using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;//
using System.Configuration;//
using System.Data;//

namespace ASP.NET_Work
{
    public partial class scoreManage : System.Web.UI.Page
    {
        private string myConstr = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
        private string mySql = "";
        private SqlConnection myCon = null;
        private SqlDataAdapter myDa = null;
        private SqlDataReader myDr = null;
        private SqlCommand myCmd = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                myCon = new SqlConnection(myConstr);
                myCon.Open();
                mySql = "declare xm_indicator cursor dynamic for select distinct(XM) from XS open xm_indicator";
                myCmd = new SqlCommand(mySql, myCon);
                myCmd.ExecuteNonQuery();
                refresh();
            }
            catch
            {
                return;
            }
        }

        private void refresh()
        {
            mySql = "select XM, KCM, CJ from CJ";
            myDa = new SqlDataAdapter(mySql, myCon);
            DataSet ds = new DataSet();
            myDa.Fill(ds, "CJDATA");
            GridView1.DataSource = ds;
            GridView1.DataBind();
            adjust_CURSOR();
        }

        protected void Button_AddCj_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = exec_AddDelPROC(DropDownList_XM.Text.Trim(), TextBox_KCM.Text, int.Parse(TextBox_CJ.Text), 1);
                Label_MSG.Text = msg;
            }
            catch
            {
                return;
            }
        }

        protected void Button_DelCj_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = exec_AddDelPROC(DropDownList_XM.Text.Trim(), TextBox_KCM.Text, int.Parse(TextBox_CJ.Text), 2);
                Label_MSG.Text = msg;
            }
            catch
            {
                return;
            }
        }

        private string exec_AddDelPROC(string name, string course, int score, int op)
        {
            myCmd = new SqlCommand();
            myCmd.Connection = myCon;
            myCmd.CommandType = CommandType.StoredProcedure;
            myCmd.CommandText = "CJ_AddDel_PROC";
            SqlParameter sqlXm = myCmd.Parameters.Add("@xm", SqlDbType.Char, 8);
            sqlXm.Direction = ParameterDirection.Input;
            sqlXm.Value = name;
            SqlParameter sqlKcm = myCmd.Parameters.Add("@kcm", SqlDbType.Char, 12);
            sqlKcm.Direction = ParameterDirection.Input;
            sqlKcm.Value = course;
            SqlParameter sqlCj = myCmd.Parameters.Add("@cj", SqlDbType.Int);
            sqlCj.Direction = ParameterDirection.Input;
            sqlCj.Value = score;
            SqlParameter sqlSel = myCmd.Parameters.Add("@SEL", SqlDbType.Int);
            sqlSel.Direction = ParameterDirection.Input;
            sqlSel.Value = op;
            SqlParameter sqlRS = myCmd.Parameters.Add("@RS", SqlDbType.Char, 20);
            sqlRS.Direction = ParameterDirection.Output;
            myCmd.ExecuteNonQuery();
            refresh();
            return sqlRS.Value.ToString();
        }

        protected void Button_QueCj_Click(object sender, EventArgs e)
        {
            try
            {
                mySql = "EXEC CJ_View_PROC '" + DropDownList_XM.Text.Trim() + "','" + TextBox_KCM.Text + "'";
                myCmd = new SqlCommand(mySql, myCon);
                myCmd.ExecuteNonQuery();
                mySql = "select XM, KCM, CJ from CJ_VIEW";
                myDa = new SqlDataAdapter(mySql, myCon);
                DataSet ds = new DataSet();
                myDa.Fill(ds, "CJVIEW");
                GridView1.DataSource = ds;
                GridView1.DataBind();
                Label_MSG.Text = "";
            }
            catch
            {
                return;
            }
        }

        protected void Button_Dn_Click(object sender, EventArgs e)
        {
            try
            {
                string curname = fetch_CURSOR("next");
                if (curname == "") curname = fetch_CURSOR("first");
                DropDownList_XM.Text = curname;
            }
            catch
            {
                return;
            }
        }

        protected void Button_Up_Click(object sender, EventArgs e)
        {
            try
            {
                string curname = fetch_CURSOR("prior");
                if (curname == "") curname = fetch_CURSOR("last");
                DropDownList_XM.Text = curname;
            }
            catch
            {
                return;
            }
        }

        private string fetch_CURSOR(string pos)
        {
            try
            {
                mySql = "fetch " + pos + " from xm_indicator";
                myCmd = new SqlCommand(mySql, myCon);
                myDr = myCmd.ExecuteReader();
                if (myDr.Read()) return myDr[0].ToString();
                return "";
            }
            catch
            {
                return "";
            }
            finally
            {
                if (myDr != null)
                {
                    myDr.Close();
                    myDr = null;
                }
            }
        }

        private void adjust_CURSOR()
        {
            try
            {
                mySql = "close xm_indicator open xm_indicator";
                myCmd = new SqlCommand(mySql, myCon);
                myCmd.ExecuteNonQuery();
                string curname = fetch_CURSOR("first");
                while (curname != DropDownList_XM.Text && curname != "") curname = fetch_CURSOR("next");
            }
            catch
            {
                return;
            }
        }

        protected void DropDownList_XM_SelectedIndexChanged(object sender, EventArgs e)
        {
            adjust_CURSOR();
        }

        protected void DropDownList_XM_TextChanged(object sender, EventArgs e)
        {
            adjust_CURSOR();
        }
    }
}