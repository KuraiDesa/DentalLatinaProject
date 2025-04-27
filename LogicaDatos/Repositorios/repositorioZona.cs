using System;
using System.Collections.Generic;
using System.Linq;
using LogicaNegocio.InterfacesRepositorios;
using DentalLatina;
using LogicaDatos.EntityFramework;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace LogicaDatos.Repositorios
{
    public class repositorioZona : IRepositorioZona
    {
        // Lista en memoria para simular el almacenamiento
        public LibreriaContext _zonas { get; set; }
        public repositorioZona(LibreriaContext context)
        {
            _zonas = context;
        }

        public void Add(Zona obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "La zona no puede ser nula.");
            }

            // Agregar una nueva zona a la lista
            _zonas.Add(obj);
        }


        public IEnumerable<Zona> FindAll()
        {
            // Devuelve todas las zonas
            throw new NotImplementedException();
        }

        public Zona FindById(int id)
        {
            // Buscar una zona por su id
            return _zonas.Zonas
           .FirstOrDefault(z => z.Id == id);
        }
         public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Zona obj)
        {
            throw new NotImplementedException();
        }
    }
}
