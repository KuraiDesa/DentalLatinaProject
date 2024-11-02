
function prueba() {
    console.log('entree')
}

function mostrarSeccion(seccionID) {
    // Array con los IDs de las secciones
    const secciones = ['estadisticas', 'gestionProductos', 'clientes', 'cambioBanner', 'envios'];

    // Itera sobre las secciones
    secciones.forEach(id => {
        const seccion = document.getElementById(id);
        // Si el ID coincide con el seleccionado, se muestra; si no, se oculta
        if (id === seccionID) {
            seccion.style.display = 'block';
        } else {
            seccion.style.display = 'none';
        }
    });
}

const nav = document.querySelector("#nav");
const abrir = document.querySelector("#abrir");
const cerrar = document.querySelector("#cerrar");

abrir.addEventListener("click", () => {
    nav.classList.add("visible");
})

cerrar.addEventListener("click", () => {
    nav.classList.remove("visible");
})

const crearProductoB = document.getElementById("crearProductoB");
const modificarProductoB = document.querySelector("#modificarProductoB");
const eliminarProductoB = document.querySelector("#eliminarProductoB");

const crearProducto = document.getElementById("crearProducto");
const modificarProducto = document.querySelector("#modificarProducto");
const eliminarProducto = document.querySelector("#eliminarProducto");

crearProductoB.addEventListener("click", () => {
    crearProducto.style.display = "block";
    modificarProducto.style.display = "none";
    eliminarProducto.style.display = "none";
});

modificarProductoB.addEventListener("click", () => {
    crearProducto.style.display = "none";
    modificarProducto.style.display = "block";
    eliminarProducto.style.display = "none";
});

eliminarProductoB.addEventListener("click", () => {
    crearProducto.style.display = "none";
    modificarProducto.style.display = "none";
    eliminarProducto.style.display = "block";
});

//Alta de imagenes
function vistaPreviaImagen(event) {
    const archivo = event.target.files[0];
    const vistaPrevia = document.getElementById('vistaPrevia');
    
    if (archivo) {
        const lector = new FileReader();
        lector.onload = function(e) {
            vistaPrevia.src = e.target.result;
            vistaPrevia.style.display = 'block';
        };
        lector.readAsDataURL(archivo);
    } else {
        vistaPrevia.src = '';
        vistaPrevia.style.display = 'none';
    }
}

// Script para manejar la funcionalidad de arrastrar y soltar
const dropZone = document.getElementById('dropZone');
const inputFile = document.getElementById('imagenProducto');

dropZone.addEventListener('dragover', (e) => {
    e.preventDefault();
    dropZone.classList.add('bg-light');
});

dropZone.addEventListener('dragleave', () => {
    dropZone.classList.remove('bg-light');
});

dropZone.addEventListener('drop', (e) => {
    e.preventDefault();
    dropZone.classList.remove('bg-light');
    inputFile.files = e.dataTransfer.files;

    // Actualizar la vista previa cuando se arrastra y suelta la imagen
    vistaPreviaImagen({ target: inputFile });
});

//cambio de banner 
let imagenSeleccionada = null;

function cambiarImagen(numero) {
    imagenSeleccionada = numero;
    document.getElementById('formularioCambio').style.display = 'block';
}

function subirImagen() {
    if (imagenSeleccionada === null) {
        alert('Selecciona una imagen para cambiar.');
        return;
    }

    const archivo = document.getElementById('nuevaImagen').files[0];
    if (!archivo) {
        alert('Selecciona un archivo.');
        return;
    }

    // Aquí deberías implementar la lógica para subir la imagen al servidor
    const formData = new FormData();
    formData.append('imagen', archivo);
    formData.append('numeroImagen', imagenSeleccionada);

    fetch('/ruta-a-tu-api/cambiar-imagen', {
        method: 'POST',
        body: formData
    }).then(response => response.json())
      .then(data => {
          if (data.success) {
              actualizarVistaPrevia(imagenSeleccionada, URL.createObjectURL(archivo));
              alert('Imagen cambiada con éxito.');
          } else {
              alert('Error al cambiar la imagen.');
          }
      }).catch(error => {
          alert('Error de red.');
          console.error('Error:', error);
      });

    document.getElementById('formularioCambio').style.display = 'none';
}

function actualizarVistaPrevia(numero, url) {
    document.getElementById(`previewBanner${numero}`).src = url;
}

function cancelarCambio() {
    document.getElementById('formularioCambio').style.display = 'none';
}

function eliminarImagen(numero) {
    if (!confirm('¿Estás seguro de que quieres eliminar esta imagen?')) {
        return;
    }

    // Aquí deberías implementar la lógica para eliminar la imagen en el servidor
    fetch(`/ruta-a-tu-api/eliminar-imagen/${numero}`, {
        method: 'DELETE'
    }).then(response => response.json())
      .then(data => {
          if (data.success) {
              document.getElementById(`previewBanner${numero}`).src = 'path/to/placeholder.jpg'; // Imagen de marcador de posición
              alert('Imagen eliminada con éxito.');
          } else {
              alert('Error al eliminar la imagen.');
          }
      }).catch(error => {
          alert('Error de red.');
          console.error('Error:', error);
      });
}