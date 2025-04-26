let estadoDeMenu = false;

function despliegoMenuCelular() {
    const contenido = document.getElementById('contenido');

    if (!estadoDeMenu) {
        contenido.style.height = '230px'; // o 'auto' si prefieres, pero animaciones no funcionan con auto
        estadoDeMenu = true;
        setTimeout(cargoContMovil, 300); // si esta función existe y es necesaria
    } else {
        contenido.style.height = '0px';
        estadoDeMenu = false;
        borrarContenidoMenu(); // si esta función existe
    }
}

function cargoContMovil(){
    
    var contenidoDiv = document.getElementById('contenido');

    var contenidoDiv = document.getElementById('contenido');

    // Crear el elemento nav
    var nav = document.createElement('nav');

    // Crear la lista ul
    var ul = document.createElement('ul');

    // Crear los elementos li con enlaces
    var enlaces = [
        { href: '', text: 'Empresa' },
        { href: '', text: 'Productos' },
        { href: '', text: 'Envios' },
        { href: '', text: 'Contacto' },
        { href: '', text: 'Ingresar' }
    ];

    // Añadir los elementos li al ul
    enlaces.forEach(function(enlace) {
        var li = document.createElement('li');
        var a = document.createElement('a');
        a.href = enlace.href;
        a.textContent = enlace.text;
        li.appendChild(a);
        ul.appendChild(li);
        
        if(enlace.text == "Productos" || enlace.text == "Contacto"){
            var flecha = document.createElement('i');
            flecha.classList.add('fas', 'fa-caret-right', 'nav-dropdown-icon');
            li.appendChild(flecha);
        }
        
        
       
       
    });

    // Añadir el ul al nav
    nav.appendChild(ul);

    // Añadir el nav al div contenido
    contenidoDiv.appendChild(nav);

    // Forzar reflujo y añadir la clase visible para iniciar la transición
    requestAnimationFrame(() => {
        nav.classList.add('visible');
    });
    
}
function borrarContenidoMenu() {
    var contenidoDiv = document.getElementById('contenido');
    contenidoDiv.innerHTML = ''; 
}