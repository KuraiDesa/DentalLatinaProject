using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalLatina;
using LogicaDatos.EntityFramework;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;

namespace LogicaDatos.Repositorios
{
    public class repositorioProducto : IRepositorioProducto
    {
        public LibreriaContext Context { get; set; }
        public repositorioProducto(LibreriaContext context)
        {
            Context = context;
        }
        public void Add(Producto obj)
        {
            if (Context.Entry(obj.categoria).State == EntityState.Detached)
            {
                Context.Attach(obj.categoria);
            }
            if (Context.Entry(obj.subcategoria).State == EntityState.Detached)
            {
                Context.Attach(obj.subcategoria);
            }
            if (obj.categoriaEspecial != null)
            {
                if (Context.Entry(obj.categoriaEspecial).State == EntityState.Detached)
                {
                    Context.Attach(obj.categoriaEspecial);
                }
            }
            Context.Set<Producto>().Add(obj);
            Context.SaveChanges();
        }

        public IEnumerable<Producto> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return FindAll();
            }

            return Context.Set<Producto>()
                          .Where(p => p.nombre.ToLower().Contains(nombre.ToLower()))
                          .ToList();
        }

        public IEnumerable<Producto> FindAll()
        {
            return Context.Set<Producto>()
              .ToList();
        }


        public IEnumerable<Producto> GetProductosRelacionados(int productoId)
        {
            // Buscar el producto original
            var producto = Context.Productos
                .Include(p => p.categoria) // Incluimos la categoría para acceder a su ID
                .FirstOrDefault(p => p.Id == productoId);

            if (producto == null || producto.categoria == null)
            {
                return new List<Producto>(); // Devuelve lista vacía si no se encuentra
            }

            // Buscar hasta 4 productos de la misma categoría, excluyendo el original
            return Context.Productos
                .Where(p => p.categoria.Id == producto.categoria.Id && p.Id != productoId)
                .Take(4)
                .ToList();
        }
        public Producto FindById(int id)
        {
            return Context.Productos.FirstOrDefault(p => p.Id == id);
        }

        public void Remove(int id)
        {
            Producto producto = Context.Set<Producto>().Find(id);

            if (producto != null)
            {
                Context.Set<Producto>().Remove(producto);
                Context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("No se encontró un producto con el ID proporcionado.");
            }
        }

        public void RemoveByCatId(int id)
        {
            IEnumerable<Producto> prodL = Context.Set<Producto>()
              .Include(p => p.categoria)
              .Where(P => P.categoria.Id == id)
              .ToList();
            if (prodL.Any())
            {
                foreach(Producto prod in prodL)
                {
                    Context.Set<Producto>().Remove(prod);
                    Context.SaveChanges();
                }
            }
        }

        public void RemoveByScatId(int id)
        {
            IEnumerable<Producto> prodL = Context.Set<Producto>()
              .Include(p => p.subcategoria)
              .Where(P => P.subcategoria.Id == id)
              .ToList();
            if (prodL.Any())
            {
                foreach (Producto prod in prodL)
                {
                    Context.Set<Producto>().Remove(prod);
                    Context.SaveChanges();
                }
            }
        }
        public void RemoveByCEId(int id)
        {
            IEnumerable<Producto> prodL = Context.Set<Producto>()
              .Include(p => p.categoriaEspecial)
              .Where(P => P.categoriaEspecial.id == id)
              .ToList();
            if (prodL.Any())
            {
                foreach (Producto prod in prodL)
                {
                    Context.Set<Producto>().Remove(prod);
                    Context.SaveChanges();
                }
            }
        }

        public void Update(Producto obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> BuscarPorCategoria(int id)
        {
            return Context.Set<Producto>()
              .Include(p => p.categoria)
              .Where(P => P.categoria.Id == id)
              .ToList();
        }

        public IEnumerable<Producto> BuscarPorNombreCategoria(int? id, string nombre)
        {
            var query = Context.Set<Producto>().AsQueryable();

            // Filtrar por nombre si se proporciona
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(p => p.nombre.ToLower().Contains(nombre.ToLower()));
            }

            // Filtrar por categoría si se proporciona un id válido
            if (id.HasValue && id.Value != -1)
            {
                query = query.Where(p => p.categoria.Id == id.Value);
            }

            return query.Include(p => p.categoria).ToList();
        }

        
    }
}
