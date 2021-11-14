using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public enum Tipos { Entity, SqlServer, MySql }
    public static class FabricaDaos
    {
        private static readonly IDaoProducto daoEntityProducto = new DaoEntityProducto();
        private static readonly IDaoUsuario daoEntityUsuario = new DaoEntityUsuario();
        private static readonly IDaoCarrito daoEntityCarrito = new DaoEntityCarrito();
        public static IDaoProducto ObtenerDaoProducto(Tipos tipo)
        {
            switch (tipo)
            {
                case Tipos.Entity:
                    return daoEntityProducto;
                default:
                    throw new NotImplementedException("No existe implementación para ese tipo todavía: " + tipo);
            }
        }

        public static IDaoUsuario ObtenerDaoUsuario(Tipos tipo)
        {
            switch (tipo)
            {
                case Tipos.Entity:
                    return daoEntityUsuario;
                default:
                    throw new NotImplementedException("No existe implementación para ese tipo todavía: " + tipo);
            }
        }

        public static IDaoCarrito ObtenerDaoCarrito(Tipos tipo)
        {
            switch (tipo)
            {
                case Tipos.Entity:
                    return daoEntityCarrito;
                default:
                    throw new NotImplementedException("No existe implementación para ese tipo todavía: " + tipo);
            }
        }
    }
}
