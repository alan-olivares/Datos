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
    public partial class carga : System.Web.UI.Page
    {
        private static string connStrCrypt = ConfigurationManager.ConnectionStrings["SQLConn"].ConnectionString;
        //string cadena=Cipher.Decrypt("zH6yzH99uG94MV7JLMSEvCFFcYJGojn8s9XRTXGoptj2Rs9/LvN97bcVqBnXE3wV/ncgnncmBIzIs7ClvBcZQeqMW02Kn3BmulCHsPh1kqgghbWGuKm7I+MKUJUIEGxHsEiPnweFskBE60gAM85UHA4Tjhn3DUpK2nivZ6d5mkHMdGpvUXPixqMjqxlKqTB4MNmY6M1NeDoeB5acNmCQzA==","Pims.2020");
        //string cadena = Cipher.Encrypt("Data Source=PC4\\SQL2K16;Initial Catalog=EMDashboard;User ID=sa;Password=pims.2016;MultipleActiveResultSets=True;", "Pims.2020");
        //string cadena = Cipher.Encrypt("Data Source=PC2;Initial Catalog=EMDashboard;User ID=emetrix;Password=3m3tr1x2019;MultipleActiveResultSets=True;", "Pims.2020");
        private static string connStr = Cipher.Decrypt(connStrCrypt, "Pims.2020");
        //Console.Write(connStr);
        public int semana = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int sem = Funciones.GetWeekNumber();
                date1.Value = (Request.QueryString["fecha"] != null) ? Request.QueryString["fecha"].ToString() : DateTime.Now.ToString("yyyy") + "-W" + (sem < 10 ? "0" + sem : sem.ToString());
                PopulateGridview();
            }
        }

        void PopulateGridview()
        {
            string fecha = Funciones.dayOfWeek(date1.Value);
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("exec sp_getDatosManualSem_v2  '" + fecha + "'" , sqlConn); 
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                gvDatos.DataSource = dtbl;
                gvDatos.DataBind();
                gvDatos.Columns[0].Visible = false;
                gvDatos.Columns[2].Visible = false;
                gvDatos.Columns[3].Visible = false;
                
            }
            //else
            //{
            //    dtbl.Rows.Add(dtbl.NewRow());
            //    gvDatos.DataSource = dtbl;
            //    gvDatos.DataBind();
            //    gvDatos.Rows[0].Cells.Clear();
            //    gvDatos.Rows[0].Cells.Add(new TableCell());
            //    gvDatos.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            //    gvDatos.Rows[0].Cells[0].Text = "No Data Found ..!";
            //    gvDatos.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            //}

        }

        protected void gvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                DateTime inicio = Funciones.dayOfWeek(Funciones.getYear(date1.Value),Funciones.getWeek(date1.Value));

                e.Row.Cells[6].Text = inicio.ToString("dd/MMM/yyyy");
                e.Row.Cells[7].Text = inicio.AddDays(1).ToString("dd/MMM/yyyy");
                e.Row.Cells[8].Text = inicio.AddDays(2).ToString("dd/MMM/yyyy");
                e.Row.Cells[9].Text = inicio.AddDays(3).ToString("dd/MMM/yyyy");
                e.Row.Cells[10].Text = inicio.AddDays(4).ToString("dd/MMM/yyyy");
                e.Row.Cells[11].Text = inicio.AddDays(5).ToString("dd/MMM/yyyy");
                e.Row.Cells[12].Text = inicio.AddDays(6).ToString("dd/MMM/yyyy");
            }
        }

        protected void gvDatos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //date1.Value = (Request.QueryString["fecha"] != null) ? Request.QueryString["fecha"].ToString() : DateTime.Now.ToString("yyyy") + "-W" + Funciones.GetWeekNumber();
            gvDatos.EditIndex = e.NewEditIndex;
            PopulateGridview();
            lblSuccessMessage.Text = "";
            lblErrorMessage.Text = "";

        }

        protected void gvDatos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //date1.Value = (Request.QueryString["fecha"] != null) ? Request.QueryString["fecha"].ToString() : DateTime.Now.ToString("yyyy") + "-W" + Funciones.GetWeekNumber();
            
            gvDatos.EditIndex = -1;
            PopulateGridview();
            lblSuccessMessage.Text = "";
            lblErrorMessage.Text = "";
            Response.Redirect("carga.aspx?fecha=" + date1.Value.ToString());
        }

        protected void gvDatos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //date1.Value = (Request.QueryString["fecha"] != null) ? Request.QueryString["fecha"].ToString() : DateTime.Now.ToString("yyyy") + "-W" + Funciones.GetWeekNumber();
            string fecha = Funciones.dayOfWeek(date1.Value);
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connStr))
                {
                    sqlConn.Open();
                    string query = "EXEC sp_CargaDatosSem_v2 '"+ fecha + "',@indicadorId,@lunes,@martes,@miercoles,@jueves,@viernes,@sabado,@domingo";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlConn);
                    sqlCmd.Parameters.AddWithValue("@indicadorId", Convert.ToInt32(gvDatos.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.Parameters.AddWithValue("@lunes", (gvDatos.Rows[e.RowIndex].FindControl("txtMonday") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@martes", (gvDatos.Rows[e.RowIndex].FindControl("txtTuesday") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@miercoles", (gvDatos.Rows[e.RowIndex].FindControl("txtWednesday") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@jueves", (gvDatos.Rows[e.RowIndex].FindControl("txtThursday") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@viernes", (gvDatos.Rows[e.RowIndex].FindControl("txtFriday") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@sabado", (gvDatos.Rows[e.RowIndex].FindControl("txtSaturday") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@domingo", (gvDatos.Rows[e.RowIndex].FindControl("txtSunday") as TextBox).Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    gvDatos.EditIndex = -1;
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
                    Response.Redirect("carga.aspx?fecha=" + date1.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void gvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //try
            //{
            //    if (e.CommandName.Equals("AddNew"))
            //    {
            //        using (SqlConnection sqlCon = new SqlConnection(connStr))
            //        {
            //            //sqlCon.Open();
            //            //string query = "INSERT INTO PhoneBook (FirstName,LastName,Contact,Email) VALUES (@FirstName,@LastName,@Contact,@Email)";
            //            //SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
            //            //sqlCmd.Parameters.AddWithValue("@FirstName", (gvDatos.FooterRow.FindControl("txtFirstNameFooter") as TextBox).Text.Trim());
            //            //sqlCmd.Parameters.AddWithValue("@LastName", (gvDatos.FooterRow.FindControl("txtLastNameFooter") as TextBox).Text.Trim());
            //            //sqlCmd.Parameters.AddWithValue("@Contact", (gvDatos.FooterRow.FindControl("txtContactFooter") as TextBox).Text.Trim());
            //            //sqlCmd.Parameters.AddWithValue("@Email", (gvDatos.FooterRow.FindControl("txtEmailFooter") as TextBox).Text.Trim());
            //            //sqlCmd.ExecuteNonQuery();
            //            PopulateGridview();
            //            //lblSuccessMessage.Text = "New Record Added";
            //            lblErrorMessage.Text = "";
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblSuccessMessage.Text = "";
            //    lblErrorMessage.Text = ex.Message;
            //}
        }



    }
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}