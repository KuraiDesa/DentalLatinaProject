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

function modificar(id) {
    let modal = document.getElementById('modalModificarProducto');
    $.ajax({
        url: '/Admin/ProductoModify',
        method: 'GET',
        data: {
            id: id
        },
        dataType: 'json',
        success: function (data) {
            modal.innerHTML = ''
            modal.innerHTML = `
                <div class="d-none">
                        <label name="modifyproducto.id" class="form-label">Nombre del Producto</label>
                        <input name="modifyproducto.id" type="text" id="nombreProducto" class="form-control" value="${data.id}" required>
                        </div>
            <div class="d-flex flex-column flex-lg-row">
            <div class="d-flex align-items-center justify-content-center m-auto flex-column w-75 lg-w50 container"  >
                <label for="imagenProducto" class="form-label">Imagen del Producto (Si no carga una quedara la anteriormente cargada)</label>
                <img src="${data.photoUrl}" class="card-img-top product-card-img " id="vistaPreviaModify"alt="${data.nombre}" >
                <input name="modifyproducto.ImagenArchivo" type="file" class="form-control mt-3  accept="image/*" onchange="vistaPreviaImagenModify(event)">
                <input type="hidden" name="modifyproducto.PhotoUrl" value="${data.photoUrl}">
            </div>
            <div class="container w-75 lg-w50">
                <div class="mb-3">
                        <label name="modifyproducto.Nombre" class="form-label">Nombre del Producto</label>
                        <input name="modifyproducto.Nombre" type="text" id="nombreProductoModify" class="form-control" value="${data.nombre}" required>
                    </div>

                    <!-- Descripción del producto -->
                    <div class="mb-3">
                        <label name="modifyproducto.Descripcion" class="form-label">Descripción del Producto</label>
                        <textarea name="modifyproducto.Descripcion" id="descripcionProductoModify" class="form-control" rows="3" required >${data.descripcion}</textarea>
                    </div>

                    <!-- Subir documentación opcional -->
                    <div class="mb-3">
                        <label name="modifyproducto.DocumentacionArchivo" class="form-label">Documentación del Producto (Si no carga una quedara la anteriormente cargada [Opcional])</label>
                        <input name="modifyproducto.DocumentacionArchivo" type="file" class="form-control" accept=".pdf,.doc,.docx">
                        <input type="hidden" name="modifyproducto.DocumentacionUrl" value="${data.documentacion}">
                        <p style="font-size:12px;">Por defecto quedara vacio</p>
                    </div>

                    <!-- Costo opcional -->
                    <div class="mb-3">
                        <label namer="modifyproducto.Precio" class="form-label">Costo del Producto (opcional)</label>
                        <input name="modifyproducto.Precio" type="number" id="costoProductoModify" class="form-control" step="0.01" min="0" value="${data.precio}">
                        <p style="font-size:12px;">Por defecto no tendra costo</p>
                    </div>

                    <!-- Selección de categoría -->
                    <div class="mb-3">
                        <label name="modifyproducto.CategoriaId" class="form-label">Categoría del Producto</label>
                        <select name="modifyproducto.CategoriaId" id="categoriaProductoModify" class="form-select" required onclick="verificarCategoria('categoriaProductoModify', 'catEspLabelModify', 'categoriaEspecialProductoModify', 'magiaModi', 'catEspecialmodify')">
                            <option value="">Selecciona una categoría</option>

                        </select>
                    </div>



                    <!-- Seleccion de subcategoria-->

                    <div class="mb-3">
                        <label name="modifyproducto.SubcategoriaId" class="form-label">Subcategoria del Producto</label>
                        <select name="modifyproducto.SubcategoriaId" id="subcategoriaProductoModify" class="form-select" required>
                            <option value="">Selecciona primero una categoría</option>
                        </select>
                    </div>
                    <div id="magiaModi" style="height:0px" class="transitionLarga mb-4">
                        <div class="mb-3 d-flex flex-column transition" style=" width: 0%;" id="catEspecialmodify">
                            <label name="modifyproducto.CategoriaEspecial" id="catEspLabelModify" class="form-label transition" style="display:none; opacity:0">Categoria especial del Producto</label>
                            <select name="modifyproducto.CategoriaEspecial" id="categoriaEspecialProductoModify" class="form-select transition" style="display:none;">
                                <option value="">Selecciona una categoria especial</option>

                            </select>
                        </div>
                    </div>
            </div>
            </div>`
            cargarCategoriasModificar(data.catId, data.scatId, data.cateId)
               
            
        },
        error: function (xhr, status, error) {
            console.error('AJAX error:', status, error);
            $('#subcategoriaProducto')
                .html('<option value="">Error cargando subcategorías</option>');
        }
    });
}

