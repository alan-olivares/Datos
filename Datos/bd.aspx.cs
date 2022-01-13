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
using System.Diagnostics;

namespace Datos
{
    public partial class bd : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int sem = Funciones.GetWeekNumber();
                date1.Value = (Request.QueryString["fecha"] != null) ? Request.QueryString["fecha"].ToString() : DateTime.Now.ToString("yyyy") + "-W" + (sem < 10 ? "0" + sem : sem.ToString());
                int semana = Funciones.getWeek(date1.Value);
                string fecha = Funciones.dayOfWeek(date1.Value);
                //PanelBtn.Controls.Add(new Label { Text = sb1.ToString() });

                int desplazaSem = 0;
                string connStrCrypt = ConfigurationManager.ConnectionStrings["SQLConn"].ConnectionString;
                //string cadena=Cipher.Decrypt("zH6yzH99uG94MV7JLMSEvCFFcYJGojn8s9XRTXGoptj2Rs9/LvN97bcVqBnXE3wV/ncgnncmBIzIs7ClvBcZQeqMW02Kn3BmulCHsPh1kqgghbWGuKm7I+MKUJUIEGxHsEiPnweFskBE60gAM85UHA4Tjhn3DUpK2nivZ6d5mkHMdGpvUXPixqMjqxlKqTB4MNmY6M1NeDoeB5acNmCQzA==","Pims.2020");                

                string connStr= Cipher.Decrypt(connStrCrypt, "Pims.2020");
                //Console.Write(connStr);
                SqlConnection sqlConn = new SqlConnection(connStr);
                SqlCommand sqlcmd;
                sqlcmd = new SqlCommand("select isnull((Select val1 from MetersConfig where idMetersConfig=8),0)", sqlConn);
                sqlConn.Open();
                desplazaSem = Convert.ToInt32(sqlcmd.ExecuteScalar().ToString());
                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '"+ fecha.ToString() +"',1", sqlConn);
                SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                StringBuilder sb = new StringBuilder();

                // Gets the Calendar instance associated with a CultureInfo.
                CultureInfo myCI = new CultureInfo("es-MX");
                System.Globalization.Calendar myCal = myCI.Calendar;

