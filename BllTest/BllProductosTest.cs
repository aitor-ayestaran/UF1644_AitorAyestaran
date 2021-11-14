using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Bll
{
    [TestClass]
    public class BllProductosTest
    {
        private DbConnection ObtenerConexion()
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.
                ConnectionStrings["TiendaContext"].ConnectionString);
        }
        [TestMethod]
        public void Consultar()
        {
            List<Producto> productos = ProductosBll.Consultar(new Usuario() { Rol = "ADMIN" }) as List<Producto>;

            Assert.IsNotNull(productos);

            Assert.ThrowsException<UnauthorizedAccessException>(() => ProductosBll.Consultar(new Usuario() { Rol = "USER" }));

            using (DbConnection con = ObtenerConexion())
            {
                con.Open();

                DbCommand com = con.CreateCommand();
                com.CommandText = "SELECT COUNT(*) FROM Productos";

                int cuenta = (int)com.ExecuteScalar();

                Assert.AreEqual(cuenta, productos.Count);

                com.CommandText = "SELECT * FROM Productos";

                DbDataReader dr = com.ExecuteReader();

                int i = 0;

                while (dr.Read())
                {
                    Assert.AreEqual(dr["Id"], productos[i].Id);
                    Assert.AreEqual(dr["Nombre"], productos[i].Nombre);
                    Assert.AreEqual(dr["Precio"], productos[i].Precio);
                    if (productos[i].Foto != null)
                    {
                        Assert.AreEqual(dr["Foto"], productos[i].Foto);
                    }

                    i++;
                }
            }
        }

        [TestMethod]
        public void BuscarPorId()
        {
            using (DbConnection con = ObtenerConexion())
            {
                con.Open();

                DbCommand com = con.CreateCommand();
                com.CommandText = "SELECT TOP 1 * FROM Productos ORDER BY Id DESC";

                DbDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    Producto producto = Bll.ProductosBll.BuscarPorId(new Usuario() { Rol = "ADMIN" }, (long)dr["Id"]);

                    Assert.IsNotNull(producto);

                    Assert.AreEqual(dr["Id"], producto.Id);
                    Assert.AreEqual(dr["Nombre"], producto.Nombre);
                    Assert.AreEqual(dr["Precio"], producto.Precio);
                    if (producto.Foto != null)
                    {
                        Assert.AreEqual(dr["Foto"], producto.Foto);
                    }

                    producto = Bll.ProductosBll.BuscarPorId(new Usuario() { Rol = "ADMIN" }, (long)dr["Id"] + 1);

                    Assert.IsNull(producto);
                }


            }
        }

        [TestMethod]
        public void Guardar()
        {
            Producto nuevo = new Producto() { Nombre = "Nuevo Prueba", Precio = 65.44m, Unidad = "Unidad", PrecioUnidad = 65.44m };
            Bll.ProductosBll.Guardar(new Usuario() { Rol = "ADMIN" }, nuevo);


            using (DbConnection con = ObtenerConexion())
            {
                con.Open();
                DbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Productos WHERE Id = " + nuevo.Id;
                DbDataReader dr = com.ExecuteReader();

                Assert.IsTrue(dr.Read());
                Assert.AreEqual("Nuevo Prueba", dr["Nombre"]);
                Assert.AreEqual(65.44m, dr["Precio"]);
                Assert.AreEqual("Unidad", dr["Unidad"]);
                Assert.AreEqual(65.44m, dr["PrecioUnidad"]);

                dr.Close();

                IDbDataParameter parNombre = com.CreateParameter();
                parNombre.ParameterName = "Nombre";
                parNombre.DbType = DbType.String;
                parNombre.Value = "Nuevo Prueba";
                com.Parameters.Add(parNombre);
                com.CommandText = "DELETE FROM Productos WHERE Nombre = @Nombre";
                com.ExecuteNonQuery();

            }
        }
        [TestMethod]
        public void Modificar()
        {
            using (DbConnection con = ObtenerConexion())
            {
                con.Open();

                DbCommand com = con.CreateCommand();
                com.CommandText = "SELECT TOP 1 Id, Nombre, Precio, Foto, Unidad, PrecioUnidad, Descuento FROM Productos ORDER BY Id DESC";
                DbDataReader dr = com.ExecuteReader();
                dr.Read();
                Producto original = new Producto()
                {
                    Id = (long)dr["Id"],
                    Nombre = (string)dr["Nombre"],
                    Precio = (decimal)dr["Precio"],
                    Unidad = (string)dr["Unidad"],
                    PrecioUnidad = (decimal)dr["PrecioUnidad"]
                };
                if (dr["Foto"] != DBNull.Value)
                {
                    original.Foto = (string)dr["Foto"];
                }
                if (dr["Descuento"] != DBNull.Value)
                {
                    original.Descuento = (decimal)dr["Descuento"];
                }
                dr.Close();
                Producto modificado = new Producto() { Id = original.Id, Nombre = "Modificado", Precio = 65.44m, Unidad = "Unidad", PrecioUnidad = 65.44m };
                Bll.ProductosBll.Modificar(new Usuario() { Rol = "ADMIN" }, modificado);

                com.CommandText = "SELECT Id, Nombre, Precio, Foto, Unidad, PrecioUnidad, Descuento FROM Productos WHERE Id = " + original.Id;

                DbDataReader dr2 = com.ExecuteReader();

                Assert.IsTrue(dr2.Read());

                Assert.AreEqual("Modificado", dr2["Nombre"]);
                Assert.AreEqual(65.44m, dr2["Precio"]);
                Assert.AreEqual("Unidad", dr2["Unidad"]);
                Assert.AreEqual(65.44m, dr2["PrecioUnidad"]);
                Assert.AreEqual(DBNull.Value, dr2["Foto"]);
                Assert.AreEqual(0.0m, dr2["Descuento"]);

                Bll.ProductosBll.Modificar(new Usuario() { Rol = "ADMIN" }, original);

            }
        }
        [TestMethod]
        public void Borrar()
        {
            using (DbConnection con = ObtenerConexion())
            {
                con.Open();

                DbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO PRODUCTOS (Nombre, Precio, Unidad, PrecioUnidad, Descuento) VALUES(@Nombre, @Precio, @Unidad, @PrecioUnidad, @Descuento)";

                IDbDataParameter parNombre = com.CreateParameter();
                parNombre.ParameterName = "Nombre";
                parNombre.DbType = DbType.String;
                parNombre.Value = "Borrar Prueba";
                com.Parameters.Add(parNombre);

                IDbDataParameter parPrecio = com.CreateParameter();
                parPrecio.ParameterName = "Precio";
                parPrecio.DbType = DbType.Decimal;
                parPrecio.Value = 6.44m;
                com.Parameters.Add(parPrecio);

                IDbDataParameter parUnidad = com.CreateParameter();
                parUnidad.ParameterName = "Unidad";
                parUnidad.DbType = DbType.String;
                parUnidad.Value = "Unidad";
                com.Parameters.Add(parUnidad);

                IDbDataParameter parPrecioUnidad = com.CreateParameter();
                parPrecioUnidad.ParameterName = "PrecioUnidad";
                parPrecioUnidad.DbType = DbType.Decimal;
                parPrecioUnidad.Value = 6.44m;
                com.Parameters.Add(parPrecioUnidad);

                IDbDataParameter parDescuento = com.CreateParameter();
                parDescuento.ParameterName = "Descuento";
                parDescuento.DbType = DbType.Decimal;
                parDescuento.Value = 10.0m;
                com.Parameters.Add(parDescuento);

                com.ExecuteNonQuery();

                com.CommandText = "SELECT Id FROM Productos WHERE Nombre = @Nombre";
                long id = (long)com.ExecuteScalar();

                Bll.ProductosBll.Borrar(new Usuario() { Rol = "ADMIN" }, id);

                com.CommandText = "SELECT * FROM Productos WHERE Id = " + id;
                DbDataReader dr = com.ExecuteReader();

                Assert.IsFalse(dr.Read());

            }
        }
    }
}
