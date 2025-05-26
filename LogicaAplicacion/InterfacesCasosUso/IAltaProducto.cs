using DTOs.DTOs;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUso
{
    public interface IAltaProducto
    {
        void AltaProd(string nombre, string url, string descripcion, int categoria, int subcategoria, string documentacion, int precio);
        void AltaProdCatEspecial(string nombre, string url, string descripcion, int categoria, int subcategoria, int categroiaEspecial, string documentacion, int precio);
        void ModifyProd(int id, string nombre, string url, string descripcion, int categoria, int subcategoria, int? cateEspecial, string documentacion, int precio);
        bool verificarCategoria(int categoria);
    }
}
