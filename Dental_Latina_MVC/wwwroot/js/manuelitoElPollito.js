function filtrar(seccion, categoria, nombre) {
    let categoriaId = document.getElementById(categoria);
    let nombreProd = document.getElementById(nombre);
    let seccionProd = document.getElementById(seccion); 
    let nombreval = nombreProd.value;
    let categoriaval = categoriaId.value;
    $.ajax({
        url: '/Admin/FilterByCategoria',
        method: 'GET',
        data: {
            categoria: categoriaval,
            nombre: nombreval
        },
        dataType: 'json',
        success: function (data) {
            seccionProd.innerHTML = "";
            if (seccion == 'resultadosBusquedaEliminar') {
                data.forEach(prod => {
                    let precio;
                    if (prod.precio != 0) {
                        precio = `<p class= "card-text"> $${prod.precio}</p>`
                    } else {
                        precio = '<p class="card-text" style="height: 24px;"> </p>'
                    }
                    seccionProd.innerHTML += `
                <div class="col-md-4 mb-4 d-flex">
                    <div class="card product-card flex-fill">
                            <img src="${prod.photoUrl}" class="card-img-top product-card-img" alt="${prod.nombre}">
                            <div class="card-body">
                                    <h5 class="card-title text-center">${prod.nombre}</h5>
                                    ${precio}
                                <div class="text-center">
                                        <a href="#" class="btn btn-danger col-10" data-bs-toggle="modal" data-bs-target="#confirmarEliminarModal" data-producto-id="${prod.id}">Eliminar</a>
                                </div>
                            </div>
                    </div>
                </div >
                `
                });
            } else {
                data.forEach(prod => {
                    let precio;
                    if (prod.precio != 0) {
                        precio =`<p class= "card-text"> $${ prod.precio }</p>`
                    } else {
                        precio = '<p class="card-text" style="height: 24px;"> </p>'
                    }
                    seccionProd.innerHTML += `
                <div class="col-md-4 mb-4 d-flex">
                    <div class="card product-card flex-fill">
                            <img src="${prod.photoUrl}" class="card-img-top product-card-img" alt="${prod.nombre}">
                            <div class="card-body">
                                    <h5 class="card-title text-center">${prod.nombre}</h5>
                                    ${precio}
                                <div class="text-center">
                                        <a href="#" class="btn btn-warning col-10">Modificar</a>
                                </div>
                            </div>
                    </div>
                </div >
                `
                });
            }
        },
        error: function (xhr, status, error) {
            console.error('AJAX error:', status, error);
            $('#subcategoriaProducto')
                .html('<option value="">Error cargando subcategorías</option>');
        }
    });
}


$(function () {
    $('#categoriaProducto').on('change', function () {
        var categoriaId = $(this).val();
        if (!categoriaId) {
            $('#subcategoriaProducto')
                .html('<option value="">Selecciona primero una categoría</option>');
            return;
        }

        $.ajax({
            url: '/Admin/GetSubcategoriasPorCategoria',
            method: 'GET',
            data: { categoriaId: categoriaId },
            dataType: 'json',
            success: function (data) {
                // data es siempre un arreglo (posiblemente vacío)
                var $sub = $('#subcategoriaProducto');
                if (!Array.isArray(data) || data.length === 0) {
                    $sub.html('<option value="">No hay subcategorías disponibles</option>');
                    return;
                }

                // Construye las opciones
                var html = '<option value="">Selecciona una subcategoría</option>';
                data.forEach(function (item) {
                    html += '<option value="' + item.id + '">' + item.nombre + '</option>';
                });
                $sub.html(html);
            },
            error: function (xhr, status, error) {
                console.error('AJAX error:', status, error);
                $('#subcategoriaProducto')
                    .html('<option value="">Error cargando subcategorías</option>');
            }
        });
    });


    $('#subcategoriaProducto').on('change', function () {
        var subcategoriaId = $(this).val();
        if (!subcategoriaId) {
            $('#categoriaEspecialProducto')
                .html('<option value="">Selecciona primero una subcategoria</option>');
            return;
        }

        $.ajax({
            url: '/Admin/GetCategoriaEspecialPorSubcategoria',
            method: 'GET',
            data: { subcategoriaId: subcategoriaId },
            dataType: 'json',
            success: function (data) {
                // data es siempre un arreglo (posiblemente vacío)
                var $sub = $('#categoriaEspecialProducto');
                if (!Array.isArray(data) || data.length === 0) {
                    $sub.html('<option value="">No hay categorias especiales disponibles</option>');
                    return;
                }

                // Construye las opciones
                var html = '<option value="">Selecciona una subcategoría</option>';
                data.forEach(function (item) {
                    html += '<option value="' + item.id + '">' + item.nombre + '</option>';
                });
                $sub.html(html);
            },
            error: function (xhr, status, error) {
                console.error('AJAX error:', status, error);
                $('#subcategoriaProducto')
                    .html('<option value="">Error cargando categoria especiales</option>');
            }
        });
    });

    
    $('#categoriaSEliminar').on('change', function () {
        var categoriaId = $(this).val();
        if (!categoriaId) {
            $('#subcategoriaEliminar')
                .html('<option value="">Selecciona primero categoría</option>');
            return;
        }
        $.ajax({
            url: '/Admin/GetSubcategoriasPorCategoria',
            method: 'GET',
            data: { categoriaId: categoriaId },
            dataType: 'json',
            success: function (data) {
                // data es siempre un arreglo (posiblemente vacío)
                var $sub = $('#subcategoriaEliminar');
                if (!Array.isArray(data) || data.length === 0) {
                    $sub.html('<option value="">No hay subcategorías disponibles</option>');
                    return;
                }

                // Construye las opciones
                var html = '<option value="">Selecciona una subcategoría</option>';
                data.forEach(function (item) {
                    html += '<option value="' + item.id + '">' + item.nombre + '</option>';
                });
                $sub.html(html);
            },
            error: function (xhr, status, error) {
                console.error('AJAX error:', status, error);
                $('#subcategoriaProducto')
                    .html('<option value="">Error cargando subcategorías</option>');
            }
        });
    });
});