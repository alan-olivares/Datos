using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Datos
{
    public partial class targets : System.Web.UI.Page
    {
        private static string connStrCrypt = ConfigurationManager.ConnectionStrings["SQLConn"].ConnectionString;
        //string cadena=Cipher.Decrypt("zH6yzH99uG94MV7JLMSEvCFFcYJGojn8s9XRTXGoptj2Rs9/LvN97bcVqBnXE3wV/ncgnncmBIzIs7ClvBcZQeqMW02Kn3BmulCHsPh1kqgghbWGuKm7I+MKUJUIEGxHsEiPnweFskBE60gAM85UHA4Tjhn3DUpK2nivZ6d5mkHMdGpvUXPixqMjqxlKqTB4MNmY6M1NeDoeB5acNmCQzA==","Pims.2020");
        //string cadena = Cipher.Encrypt("Data Source=PC2;Initial Catalog=EmployeeDB;User ID=sa;Password=pims.2016;MultipleActiveResultSets=True;", "Pims.2020");
        private static string connStr = Cipher.Decrypt(connStrCrypt, "Pims.2020");
        //Console.Write(connStr);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenaCampos();
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string val;
            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();
                val = txt36.Text.Trim() + "," + txt39.Text.Trim() + "," + txt40.Text.Trim() + "," + txt41.Text.Trim() + "," + txt54.Text.Trim() + "," + txt55.Text.Trim() + "," + txt56.Text.Trim() + "," + txt57.Text.Trim() + "," + txt58.Text.Trim() + "," + txt59.Text.Trim() + "," + txt60.Text.Trim() + "," + txt61.Text.Trim() + "," + txt62.Text.Trim() + "," + txt64.Text.Trim() + "," + txt65.Text.Trim() + "," + txt66.Text.Trim() + "," + txt67.Text.Trim() + "," + txt68.Text.Trim() + "," + txt69.Text.Trim() + "," + txt74.Text.Trim() + "," + txt75.Text.Trim() + "," + txt76.Text.Trim() + "," + txt77.Text.Trim() + "," + txt78.Text.Trim() + "," + txt80.Text.Trim() + "," + txt81.Text.Trim() + "," + txt82.Text.Trim() + "," + txt84.Text.Trim() + "," + txt85.Text.Trim() + "," + txt86.Text.Trim() + "," + txt96.Text.Trim() + "," + txt88.Text.Trim() + "," + txt89.Text.Trim() + "," + txt90.Text.Trim() + "," + txt91.Text.Trim() + "," + txt92.Text.Trim() + "," + txt93.Text.Trim() + "," + txt204.Text.Trim() + "," + txt205.Text.Trim() + "," + txt206.Text.Trim() + "," + txt207.Text.Trim()+"," + txt593.Text.Trim()+"," + txt594.Text.Trim()+"," + txt595.Text.Trim();
                SqlCommand sqlcmd = new SqlCommand("UPDATE MetersConfig SET Description='" + @val + "' WHERE IDMetersConfig=5", sqlConn);
                sqlcmd.ExecuteNonQuery();
                sqlcmd = new SqlCommand("EXEC sp_setTargets", sqlConn);
                sqlcmd.ExecuteNonQuery();


                //SqlCommand sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt36.Text.Trim() + " WHERE IndicadorId=36", sqlConn);
                ////sqlcmd.Parameters.AddWithValue("@valor", txt36.Text.Trim());
                ////sqlcmd.Parameters.AddWithValue("@indicador", Convert.ToInt32(36).ToString());
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value="+ txt39.Text.Trim() + " WHERE IndicadorId=39", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value="+ txt40.Text.Trim() + " WHERE IndicadorId=40", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt41.Text.Trim() + " WHERE IndicadorId=41", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt54.Text.Trim() + " WHERE IndicadorId=54", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt55.Text.Trim() + " WHERE IndicadorId=55", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt56.Text.Trim() + " WHERE IndicadorId=56", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt57.Text.Trim() + " WHERE IndicadorId=57", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt58.Text.Trim() + " WHERE IndicadorId=58", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt59.Text.Trim() + " WHERE IndicadorId=59", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt60.Text.Trim() + " WHERE IndicadorId=60", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt61.Text.Trim() + " WHERE IndicadorId=61", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt62.Text.Trim() + " WHERE IndicadorId=62", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt64.Text.Trim() + " WHERE IndicadorId=64", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt65.Text.Trim() + " WHERE IndicadorId=65", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt66.Text.Trim() + " WHERE IndicadorId=66", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt67.Text.Trim() + " WHERE IndicadorId=67", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt68.Text.Trim() + " WHERE IndicadorId=68", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt69.Text.Trim() + " WHERE IndicadorId=69", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt74.Text.Trim() + " WHERE IndicadorId=74", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt75.Text.Trim() + " WHERE IndicadorId=75", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt76.Text.Trim() + " WHERE IndicadorId=76", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt77.Text.Trim() + " WHERE IndicadorId=77", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt78.Text.Trim() + " WHERE IndicadorId=78", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt80.Text.Trim() + " WHERE IndicadorId=80", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt81.Text.Trim() + " WHERE IndicadorId=81", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt82.Text.Trim() + " WHERE IndicadorId=82", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt84.Text.Trim() + " WHERE IndicadorId=84", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt85.Text.Trim() + " WHERE IndicadorId=85", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt86.Text.Trim() + " WHERE IndicadorId=86", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt96.Text.Trim() + " WHERE IndicadorId=96", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt88.Text.Trim() + " WHERE IndicadorId=88", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt89.Text.Trim() + " WHERE IndicadorId=89", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt90.Text.Trim() + " WHERE IndicadorId=90", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt91.Text.Trim() + " WHERE IndicadorId=91", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt92.Text.Trim() + " WHERE IndicadorId=92", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt93.Text.Trim() + " WHERE IndicadorId=93", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt204.Text.Trim() + " WHERE IndicadorId=204", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt205.Text.Trim() + " WHERE IndicadorId=205", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value=" + txt206.Text.Trim() + " WHERE IndicadorId=206", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                //sqlcmd = new SqlCommand("UPDATE En_Targets SET Value="+ txt207.Text.Trim() + " WHERE IndicadorId=207", sqlConn);
                //sqlcmd.ExecuteNonQuery();

                llenaCampos();
            }
        }
        void llenaCampos()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                SqlCommand sqlcmd = new SqlCommand("EXEC SP_getTargets", sqlConn);
                sqlConn.Open();
                SqlDataReader reader = sqlcmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        switch (reader.GetInt32(0))
                        {
                            case 36:
                                txt36.Text=reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 39:
                                txt39.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 40:
                                txt40.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 41:
                                txt41.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 54:
                                txt54.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 55:
                                txt55.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 56:
                                txt56.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 57:
                                txt57.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 58:
                                txt58.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 59:
                                txt59.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 60:
                                txt60.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 61:
                                txt61.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 62:
                                txt62.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 64:
                                txt64.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 65:
                                txt65.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 66:
                                txt66.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 67:
                                txt67.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 68:
                                txt68.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 69:
                                txt69.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 74:
                                txt74.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 75:
                                txt75.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 76:
                                txt76.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 77:
                                txt77.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 78:
                                txt78.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 80:
                                txt80.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 81:
                                txt81.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 82:
                                txt82.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 593:
                                txt593.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 594:
                                txt594.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 595:
                                txt595.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 84:
                                txt84.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 85:
                                txt85.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 86:
                                txt86.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 96:
                                txt96.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 88:
                                txt88.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 89:
                                txt89.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 90:
                                txt90.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 91:
                                txt91.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 92:
                                txt92.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 93:
                                txt93.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 204:
                                txt204.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 205:
                                txt205.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 206:
                                txt206.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            case 207:
                                txt207.Text = reader.GetDouble(1).ToString("##0.00");
                                break;
                            default:
                                break;
                        }
                    }
                }
                reader.Close();
            }

        }

    }
}