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