let ultimoMenuDesplegado = null;

function despliegoNavBar(menu) {
    var li;
    if (menu == 1) {
        li = document.getElementById('liProductos');
       
    } else {
        li = document.getElementById('liContacto');
    }
    if(li === ultimoMenuDesplegado){
        cerrarMenuDesplegado(ultimoMenuDesplegado);
        if(menu==2){
            cambiopaulauwu.classList.remove("fa-envelope-open");
            cambiopaulauwu.classList.add("fas-fa-envelope");
        }
        ultimoMenuDesplegado = null
    }else if(li !== ultimoMenuDesplegado && ultimoMenuDesplegado !== null){
        if(menu == 2){
            let cambiopaulauwu= document.getElementById('cambiopaulauwu');
            cambiopaulauwu.classList.remove("fas-fa-envelope");
            cambiopaulauwu.classList.add("fa-envelope-open");
        }else{
            let cambiopaulauwu= document.getElementById('cambiopaulauwu');
            cambiopaulauwu.classList.remove("fa-envelope-open");
            cambiopaulauwu.classList.add("fas-fa-envelope");
        }
        ultimoMenuDesplegado.style.color="";
        rotoGrados90(ultimoMenuDesplegado);
        li.style.color = "blue";
        rotoGrados90(li);
        setTimeout(() => cargoMenu(menu), 500);
        ultimoMenuDesplegado = li;
    }else{
        
        abrirMenuDesplegado(li);
        ultimoMenuDesplegado = li;
        if(menu == 2){
            let cambiopaulauwu= document.getElementById('cambiopaulauwu');
            cambiopaulauwu.classList.remove("fas-fa-envelope");
            cambiopaulauwu.classList.add("fa-envelope-open");
        }
        setTimeout(() => cargoMenu(menu), 500);
    }
}
 function cargoMenu() {
    const contenido2 = document.getElementById('contenido2');
    contenido2.style.display = 'flex'; // Mostrar el contenido como flexbox
    setTimeout(() => {
        contenido2.style.opacity = '1'; // Cambiar la opacidad a 1 gradualmente
    }, 4);// Pequeño retardo para permitir que el navegador registre el cambio de display
        }
function DescargoMenu() {
    const contenido2 = document.getElementById('contenido2');
    contenido2.style.opacity = '0'; // Cambiar la opacidad a 0 inmediatamente
    
        contenido2.style.display = 'none'; // Ocultar el contenido después de la transición

}
function abrirMenuDesplegado(li) {
    li.style.color = "blue";
    rotoGrados90(li);
    cargoContenido();
}

function cerrarMenuDesplegado(li) {
    li.style.color = "";
    rotoGrados90(li);
    cargoContenido();
    DescargoMenu();
}

function cargoContenido() {
    const contenido = document.getElementById('contenido');
    const alturaActual = contenido.offsetHeight;

    contenido.style.transition = "height 0.5s ease";
    contenido.style.height = alturaActual !== 0 && ultimoMenuDesplegado ? "0" : "300px";
}

function rotoGrados90(li) {
    const segundoIcono = li.querySelector('i:nth-child(2)');
    if (segundoIcono) {
        segundoIcono.style.transform = segundoIcono.style.transform.includes('rotate(90deg)') ? "" : "rotate(90deg)";
    }
}




