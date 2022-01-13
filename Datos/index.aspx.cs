using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Datos
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int sem = Funciones.GetWeekNumber();
                date1.Value = (Request.QueryString["fecha"] != null) ? Request.QueryString["fecha"].ToString() : DateTime.Now.ToString("yyyy") + "-W" + (sem < 10 ? "0" + sem : sem.ToString());
                //int semana = Funciones.getWeek(date1.Value);
                string fecha = Funciones.dayOfWeek(date1.Value);

                string connStrCrypt = ConfigurationManager.ConnectionStrings["SQLConn"].ConnectionString;
                //string cadena=Cipher.Decrypt("zH6yzH99uG94MV7JLMSEvCFFcYJGojn8s9XRTXGoptj2Rs9/LvN97bcVqBnXE3wV/ncgnncmBIzIs7ClvBcZQeqMW02Kn3BmulCHsPh1kqgghbWGuKm7I+MKUJUIEGxHsEiPnweFskBE60gAM85UHA4Tjhn3DUpK2nivZ6d5mkHMdGpvUXPixqMjqxlKqTB4MNmY6M1NeDoeB5acNmCQzA==","Pims.2020");
                //string cadena = Cipher.Encrypt("Data Source=PC4\\SQL2K16;Initial Catalog=EMDashboard;User ID=sa;Password=pims.2016;MultipleActiveResultSets=True;", "Pims.2020");
                //string cadena = Cipher.Encrypt("Data Source=Z1GLMXMYVLA11;Initial Catalog=EMDashboard;User ID=emetrix;Password=3m3tr1x2019;MultipleActiveResultSets=True;", "Pims.2020");
                string connStr = Cipher.Decrypt(connStrCrypt, "Pims.2020");
                //Console.Write(connStr);
                SqlConnection sqlConn = new SqlConnection(connStr);
                SqlCommand sqlcmd;
                sqlcmd = new SqlCommand("exec sp_getDatos_v2 '"+ fecha+"',0", sqlConn);
                sqlConn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                StringBuilder sb = new StringBuilder();
                sb.Append("<thead>");
                sb.Append("<tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    if ((dc.ColumnName.ToUpper() != "ORDENAREA") && (dc.ColumnName.ToUpper() != "ORDENIND"))
                    {
                        sb.Append("<th>");
                        sb.Append(dc.ColumnName.ToString());
                        sb.Append("</th>");
                    }
                }
                sb.Append("</tr></thead>");
                sb.Append("<tbody>");
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if ((dc.ColumnName.ToUpper() != "ORDENAREA") && (dc.ColumnName.ToUpper() != "AREA") && (dc.ColumnName.ToUpper() != "ORDENIND") && (dc.ColumnName.ToUpper() != "INDICADOR") && (dc.ColumnName.ToUpper() != "UNIDAD"))
                        {
                            sb.Append("<td style=\"text-align:right\">");
                            Double val;
                            bool res;
                            res = Double.TryParse(dr[dc.ColumnName].ToString(), out val);
                            if (val > 0)
                            {
                                sb.Append(val.ToString("#,##0.00"));
                            }
                            sb.Append("</td>");
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "AREA") || (dc.ColumnName.ToUpper() == "INDICADOR") || (dc.ColumnName.ToUpper() == "UNIDAD"))
                            {
                                sb.Append("<td style=\"text-align:left\">");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</td>");
                            }
                        }
                    }
                    sb.Append("</tr>");
                }
                sb.Append("</tbody>");
                //sb.Append("<tfoot>");
                //sb.Append("<tr>");
                //foreach (DataColumn dc in dt.Columns)
                //{
                //    if ((dc.ColumnName.ToUpper() != "ORDENAREA") && (dc.ColumnName.ToUpper() != "ORDENIND"))
                //    {
                //        sb.Append("<th>");
                //        sb.Append(dc.ColumnName.ToString());
                //        sb.Append("</th>");
                //    }
                //}
                //sb.Append("</tr></tfoot>");
                sqlConn.Close();
                Panel1.Controls.Add(new Label { Text = sb.ToString() });
            }
        }
    }
}