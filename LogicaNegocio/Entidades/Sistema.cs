using System.Reflection.Metadata.Ecma335;

namespace DentalLatina
{
    public class Sistema
    {

        public static Sistema inst;
        public string[] categorias = { "CLINICA", "LABORATORIO", "IMPLANTOLOGIA" };
        public List<string> subcategorias= new List<string>();
        public List<Producto> productos = new List<Producto>();
        public List<Evento> eventos = new List<Evento>();
        public List<Usuario> usuarios = new List<Usuario>();
        public List<Promocion> promociones = new List<Promocion>(); 

        //GETS
        public string[] getCategoria() { return categorias; }
        public List<Producto> getProductos() {  return productos; }
        public List <Evento> getEventos() {  return eventos; }
        public List<string> getSubcategorias() { return subcategorias; }
        public List<Usuario> getUsuarios() { return usuarios; }
        public List<Promocion> getPromociones() { return promociones; }
   
        

        //SETS
        public void setProductos(List<Producto> lista) {  productos = lista; }
        public void setEventos(List<Evento> lista) {  eventos = lista; }
        public void setSubCat(List<string> lista) { subcategorias = lista; }
        public void setCategorias(string[] array) {  categorias = array; }
        public void setUsuarios(List<Usuario> lista) {  usuarios = lista; }
        public void setPromociones(List<Promocion> lsita) {  promociones = lsita; }


        //AGREGAR
        public void addProducto(Producto producto){productos.Add(producto);}
        public void addSubCat(string subCat){subcategorias.Add(subCat);}
        public void addEvento(Evento evento) { eventos.Add(evento); }
        public void addUsuario(Usuario user) {usuarios.Add(user);}
        public void addPromocion(Promocion promocion) { promociones.Add(promocion); }




        //FUNCIONALIDADES
        public static Sistema instancia()
        {
            if (inst == null)
            {
                inst = new Sistema();
            }
            return inst;
        }

    }
}
