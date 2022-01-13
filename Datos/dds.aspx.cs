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
    public partial class dds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int sem = Funciones.GetWeekNumber();
                date1.Value = (Request.QueryString["fecha"] != null) ? Request.QueryString["fecha"].ToString() : DateTime.Now.ToString("yyyy") + "-W" + (sem < 10 ? "0" + sem : sem.ToString());
                int semana = Funciones.getWeek(date1.Value);
                string fecha = Funciones.dayOfWeek(date1.Value);

                StringBuilder sb1 = new StringBuilder();
                
                int desplazaSem = 0;
                string connStrCrypt = ConfigurationManager.ConnectionStrings["SQLConn"].ConnectionString;
                //string cadena=Cipher.Decrypt("zH6yzH99uG94MV7JLMSEvCFFcYJGojn8s9XRTXGoptj2Rs9/LvN97bcVqBnXE3wV/ncgnncmBIzIs7ClvBcZQeqMW02Kn3BmulCHsPh1kqgghbWGuKm7I+MKUJUIEGxHsEiPnweFskBE60gAM85UHA4Tjhn3DUpK2nivZ6d5mkHMdGpvUXPixqMjqxlKqTB4MNmY6M1NeDoeB5acNmCQzA==","Pims.2020");
                //string cadena = Cipher.Encrypt("Data Source=PC2;Initial Catalog=EmployeeDB;User ID=sa;Password=pims.2016;MultipleActiveResultSets=True;", "Pims.2020");
                string connStr = Cipher.Decrypt(connStrCrypt, "Pims.2020");
                //Console.Write(connStr);
                SqlConnection sqlConn = new SqlConnection(connStr);
                SqlCommand sqlcmd;
                sqlcmd = new SqlCommand("select isnull((Select val1 from MetersConfig where idMetersConfig=8),0)", sqlConn);
                sqlConn.Open();
                desplazaSem = Convert.ToInt32(sqlcmd.ExecuteScalar().ToString());

                sqlcmd = new SqlCommand("exec sp_getDatosDDSSem_v2 '" + fecha + "'", sqlConn);
                SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                StringBuilder sb = new StringBuilder();

                // Gets the Calendar instance associated with a CultureInfo.
                CultureInfo myCI = new CultureInfo("es-MX");
                System.Globalization.Calendar myCal = myCI.Calendar;

                // Gets the DTFI properties required by GetWeekOfYear.
                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th align=\"center\" rowspan=\"2\" colspan=\"2\"></th>");
                sb.Append("<th align=\"center\" colspan=\"7\" style=\"background-color:#FFF2CC\">SEMANA " + semana + "</th>");
                sb.Append("</tr><tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    if ((dc.ColumnName.ToUpper() != "ORDENIND") && (dc.ColumnName.ToUpper() != "INDICADOR") && (dc.ColumnName.ToUpper() != "CLASE") && (dc.ColumnName.ToUpper() != "TIPO") && (dc.ColumnName.ToUpper() != "VALOR") && (dc.ColumnName.ToUpper() != "HEAD") && (dc.ColumnName.ToUpper() != "DETALLE"))
                    {
                        sb.Append("<th style=\"backgorund-color:#DBDBDB\">");
                        sb.Append(dc.ColumnName.ToString());
                        sb.Append("</th>");
                    }
                }
                sb.Append("</tr></thead>");
                sb.Append("<tbody>");
                int flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;

                    int val = 0;
                    bool res = Int32.TryParse(dr[2].ToString(), out val);
                    if (val == 1) //clase titulo
                    {
                        sb.Append("<tr>");
                        if (dr[1].ToString().ToUpper() == "SEPARADOR")
                        {
                            sb.Append("<td colspan=\"2\" style=\"text-align:center; color: #" + dr[5].ToString() + "; background-color: #" + dr[5].ToString() + "\">");
                            sb.Append("S");
                        }
                        else
                        {
                            sb.Append("<td colspan=\"2\" style=\"text-align:center; color: #000000; background-color: #" + dr[5].ToString() + "\">");
                            sb.Append(dr[1].ToString());
                        }
                        sb.Append("</td>");
                        sb.Append("<td colspan=\"7\" style=\"text-align:right; color: #000000; background-color: #" + dr[6].ToString() + "\">");
                        sb.Append("</td>");
                    }
                    else
                    {
                        int val2 = 0;
                        res = Int32.TryParse(dr[3].ToString(), out val2);
                        if(val2==1)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style=\"text-align:left; color: #000000; background-color: #" + dr[5].ToString() + "\">");
                            sb.Append(dr[1].ToString());
                            sb.Append("</td>");
                            sb.Append("<td style=\"text-align:right; color: #000000; background-color: #" + dr[5].ToString() + "\">");
                            Double val3 = 0;
                            res = Double.TryParse(dr[4].ToString(), out val3);
                            if(val3>0)
                                sb.Append(dr[4].ToString());
                            sb.Append("</td>");
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td colspan=\"2\" style=\"text-align:center; color: #000000; background-color: #" + dr[5].ToString() + "\">");
                            sb.Append(dr[1].ToString());
                            sb.Append("</td>");
                        }
                        for (int j = 1; j < 8; j++)
                        {
                            Double dato = 0;
                            res = Double.TryParse(dr[j+6].ToString(), out dato);
                            if (dato > 0)
                            {
                                Double dato2 = 0;
                                res = Double.TryParse(dr[4].ToString(), out dato2);
                                if ((dato2>0) && (dato>dato2))
                                    sb.Append("<td style=\"text-align:right; color:#000000; background-color: #FFC7CE\">");
                                else
                                    sb.Append("<td style=\"text-align:right; color:#000000; background-color: #" + dr[6].ToString() + "\">");
                                sb.Append(dato.ToString("#,##0.00"));
                            }
                            else
                                sb.Append("<td style=\"text-align:right; color:#000000; background-color: #" + dr[6].ToString() + "\">");

                            sb.Append("</td>");
                        }
                    }
                    sb.Append("</tr>");
                }
                sqlConn.Close();
                Panel1.Controls.Add(new Label { Text = sb.ToString() });

            }
            }
        }
}