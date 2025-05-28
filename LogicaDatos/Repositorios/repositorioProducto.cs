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
           return Context.Productos
                    .Include(p => p.categoria)
                    .Include(p => p.subcategoria)
                    .Include(p => p.categoriaEspecial)
                    .FirstOrDefault(p => p.Id == id);


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
            // Obtenemos el producto original desde el contexto, incluyendo las relaciones necesarias
            var productoExistente = Context.Productos
                .Include(p => p.categoria)
                .Include(p => p.subcategoria)
                .Include(p => p.categoriaEspecial)
                .FirstOrDefault(p => p.Id == obj.Id);

            if (productoExistente == null)
            {
                throw new Exception("Producto no encontrado.");
            }

            // Asignar nuevas relaciones usando solo IDs y marcar como Unchanged para evitar conflictos
            if (obj.categoria != null)
            {
                productoExistente.categoria = Context.Categorias.Local
                    .FirstOrDefault(c => c.Id == obj.categoria.Id) ??
                    new Categoria { Id = obj.categoria.Id };

                Context.Entry(productoExistente.categoria).State = EntityState.Unchanged;
            }

            if (obj.subcategoria != null)
            {
                productoExistente.subcategoria = Context.Subcategorias.Local
                    .FirstOrDefault(s => s.Id == obj.subcategoria.Id) ??
                    new Subcategoria { Id = obj.subcategoria.Id };

                Context.Entry(productoExistente.subcategoria).State = EntityState.Unchanged;
            }

            if (obj.categoriaEspecial != null)
            {
                productoExistente.categoriaEspecial = Context.CEspecial.Local
                    .FirstOrDefault(ce => ce.id == obj.categoriaEspecial.id) ??
                    new CEspecial { id = obj.categoriaEspecial.id };

                Context.Entry(productoExistente.categoriaEspecial).State = EntityState.Unchanged;
            }
            else
            {
                productoExistente.categoriaEspecial = null;
            }

            // Actualizar propiedades simples
            productoExistente.nombre = obj.nombre;
            productoExistente.descripcion = obj.descripcion;
            productoExistente.photoUrl = obj.photoUrl;
            productoExistente.documentacion = obj.documentacion;
            productoExistente.precio = obj.precio;

            Context.SaveChanges();
        }

        public IEnumerable<Producto> BuscarPorCategoria(int id)
        {
            return Context.Set<Producto>()
              .Include(p => p.categoria)
              .Where(P => P.categoria.Id == id)
              .ToList();
        }
        public IEnumerable<Producto> BuscarPorSubcategoria(int id)
        {
            return Context.Set<Producto>()
              .Include(p => p.subcategoria)
              .Where(P => P.subcategoria.Id == id)
              .ToList();
        }
        public IEnumerable<Producto> BuscarPorCategoriaEspecial(int id)
        {
            return Context.Set<Producto>()
              .Include(p => p.categoriaEspecial)
              .Where(P => P.categoriaEspecial.id == id)
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