                // Gets the DTFI properties required by GetWeekOfYear.
                CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;

                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th rowspan=\"2\"></th>");
                sb.Append("<th align=\"center\" rowspan=\"2\" colspan = \"2\"> INDICADORES </th>");
                sb.Append("<th align=\"center\" colspan=\"7\" >SEMANA "+ semana.ToString()+"</th>");
                sb.Append("<th rowspan=\"2\"> WTD </th></tr>");
                sb.Append("<tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    if ((dc.ColumnName.ToUpper() != "ORDENAREA") && (dc.ColumnName.ToUpper() != "AREA") && (dc.ColumnName.ToUpper() != "ORDENIND") && (dc.ColumnName.ToUpper() != "INDICADOR") && (dc.ColumnName.ToUpper() != "UNIDAD"))
                    {
                        sb.Append("<th>");
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
                    if (flag==1)
                        sb.Append("<tr style=\"background-color: #E2EFDA\"><td style=\"color:black; background-color: #FFC000\" rowspan=\"16\"><b>SMD NORTE</b></td>");
                    else 
                        if (flag < 7)
                            sb.Append("<tr style=\"background-color: #E2EFDA\">");
                        else
                            if (flag < 16)
                                sb.Append("<tr style=\"background-color: #C6E0B4\">");
                            else 
                                    sb.Append("<tr style=\"background-color: yellowgreen\">");
                    double total=0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if ((dc.ColumnName.ToUpper() != "ORDENAREA") && (dc.ColumnName.ToUpper() != "AREA") && (dc.ColumnName.ToUpper() != "ORDENIND") && (dc.ColumnName.ToUpper() != "INDICADOR") && (dc.ColumnName.ToUpper() != "UNIDAD"))
                        {
                            sb.Append("<td style=\"text-align:right\">");
                            Double val;
                            bool res;
                            res = Double.TryParse(dr[dc.ColumnName].ToString(), out val);
                            if ( val> 0){
                                sb.Append(val.ToString("#,##0.00"));
                                total += val;
                            }
                            sb.Append("</td>");
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR") || (dc.ColumnName.ToUpper() == "UNIDAD"))
                            {
                                sb.Append("<td style=\"text-align:left\">");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</td>");
                            }
                        }
                    }
                    // Se termina la fila, se agrega totalizado
                    if (flag<7)
                    {
                        sb.Append("<td style=\"text-align:right\">");
                        if (total>0)
                            sb.Append(total.ToString("#,##0.00"));
                        sb.Append("</td>");

                    }
                    sb.Append("</tr>");
                }
                sqlConn.Close();
                sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text=sb.ToString()});

                // SE AGREGA SMD SUR

                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',2", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                        sb.Append("<tr style=\"background-color: #E2EFDA\"><td style=\"color:white; background-color: #4472C4\" rowspan=\"16\"><b>SMD SUR</b></td>");
                    else
                        if (flag < 7)
                        sb.Append("<tr style=\"background-color: #E2EFDA\">");
                    else
                            if (flag < 16)
                        sb.Append("<tr style=\"background-color: #C6E0B4\">");
                    else
                        sb.Append("<tr style=\"background-color: yellowgreen\">");
                    double total = 0;
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
                                total += val;
                            }
                            sb.Append("</td>");
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR") || (dc.ColumnName.ToUpper() == "UNIDAD"))
                            {
                                sb.Append("<td style=\"text-align:left\">");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</td>");
                            }
                        }
                    }
                    // Se termina la fila, se agrega totalizado
                    if (flag < 7)
                    {
                        sb.Append("<td style=\"text-align:right\">");
                        if (total > 0)
                            sb.Append(total.ToString("#,##0.00"));
                        sb.Append("</td>");

                    }
                    sb.Append("</tr>");
                }
                sqlConn.Close();
                sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });

                //Producciones de FineCut y Producción Honduras
                double val2 = 0;
                double val3 = 0;
                double val4 = 0;
                double valor = 0;
                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',20", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                        sb.Append("<tr style=\"background-color: #E2EFDA\"><td style=\"border:none; color:white; background-color: white\" rowspan=\"12\"></td>");
                    else
                        if (flag < 7)
                        sb.Append("<tr style=\"background-color: #E2EFDA\">");
                    else
                    {
                        if (flag == 7)
                            sb.Append("<tr style=\"background-color: white; border:none; height:30px\"><td style=\"border:none\" colspan=\"9\"></td><tr>");
                        sb.Append("<tr style=\"background-color: #FFFF00\">");
                    }
                    double total = 0;
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
                                total += val;
                                sb.Append("</td>");
                            }
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR") || (dc.ColumnName.ToUpper() == "UNIDAD"))
                            {
                                sb.Append("<td style=\"text-align:left\" \">");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</td>");
                            }
                        }
                    }
                        sb.Append("<td style=\"font-weight:bold; text-align:right\">");

                        if (total > 0)
                            sb.Append(total.ToString("#,##0.00"));
                        sb.Append("</td>");
                    sb.Append("</tr>");

                }
                sqlConn.Close();
                //sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });

                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',21", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                        sb.Append("<tr style=\"background-color: #E2EFDA\"><td style=\"border:none; color:white; background-color: white\" rowspan=\"12\"></td>");
                    else
                        if (flag < 7)
                        sb.Append("<tr style=\"background-color: #E2EFDA\">");
                    else
                    {
                        if (flag == 7)
                            sb.Append("<tr style=\"background-color: white; border:none; height:30px\"><td style=\"border:none\" colspan=\"9\"></td><tr>");
                        sb.Append("<tr style=\"background-color: #FFFF00\">");
                    }
                    double total = 0;
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
                                total += val;
                                sb.Append("</td>");
                            }
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR") || (dc.ColumnName.ToUpper() == "UNIDAD"))
                            {
                                sb.Append("<td style=\"text-align:left\" \">");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</td>");
                            }
                        }
                    }
                    sb.Append("<td style=\"font-weight:bold; text-align:right\">");

                    if (total > 0)
                        sb.Append(total.ToString("#,##0.00"));
                    sb.Append("</td>");
                    sb.Append("</tr>");

                }
                sqlConn.Close();
                sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });


                // PMD
                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',4", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                        sb.Append("<tr style=\"background-color: #E2EFDA\"><td style=\"color:white; background-color: #833C0C\" rowspan=\"18\"><b>PMD</b></td>");
                    else
                        if (flag < 11)
                        sb.Append("<tr style=\"background-color: #E2EFDA\">");
                    else
                            if (flag < 18)
                        sb.Append("<tr style=\"background-color: #C6E0B4\">");
                    else
                        sb.Append("<tr style=\"background-color: yellowgreen\">");
                    double total = 0;
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
                                total += val;
                            }
                            sb.Append("</td>");
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR") || (dc.ColumnName.ToUpper() == "UNIDAD"))
                            {
                                sb.Append("<td style=\"text-align:left\">");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</td>");
                            }
                        }
                    }
                    // Se termina la fila, se agrega totalizado
                    if (flag <11)
                    {
                        sb.Append("<td style=\"text-align:right\">");
                        if (total > 0)
                            sb.Append(total.ToString("#,##0.00"));
                        sb.Append("</td>");

                    }
                    sb.Append("</tr>");
                }
                sqlConn.Close();
                sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });


                // FMD
                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',5", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                        sb.Append("<tr style=\"background-color: #E2EFDA\"><td style=\"color:white; background-color: #757171\" rowspan=\"6\"><b>FMD</b></td>");
                    else
                        if (flag < 5)
                            sb.Append("<tr style=\"background-color: #E2EFDA\">");
                        else
                            sb.Append("<tr style=\"background-color: #C6E0B4\">");

                    double total = 0;
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
                                total += val;
                            }
                            sb.Append("</td>");
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR") || (dc.ColumnName.ToUpper() == "UNIDAD"))
                            {
                                sb.Append("<td style=\"text-align:left\">");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</td>");
                            }
                        }
                    }
                    // Se termina la fila, se agrega totalizado
                    if (flag < 5)
                    {
                        sb.Append("<td style=\"text-align:right\">");
                        if (total > 0)
                            sb.Append(total.ToString("#,##0.00"));
                        sb.Append("</td>");

                    }
                    sb.Append("</tr>");
                }
                sqlConn.Close();
                sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });

                // BOILER HOUSE
                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',6", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                        sb.Append("<tr style=\"background-color: #E2EFDA\"><td style=\"color:black; background-color: #FFD966\" rowspan=\"5\"><b>BOILER HOUSE</b></td>");
                    else
                        if (flag < 5)
                        sb.Append("<tr style=\"background-color: #E2EFDA\">");
                    else
                        sb.Append("<tr style=\"background-color: #C6E0B4\">");

                    double total = 0;
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
                                total += val;
                            }
                            sb.Append("</td>");
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR") || (dc.ColumnName.ToUpper() == "UNIDAD"))
                            {
                                sb.Append("<td style=\"text-align:left\">");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</td>");
                            }
                        }
                    }
                    // Se termina la fila, se agrega totalizado
                    if (flag < 6)
                    {
                        sb.Append("<td style=\"text-align:right\">");
                        if (total > 0)
                            sb.Append(total.ToString("#,##0.00"));
                        sb.Append("</td>");

                    }
                    sb.Append("</tr>");
                }
                sqlConn.Close();
                sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });

                // HVAC
                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',7", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                        sb.Append("<tr style=\"background-color: #E2EFDA\"><td style=\"color:black; background-color: #8EA9DB\" rowspan=\"3\"><b>HVAC</b></td>");
                    else
                        sb.Append("<tr style=\"background-color: #E2EFDA\">");

                    double total = 0;
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
                                total += val;
                            }
                            sb.Append("</td>");
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR") || (dc.ColumnName.ToUpper() == "UNIDAD"))
                            {
                                sb.Append("<td style=\"text-align:left\">");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</td>");
                            }
                        }
                    }
                    // Se termina la fila, se agrega totalizado
                        sb.Append("<td style=\"text-align:right\">");
                        if (total > 0)
                            sb.Append(total.ToString("#,##0.00"));
                        sb.Append("</td>");
                    sb.Append("</tr>");
                }
                sqlConn.Close();
                sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });

                // UTILITIES
                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',8", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                        sb.Append("<tr style=\"background-color: #E2EFDA\"><td style=\"color:white; background-color: #548235\" rowspan=\"17\"><b>UTILITIES</b></td>");
                    else
                        if (flag < 10)
                            sb.Append("<tr style=\"background-color: #E2EFDA\">");
                        else
                            sb.Append("<tr style=\"background-color: #C6E0B4\">");

                    double total = 0;
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
                                total += val;
                            }
                            sb.Append("</td>");
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR") || (dc.ColumnName.ToUpper() == "UNIDAD"))
                            {
                                sb.Append("<td style=\"text-align:left\">");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</td>");
                            }
                        }
                    }
                    // Se termina la fila, se agrega totalizado
                    sb.Append("<td style=\"text-align:right\">");
                    if (total > 0)
                        sb.Append(total.ToString("#,##0.00"));
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
                sqlConn.Close();
                sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });

                // GENERAL
                double prodtotal=0;
                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',9", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                        sb.Append("<tr style=\"background-color: #E2EFDA\"><td style=\"color:black; background-color: #0CE235\" rowspan=\"8\"><b>GENERAL</b></td>");
                    else
                        if (flag < 6)
                        sb.Append("<tr style=\"background-color: #E2EFDA\">");
                    else
                        if (flag < 8)
                        sb.Append("<tr style=\"background-color: #FFC000\">");
                    else
                        sb.Append("<tr style=\"background-color: #833C0C\">");

                    double total = 0;
                    int col = 0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if ((dc.ColumnName.ToUpper() != "ORDENAREA") && (dc.ColumnName.ToUpper() != "AREA") && (dc.ColumnName.ToUpper() != "ORDENIND") && (dc.ColumnName.ToUpper() != "INDICADOR") && (dc.ColumnName.ToUpper() != "UNIDAD"))
                        {
                            col++;
                            Double val;
                            bool res;
                            if ((flag == 6) || (flag == 7))
                            {
                                if (col == 1)
                                {
                                    sb.Append("<td style=\"text-align:right\">");
                                    res = Double.TryParse(dr[dc.ColumnName].ToString(), out val);
                                    if (val > 0)
                                    {
                                        sb.Append(val.ToString("#,##0.00"));
                                        total += val;
                                    }
                                    sb.Append("</td>");
                                }
                            }
                            else
                            {
                                if (flag==8)
                                    sb.Append("<td style=\"color:white; text-align:right\">");
                                else 
                                    sb.Append("<td style=\"text-align:right\">");
                                res = Double.TryParse(dr[dc.ColumnName].ToString(), out val);
                                if (val > 0)
                                {
                                    sb.Append(val.ToString("#,##0.00"));
                                    total += val;
                                }
                                sb.Append("</td>");
                            }
                        }
                        else
                        {
                            if (flag < 6)
                            {
                                if ((dc.ColumnName.ToUpper() == "INDICADOR") || (dc.ColumnName.ToUpper() == "UNIDAD"))
                                {
                                    sb.Append("<td style=\"text-align:left\">");
                                    sb.Append(dr[dc.ColumnName].ToString());
                                    sb.Append("</td>");
                                }
                            }
                            else
                            {
                                if ((dc.ColumnName.ToUpper() == "INDICADOR"))
                                {
                                    if (flag==8)
                                        sb.Append("<td style=\"color:white; text-align:center\" colspan=\"2\">");
                                    else
                                        sb.Append("<td style=\"text-align:center\" colspan=\"2\">");
                                    sb.Append(dr[dc.ColumnName].ToString());
                                    sb.Append("</td>");
                                }
                            }
                        }
                    }
                    // Se termina la fila, se agrega totalizado
                    if (flag < 6 || flag ==8)
                    {
                        if (flag == 8)
                        {
                            sb.Append("<td style=\"color:white; font-weight:bold; text-align:right\">");
                            prodtotal = total;
                        }
                        else
                            sb.Append("<td style=\"text-align:right\">");

                        if (total > 0)
                            sb.Append(total.ToString("#,##0.00"));
                        sb.Append("</td>");
                    }
                    sb.Append("</tr>");
                }
                sqlConn.Close();
                sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });

                // TOTALES
                val2 = 0;
                val3 = 0;
                val4 = 0;
                valor=0;
                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',10", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                        sb.Append("<tr style=\"background-color: #E2EFDA\"><td style=\"border:none; color:white; background-color: white\" rowspan=\"12\"></td>");
                    else
                        if (flag < 7)
                            sb.Append("<tr style=\"background-color: #E2EFDA\">");
                        else
                        {
                            if (flag == 7)
                                sb.Append("<tr style=\"background-color: white; border:none; height:30px\"><td style=\"border:none\" colspan=\"9\"></td><tr>");
                            sb.Append("<tr style=\"background-color: #FFFF00\">");
                        }
                    double total = 0;
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
                                total += val;
                                switch (flag)
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                        val2 += val;
                                        break;
                                    case 5:
                                        val3 += val;
                                        break;
                                    case 6:
                                        val4 += val;
                                        break;
                                }
                                sb.Append("</td>");
                            }
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR"))
                            {
                                sb.Append("<td style=\"text-align:left\" colspan=\"2\">");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</td>");
                            }
                        }
                    }
                    // Se termina la fila, se agrega totalizado
                    if (flag < 7 )
                    {
                            sb.Append("<td style=\"font-weight:bold; text-align:right\">");

                        if (total > 0)
                            sb.Append(total.ToString("#,##0.00"));
                        sb.Append("</td>");
                        
                    }
                    else
                    {
                        sb.Append("<td style=\"font-weight:bold; text-align:right\">");
                            switch (flag)
                            {
                                case 7:
                                valor = Math.Round(val2/prodtotal,2) + Math.Round(val3 / prodtotal,2);
                                    break;
                                case 8:
                                    valor= Math.Round(val2 / prodtotal, 2);
                                    break;
                                case 9:
                                valor = Math.Round(val3 / prodtotal, 2);
                                    break;
                                case 10:
                                valor = Math.Round(val4 / prodtotal, 2);
                                    break;
                            }
                            if (valor > 0)
                            sb.Append(valor.ToString("#,##0.00"));
                        sb.Append("</td>");
                    }

                    sb.Append("</tr>");

                }
                sqlConn.Close();
                sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });

                // BENCHMARK
                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',11", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                    {
                        sb.Append("<tr style=\"background-color: #C6E0B4\"><td style=\"border:none; color:white; background-color: white\" rowspan=\"16\"></td></tr>");
                        sb.Append("<tr style=\"background-color: #BF8F00; border:none; height:30px\"><td style=\"border:none; background-color: BF8F00; text-align:center\" colspan=\"9\"><b>BENCHMARK</b></td></tr>");
                        sb.Append("<tr style=\"background-color: #C6E0B4\">");
                    }
                    else
                        if (flag < 4)
                            sb.Append("<tr style=\"background-color: #C6E0B4\">");
                        else
                            if (flag<6)
                                sb.Append("<tr style=\"background-color: #FFE699\">");
                            else
                                if (flag < 11)
                                    sb.Append("<tr style=\"background-color: #F8CBAD\">");
                                else
                                    sb.Append("<tr style=\"background-color: #C6E0B4\">");

                    double total = 0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if ((dc.ColumnName.ToUpper() != "ORDENAREA") && (dc.ColumnName.ToUpper() != "AREA") && (dc.ColumnName.ToUpper() != "ORDENIND") && (dc.ColumnName.ToUpper() != "INDICADOR") && (dc.ColumnName.ToUpper() != "UNIDAD"))
                        {
                            sb.Append("<td style=\"background-color:#FFFFFF; text-align:right\">");
                            Double val;
                            bool res;
                            res = Double.TryParse(dr[dc.ColumnName].ToString(), out val);
                            if (val > 0)
                            {
                                sb.Append(val.ToString("#,##0.00"));
                                total += val;
                            }
                            sb.Append("</td>");
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR"))
                            {
                                sb.Append("<td style=\"text-align:left\" colspan=\"2\"><b>");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</b></td>");
                            }
                        }
                    }
                    sb.Append("</tr>");
                }
                sqlConn.Close();
                //sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });
                // BENCHMARK
                sqlcmd = new SqlCommand("exec sp_getDatosSemArea_v2 '" + fecha.ToString() + "',12", sqlConn);
                sqlConn.Open();
                sda = new SqlDataAdapter(sqlcmd);
                dt = new DataTable();
                sda.Fill(dt);
                sb = new StringBuilder();
                sb.Append("<tbody>");
                flag = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    flag++;
                    if (flag == 1)
                    {
                        sb.Append("<tr style=\"background-color: #C6E0B4\"><td style=\"border:none; color:white; background-color: white\" rowspan=\"16\"></td></tr>");
                        sb.Append("<tr style=\"background-color: #BF8F00; border:none; height:30px\"><td style=\"border:none; background-color: BF8F00; text-align:center\" colspan=\"9\"><b>BENCHMARK/INTENSITY</b></td></tr>");
                        sb.Append("<tr style=\"background-color: #C6E0B4\">");
                    }
                    else
                        if (flag < 4)
                        sb.Append("<tr style=\"background-color: #C6E0B4\">");
                    else
                            if (flag < 6)
                        sb.Append("<tr style=\"background-color: #FFE699\">");
                    else
                                if (flag < 11)
                        sb.Append("<tr style=\"background-color: #F8CBAD\">");
                    else
                        sb.Append("<tr style=\"background-color: #C6E0B4\">");

                    double total = 0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if ((dc.ColumnName.ToUpper() != "ORDENAREA") && (dc.ColumnName.ToUpper() != "AREA") && (dc.ColumnName.ToUpper() != "ORDENIND") && (dc.ColumnName.ToUpper() != "INDICADOR") && (dc.ColumnName.ToUpper() != "UNIDAD"))
                        {
                            sb.Append("<td style=\"background-color:#FFFFFF; text-align:right\">");
                            Double val;
                            bool res;
                            res = Double.TryParse(dr[dc.ColumnName].ToString(), out val);
                            if (val > 0)
                            {
                                sb.Append(val.ToString("#,##0.00"));
                                total += val;
                            }
                            sb.Append("</td>");
                        }
                        else
                        {
                            if ((dc.ColumnName.ToUpper() == "INDICADOR"))
                            {
                                sb.Append("<td style=\"text-align:left\" colspan=\"2\"><b>");
                                sb.Append(dr[dc.ColumnName].ToString());
                                sb.Append("</b></td>");
                            }
                        }
                    }
                    sb.Append("</tr>");
                }
                sqlConn.Close();
                sb.Append("<tr style=\"background -color: white;  border:none; height: 30px\"><td style=\"border:none\" colspan=\"10\"></th></tr></tbody>");
                Panel1.Controls.Add(new Label { Text = sb.ToString() });


            }
        }

        protected void calendario_SelectionChanged(object sender, EventArgs e)
        {

        }
    }

    
}