using Dal;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public static class UsuariosBll
    {
        private static readonly IDaoUsuario dao = FabricaDaos.ObtenerDaoUsuario(Tipos.Entity);

        public static Usuario Verificar(UsuarioLogin usuario)
        {
            Usuario aVerificar = dao.ObtenerPorEmail(usuario.Email);

            if (aVerificar != null && BCrypt.Net.BCrypt.Verify(usuario.Password, aVerificar.Password))
            {
                return aVerificar;
            }

            return null;

        }
        public static string ObtenerHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static void Validar(Usuario usuario)
        {
            if (usuario == null || usuario.Rol != "ADMIN")
            {
                throw new UnauthorizedAccessException("Sólo se admiten usuarios administradores: " + usuario);
            }
        }
        public static IEnumerable<Usuario> Consultar(Usuario admin)
        {
            Validar(admin);
            return dao.ObtenerTodos();
        }


        public static Usuario BuscarPorId(Usuario admin, long? id)
        {
            Validar(admin);
            return dao.ObtenerPorId(id.Value);
        }

        public static void Guardar(Usuario admin, Usuario usuario)
        {
            Validar(admin);
            dao.Insertar(usuario);
        }

        public static void Modificar(Usuario admin, Usuario usuario)
        {
            Validar(admin);
            dao.Modificar(usuario);
        }

        public static void Borrar(Usuario admin, long id)
        {
            Validar(admin);
            dao.Borrar(id);
        }
        public static void DarPermisos(Usuario admin, long id)
        {
            Validar(admin);
            Usuario usuario = dao.ObtenerPorId(id);
            usuario.Rol = "ADMIN";
            dao.Modificar(usuario);
        }

        public static void QuitarPermisos(Usuario admin, long id)
        {
            Validar(admin);
            Usuario usuario = dao.ObtenerPorId(id);
            usuario.Rol = "USER";
            dao.Modificar(usuario);
        }

    }
}
