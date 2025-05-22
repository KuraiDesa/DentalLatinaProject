function filtrarAjax(id, tipo) {
    window.location.href = `/Producto/FilterAjax?id=${id}&tipo=${tipo}`;

}

function mostrarSub(id1, id2) {
    let div = document.getElementById(id1);
    let div2 = document.getElementById(id2);
    if (id2 == '') {
        if (!div.classList.contains('abierta')) {
            div.style.maxHeight = div.scrollHeight + 'px';
            div.classList.add('abierta');
            div.classList.remove('no_click')
        } else {
            div.classList.remove('abierta');
            div.classList.add('no_click');
            div.style.maxHeight = '0px'
        }
    } else {
        if (!div.classList.contains('abierta')) {           
            div.style.maxHeight = div.scrollHeight + 'px';
            let num1='';
            let num2='';
            for (let i=0; i < div.style.maxHeight.length;i++) {
                let n = div.style.maxHeight.charCodeAt(i);
                if (n >= 48 && n <= 57) {
                    num1 += div.style.maxHeight.charAt(i);
                } else {
                    break;
                }
            }
            for (let i=0; i < div2.style.maxHeight.length; i++) {
                let n = div2.style.maxHeight.charCodeAt(i);
                if (n >= 48 && n <= 57) {
                    num2 += div2.style.maxHeight.charAt(i);
                } else {
                    break;
                }
            }
            let tamaño = parseInt(num1) + parseInt(num2);
            div2.style.maxHeight = tamaño + 'px';
            div.classList.add('abierta');
            div.classList.remove('no_click')
        } else {
            div.classList.remove('abierta');
            div.classList.add('no_click');
            div.style.maxHeight = '0px'
        }
    }
    
        

    
}
function traerSubcategorias(categoria, categoriaId) {
    let html = document.getElementById(categoriaId);

    $.ajax({
        url: '/Producto/FilterSubcategorias',
        method: 'GET',
        data: { cateid: categoria },
        dataType: 'json',
        success: function (data) {
            html.innerHTML += `<div id="DivScat${categoria}" height:0px;" class="flex-column ml-3 p-1 transition subcategorias no_click" ></div>`
            let ht = document.getElementById(`DivScat${categoria}`)
            data.forEach(prod => {
                ht.innerHTML += ``
                ht.innerHTML += `<div  id="Dscat_${prod.id}" class="transition">
                <li  id="scatI${prod.id}" class="subcategoria-item no_p transition" data-id="${prod.id}" >
                <div  class="d-flex justify-content-between align-items-center pt-1 pb-1 pl-1 transition filtroLindo" id="DivSubcate${prod.id}">
                <a onclick="filtrarAjax(${prod.id},1)" style="width:85%; padding-left:5px">${prod.nombre}</a>
                </div>
                <div id="catEspecial_${prod.id}" class="ml-3 p-1 transition subcategorias no_click">
                </div>
                </li>
                </div>`;
                
            });
            
            if (!$.isEmptyObject(data)) {
                document.getElementById(`Dcat_${categoria}`).innerHTML += `<button type="button" onclick="mostrarSub('DivScat${categoria}' , '')"
                class="d-flex justify-content-center extender textoBlanco transition mr-1" style="width:15%"><a>+</a></button>`
            }
            
            data.forEach(prod => {
                traerCategoriaEspecial(prod.id, `catEspecial_${prod.id}`, `DivScat${categoria}`);
            });
        },
        error: function (err) {
            console.error("Error al traer subcategorías", err);
        }
    });
}
//▼
function traerCategoriaEspecial(categoria, categoriaId, idSubCSS) {
    let html = document.getElementById(categoriaId);

    $.ajax({
        url: '/Producto/FilterCategoriaEspecial',
        method: 'GET',
        data: { cateid: categoria },
        dataType: 'json',
        success: function (data) {
            
            data.forEach(prod => {
                html.innerHTML += `<div class="p-1 filtroLindo textoBlanco"><li  id="catEI${prod.id}" class="subcategoria-item no_p ">
                <a onclick="filtrarAjax(${prod.id},2)" style="width:85%; padding-left:5px">${prod.nombre}</a>
                </li><div>`;
            });
            if (!$.isEmptyObject(data)) {
                document.getElementById(`DivSubcate${categoria}`).innerHTML += `<button type="button" style="width:15%" onclick="mostrarSub('catEspecial_${categoria}', '${idSubCSS}')"
                class="d-flex justify-content-center extender textoBlanco transition mr-1  "><a>+</a></button>`
            }
            
        },
        error: function (err) {
            console.error("Error al traer subcategorías", err);
        }
    });
}