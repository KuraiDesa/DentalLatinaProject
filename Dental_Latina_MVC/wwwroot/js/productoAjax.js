function filtrarAjax(cat, nombre) {
    let categoriaId = document.getElementById(categoria);
    let nombreProd = document.getElementById(nombre);
    let seccionProd = document.getElementById(seccion);
    let nombreval = nombreProd.value;
    let categoriaval = categoriaId.value;
    $.ajax({
        url: '/Home/FilterAjax',
        method: 'GET',
        data: {
            categoria: categoriaval,
            nombre: nombreval
        },
        dataType: 'json',
        success: function (data) {
            seccionProd.innerHTML = "";
            
        },
        error: function (xhr, status, error) {
            console.error('AJAX error:', status, error);
            $('#subcategoriaProducto')
                .html('<option value="">Error cargando subcategorías</option>');
        }
    });
}

function traerSubcategorias(categoria, categoriaId) {
    let html = document.getElementById(categoriaId);

    $.ajax({
        url: '/Producto/FilterSubcategorias',
        method: 'GET',
        data: { cateid: categoria },
        dataType: 'json',
        success: function (data) {
            html.innerHTML = ''; // limpia el contenido del elemento
            data.forEach(prod => {
                html.innerHTML += `<a>${prod.nombre}</a>`;
            });
        },
        error: function (err) {
            console.error("Error al traer subcategorías", err);
        }
    });
}