function cargarCategoriasModificar(id, idS, idCE) {

        $.ajax({
            url: '/Admin/GetAllCategorias',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                html = document.getElementById('categoriaProductoModify')
                html.innerHTML = '';
                html.innerHTML = '<option value="">Selecciona una categoría</option>';
                data.forEach(function (item) {
                    html.innerHTML += '<option value="' + item.id + '">' + item.nombre + '</option>';
                });

                document.getElementById('categoriaProductoModify').value = id;
                verificarCategoria('categoriaProductoModify', 'catEspLabelModify', 'categoriaEspecialProductoModify', 'magiaModi', 'catEspecialmodify')
                cargarPorCateModify(id, idS, idCE)
            }
        })
}

function cargarPorCateModify(id, idS, idCE) {
    $.ajax({
        url: '/Admin/GetSubcategoriasPorCategoria',
        method: 'GET',
        data: { categoriaId: id },
        dataType: 'json',
        success: function (data) {
            var $sub = $('#subcategoriaProductoModify');
            if (!Array.isArray(data) || data.length === 0) {
                $sub.html('<option value="">No hay subcategorías disponibles</option>');
                return;
            }

            var html = '<option value="">Selecciona una subcategoría</option>';
            data.forEach(function (item) {
                html += `<option value="${item.id}">${item.nombre}</option>`;
            });
            $sub.html(html);
            document.getElementById('subcategoriaProductoModify').value = idS;
            cargarPorScatModify(idS, idCE);
        },
        error: function (xhr, status, error) {
            console.error('AJAX error:', status, error);
            $('#subcategoriaProductoModify')
                .html('<option value="">Error cargando subcategorías</option>');
        }
    });
}

function cargarPorScatModify(idS, idCE) {
    if (idCE != 0) {

        $.ajax({
            url: '/Admin/GetCategoriaEspecialPorSubcategoria',
            method: 'GET',
            data: { subcategoriaId: idS },
            dataType: 'json',
            success: function (data) {
                // data es siempre un arreglo (posiblemente vacío)
                var $sub = $('#categoriaEspecialProductoModify');
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
                document.getElementById('categoriaEspecialProductoModify').value = idCE;
            },
            error: function (xhr, status, error) {
                console.error('AJAX error:', status, error);
                $('#subcategoriaProductoModify')
                    .html('<option value="">Error cargando categoria especiales</option>');
            }
        });
    } 
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
    $(document).on('change', '#categoriaProductoModify', function () {
        var categoriaId = $(this).val();
        if (!categoriaId) {
            $('#subcategoriaProductoModify')
                .html('<option value="">Selecciona primero una categoría</option>');
            return;
        }

        $.ajax({
            url: '/Admin/GetSubcategoriasPorCategoria',
            method: 'GET',
            data: { categoriaId: categoriaId },
            dataType: 'json',
            success: function (data) {
                var $sub = $('#subcategoriaProductoModify');
                if (!Array.isArray(data) || data.length === 0) {
                    $sub.html('<option value="">No hay subcategorías disponibles</option>');
                    return;
                }

                var html = '<option value="">Selecciona una subcategoría</option>';
                data.forEach(function (item) {
                    html += `<option value="${item.id}">${item.nombre}</option>`;
                });
                $sub.html(html);
            },
            error: function (xhr, status, error) {
                console.error('AJAX error:', status, error);
                $('#subcategoriaProductoModify')
                    .html('<option value="">Error cargando subcategorías</option>');
            }
        });
    });

    $('#subcateEliminarCE').on('change', function () {
        var subcategoriaId = $(this).val();
        if (!subcategoriaId) {
            $('#EliminarcategoriaEspecialProducto')
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
                var $sub = $('#EliminarcategoriaEspecialProducto');
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


    $(document).on('change', '#subcategoriaProductoModify', function () {
        var subcategoriaId = $(this).val();
        if (!subcategoriaId) {
            $('#categoriaEspecialProductoModify')
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
                var $sub = $('#categoriaEspecialProductoModify');
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
                $('#subcategoriaProductoModify')
                    .html('<option value="">Error cargando categoria especiales</option>');
            }
        });
    });


    
    $('#categoriaSEliminar').on('change', function () {
        var categoriaId = $(this).val();
        if (!categoriaId) {
            $('#subcategoriaEliminar')
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