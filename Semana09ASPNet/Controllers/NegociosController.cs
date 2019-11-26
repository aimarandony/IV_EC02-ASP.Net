using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Semana09ASPNet.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Semana09ASPNet.Controllers
{
    public class NegociosController : Controller
    {
        
        SqlConnection cn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["cadena"].ConnectionString);

        List<Contribuyente> Contribuyentes()
        {
            List<Contribuyente> temporal = new List<Contribuyente>();

            SqlCommand cmd = new SqlCommand("usp_contribuyentes", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Contribuyente reg = new Contribuyente
                {
                    dnicont = dr.GetString(0),
                    nomcont = dr.GetString(1),
                    apecont = dr.GetString(2),
                    dircont = dr.GetString(3),
                    iddis = dr.GetInt32(4),
                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }
    
        public ActionResult Home()
        {
            return View(Contribuyentes());
        }

        List<Distrito> Distritos()
        {
            List<Distrito> temporal = new List<Distrito>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM DISTRITO", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Distrito reg = new Distrito
                {
                    iddis = dr.GetInt32(0),
                    nomdis = dr.GetString(1)
                };
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }

        public ActionResult Nuevo()
        {
            ViewBag.distritos = new SelectList(Distritos(), "iddis", "nomdis");
            return View(new Contribuyente());
        }

        [HttpPost]public ActionResult Nuevo(Contribuyente reg)
        {
            if (!ModelState.IsValid) { return View(reg); }

            ViewBag.mensaje = "";

            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("usp_guardar_contribuyente", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dni", reg.dnicont);
                cmd.Parameters.AddWithValue("@nombre", reg.nomcont);
                cmd.Parameters.AddWithValue("@ape", reg.apecont);
                cmd.Parameters.AddWithValue("@dir", reg.dircont);
                cmd.Parameters.AddWithValue("@dis", reg.iddis);

                int q = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = q.ToString() + ") AGREGADO";
            }
            catch (SqlException ex)
            {
                ViewBag.mensaje = ex.Message;
                tr.Rollback();
            }
            finally
            {
                cn.Close();
            }

            ViewBag.distritos = new SelectList(Distritos(), "iddis", "nomdis", reg.iddis);
            return View(reg);
        }

        public ActionResult Edit(string id)
        {
            Contribuyente reg = Contribuyentes().Where(c => c.dnicont == id).FirstOrDefault();
            ViewBag.distritos = new SelectList(Distritos(), "iddis", "nomdis", reg.iddis);
            return View(reg);
        }

        [HttpPost]
        public ActionResult Edit(Contribuyente reg)
        {
            if (!ModelState.IsValid) { return View(reg); }

            ViewBag.mensaje = "";

            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("usp_guardar_contribuyente", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dni", reg.dnicont);
                cmd.Parameters.AddWithValue("@nombre", reg.nomcont);
                cmd.Parameters.AddWithValue("@ape", reg.apecont);
                cmd.Parameters.AddWithValue("@dir", reg.dircont);
                cmd.Parameters.AddWithValue("@dis", reg.iddis);

                int q = cmd.ExecuteNonQuery();
                tr.Commit();
                ViewBag.mensaje = q.ToString() + ") ACTUALIZADO";
            }
            catch (SqlException ex)
            {
                ViewBag.mensaje = ex.Message;
                tr.Rollback();
            }
            finally
            {
                cn.Close();
            }

            ViewBag.distritos = new SelectList(Distritos(), "iddis", "nomdis", reg.iddis);
            return View(reg);
        }
    }
}