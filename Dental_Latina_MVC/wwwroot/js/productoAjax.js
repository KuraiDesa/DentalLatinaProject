function filtrarAjax(cat) {

}

function traerSubcategorias(categoria, categoriaId) {
    let html = document.getElementById(categoriaId);

    $.ajax({
        url: '/Producto/FilterSubcategorias',
        method: 'GET',
        data: { cateid: categoria },
        dataType: 'json',
        success: function (data) {
            html.innerHTML+="<ul>"
            data.forEach(prod => {
                html.innerHTML += `<li onclick="filtrarAjax(${prod.id})" id="scatI${prod.id}" class="subcategoria-item" data-id="${prod.id}">${prod.nombre}
                <div id="catEspecial_${prod.id}"></div>
                </li>`;
            });
            html.innerHTML += "</ul"

            data.forEach(prod => {
                traerCategoriaEspecial(prod.id, `catEspecial_${prod.id}`);
            });
        },
        error: function (err) {
            console.error("Error al traer subcategorías", err);
        }
    });
}

function traerCategoriaEspecial(categoria, categoriaId) {
    let html = document.getElementById(categoriaId);

    $.ajax({
        url: '/Producto/FilterCategoriaEspecial',
        method: 'GET',
        data: { cateid: categoria },
        dataType: 'json',
        success: function (data) {
            html.innerHTML += "<ul>"
            data.forEach(prod => {
                html.innerHTML += `<li onclick="filtrarAjax(${prod.id})" id="catEI${prod.id}" class="subcategoria-item" >${prod.nombre}</li>`;
            });
            html.innerHTML += "</ul"
        },
        error: function (err) {
            console.error("Error al traer subcategorías", err);
        }
    });
}