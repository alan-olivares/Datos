using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Datos.Controllers
{
    //[RoutePrefix("api/datos")]
    public class IndexController : ApiController
    {
        private string conn = Cipher.Decrypt(@ConfigurationManager.ConnectionStrings["SQLConn"].ConnectionString, "Pims.2020");

        [Route("api/datos/currentWeek")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage currentWeek()
        {
            int sem = Funciones.GetWeekNumber();
            String semana = DateTime.Now.ToString("yyyy") + "-W" + (sem < 10 ? "0" + sem : sem.ToString());
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent(semana, System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }

        [Route("api/datos/areas")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetPlantas()
        {
            return Consulta("select AreaId,Nombre from En_Areas where IdDashboard=1 and Grafica=1 order by Nombre for json path", conn);
        }
        [Route("api/datos")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetAreaDatos(String fecha,int area)
        {
            return Consulta("exec sp_getDatosGrafica '" + Funciones.dayOfWeek(fecha) + "',"+ area+",1", conn);
        }
        [Route("api/datos/targets")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetTargets()
        {
            return Consulta("select  I.IndicadorId,g.Value,A.Area,I.Nombre from En_Targets G " +
                "RIGHT OUTER join En_Indicadores I on G.IndicadorId = I.IndicadorId " +
                "inner join En_AreaTarget A on G.IdAreaTarget = A.IdAreaTarget WHERE(I.Tipo = 1) " +
                "and I.AreaId = 3 order by A.IdAreaTarget, I.Orden for json path", conn);
        }
        [Route("api/datos/guardarTarget")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage GuardarDatos([FromBody] Object objeto)
        {
            JObject jsonObject = JObject.Parse(objeto.ToString());

            try {
                EjecutarComando("UPDATE En_Targets set value=" + jsonObject["valor"].ToString() + " where IndicadorId=" + jsonObject["id"].ToString(), conn);
                return mostrarMensaje(true, "");
            } catch (Exception e) {
                return mostrarMensaje(false, e.Message);
            }
            
        }
        private HttpResponseMessage mostrarMensaje(bool correcto,string error) {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            if(correcto)
                resp.Content = new StringContent("Dato almacenado correctamente", System.Text.Encoding.UTF8, "text/plain");
            else
                resp.Content = new StringContent("..Error.. Se generó un problema al guardar los datos "+ error, System.Text.Encoding.UTF8, "text/plain");
            return resp;

        }

        private void EjecutarComando(String comando, String connStr)
        {
            SqlConnection sqlConn = new SqlConnection(connStr);
            sqlConn.Open();
            SqlCommand sqlcmd = new SqlCommand(comando, sqlConn);
            sqlcmd.ExecuteNonQuery();
            sqlConn.Close();
        }
        private HttpResponseMessage Consulta(String consulta, String connStr)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            SqlConnection sqlConn = new SqlConnection(connStr);
            sqlConn.Open();
            try
            {
                SqlCommand sqlcmd = new SqlCommand(consulta, sqlConn);
                SqlDataReader reader = sqlcmd.ExecuteReader();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                while (reader.Read())
                    sb.Append(reader.GetSqlString(0).Value);
                //Console.WriteLine(sqlcmd.ExecuteScalar().ToString());
                resp.Content = new StringContent(sb.ToString().Length == 0 ? "[]" : sb.ToString(), System.Text.Encoding.UTF8, "text/plain");

                return resp;
            }
            catch
            {
                resp.Content = new StringContent("..Error.. No se pudo consultar la información en la base de datos, intenta de nuevo más tarde", System.Text.Encoding.UTF8, "text/plain");
                sqlConn.Close();
                return resp;
            }
            finally
            {
                sqlConn.Close();
            }
        }
    }
}