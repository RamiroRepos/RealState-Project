﻿using Microsoft.AspNetCore.Mvc;
using RealState_WEB.Filtros;
using RealState_WEB.Model;
using System.Text.Json;

namespace RealState_WEB.Controllers
{
    public class UsuariosController : Controller
    {
        [HttpGet]
        [FiltroLogin]
        public async Task<IActionResult> MiPerfil()
        {
            using var client = new HttpClient();
            
            var idUsuario = HttpContext.Session.GetInt32("_id");

            var apiUrl = "https://localhost:7273/api/Usuarios/Usuario/" + idUsuario;
            var respuesta = await client.GetAsync(apiUrl);
            if (respuesta.IsSuccessStatusCode)
            {
                var UsuarioJson = await respuesta.Content.ReadAsStringAsync();
                var Usuario = JsonSerializer.Deserialize<USUARIOS>(UsuarioJson);
                Usuario.rol.rolesList = await Roles();
                Usuario.direccion.pais.paisesList = await Paises();
                Usuario.direccion.provincia.provinciaList = await Provincias();
                return View(Usuario);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [FiltroAdmin]
        public async Task<IActionResult> ConsultarUsuarios()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Usuarios/Usuarios";
            var respuesta = await client.GetAsync(apiUrl);
            if (respuesta.IsSuccessStatusCode)
            {
                var UsuariosJson = await respuesta.Content.ReadAsStringAsync();
                var UsuariosList = JsonSerializer.Deserialize<List<USUARIOS>>(UsuariosJson);
                return View(UsuariosList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [FiltroAdmin]
        public async Task<IActionResult> ActualizarUsuario(long id)
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Usuarios/Usuario/" + id;
            var respuesta = await client.GetAsync(apiUrl);
            if (respuesta.IsSuccessStatusCode)
            {
                var usuarioJson = await respuesta.Content.ReadAsStringAsync();
                var usuario = JsonSerializer.Deserialize<USUARIOS>(usuarioJson);

                usuario.rol.rolesList = await Roles();
                usuario.direccion.pais.paisesList = await Paises();
                usuario.direccion.provincia.provinciaList = await Provincias();
                usuario.confirmarContrasenna = usuario.contrasenna;
                return View(usuario);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [FiltroAdmin]
        public async Task<IActionResult> ActualizarUsuario(USUARIOS usuarioActualizado)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    using var client = new HttpClient();
                    var apiUrl = "https://localhost:7273/api/Usuarios/ActualizarUsuario";

                    JsonContent body = JsonContent.Create(usuarioActualizado);

                    var response = await client.PutAsync(apiUrl, body);

                    if (response.IsSuccessStatusCode)
                    {
                        // Mostrar Sweet Alert
                        TempData["SweetAlertMessage"] = "El usuario se actualizó correctamente.";
                        TempData["SweetAlertType"] = "success";
                        return RedirectToAction("ConsultarUsuarios");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    if (usuarioActualizado.rol == null)
                    {
                        usuarioActualizado.rol = new USUARIO_ROLES();
                    }
                    usuarioActualizado.rol.rolesList = await Roles();
                    usuarioActualizado.direccion.pais.paisesList = await Paises();
                    usuarioActualizado.direccion.provincia.provinciaList = await Provincias();
                    return View(usuarioActualizado);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [FiltroAdmin]
        public async Task<IActionResult> NuevoUsuario()
        {
            try
            {
                USUARIOS nUsuario = new USUARIOS();
                nUsuario.rol = new USUARIO_ROLES();
                nUsuario.rol.rolesList = await Roles();
                nUsuario.direccion = new USUARIO_DIRECCIONES();
                nUsuario.direccion.pais = new PAISES();
                nUsuario.direccion.pais.paisesList = await Paises();
                nUsuario.direccion.provincia = new PROVINCIAS();
                nUsuario.direccion.provincia.provinciaList = await Provincias();
                return View(nUsuario);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [FiltroAdmin]
        public async Task<IActionResult> NuevoUsuario(USUARIOS nuevoUsuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using var client = new HttpClient();
                    var apiUrl = "https://localhost:7273/api/Usuarios/Usuario";

                    JsonContent body = JsonContent.Create(nuevoUsuario);

                    var response = await client.PostAsync(apiUrl, body);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ConsultarPropiedades");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    nuevoUsuario.rol.rolesList = await Roles();
                    nuevoUsuario.direccion.pais.paisesList = await Paises();
                    nuevoUsuario.direccion.provincia.provinciaList = await Provincias();
                    return View(nuevoUsuario);
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        // Intercambia el estado del usuario,si es true (Activo) lo pasa a false (Inactivo)

        [HttpGet]
        [FiltroAdmin]
        public async Task<IActionResult> CambiarEstado(long id)
        {
            try
            {
                using var client = new HttpClient();
                var apiUrl = "https://localhost:7273/api/Usuarios/CambiarEstado/" + id;
                var respuesta = await client.GetAsync(apiUrl);
                if (respuesta.IsSuccessStatusCode)
                {
                    // Mostrar Sweet Alert
                    TempData["SweetAlertMessage"] = "Se ha cambiado el estado correctamente";
                    TempData["SweetAlertType"] = "success";
                    return RedirectToAction("ConsultarUsuarios");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        //Consulta los roles
        [HttpGet]
        [FiltroAdmin]
        public async Task<List<USUARIO_ROLES>> Roles()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Usuarios/Roles";
            var respuesta = await client.GetAsync(apiUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                var rolesJson = await respuesta.Content.ReadAsStringAsync();
                var rolesList = JsonSerializer.Deserialize<List<USUARIO_ROLES>>(rolesJson);

                return rolesList;
            }
            else
            {
                return new List<USUARIO_ROLES>();
            }
        }
        //Consulta los paises
        [HttpGet]
        [FiltroAdmin]
        public async Task<List<PAISES>> Paises()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Paises/";
            var respuesta = await client.GetAsync(apiUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                var paisesJson = await respuesta.Content.ReadAsStringAsync();
                var paisesList = JsonSerializer.Deserialize<List<PAISES>>(paisesJson);

                return paisesList;
            }
            else
            {
                return new List<PAISES>();
            }
        }
        //Consulta las provincias
        [HttpGet]
        [FiltroAdmin]
        public async Task<List<PROVINCIAS>> Provincias()
        {
            using var client = new HttpClient();
            var apiUrl = "https://localhost:7273/api/Propiedades/Provincias/";
            var respuesta = await client.GetAsync(apiUrl);

            if (respuesta.IsSuccessStatusCode)
            {
                var provinciasJson = await respuesta.Content.ReadAsStringAsync();
                var provinciasList = JsonSerializer.Deserialize<List<PROVINCIAS>>(provinciasJson);

                return provinciasList;
            }
            else
            {
                return new List<PROVINCIAS>();
            }
        }
    }
}
