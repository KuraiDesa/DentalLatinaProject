
function validarInput() {
    const input = document.getElementById('direccionInput');
    const boton = document.getElementById('cotizarBtn');

    boton.disabled = input.value.trim() === "";
}




function cotizar() {
    const direccion = document.getElementById('direccionInput').value.trim();
    if (direccion === "") return;

    document.getElementById('modalCotizar').style.display = 'flex';

    geocodificarDireccion(direccion);
}

function cerrarModal() {
    document.getElementById('modalCotizar').style.display = 'none';
}

// Función para geocodificar la dirección usando Nominatim (OpenStreetMap)
function geocodificarDireccion(direccion) {
    fetch(`https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(direccion + ', Uruguay')}&countrycodes=UY`)
        .then(response => response.json())
        .then(data => {
            if (data && data.length > 0) {
                const lat = data[0].lat;
                const lon = data[0].lon;

                mostrarMapa(lat, lon);
           } else {
                alert('No se encontró la dirección. Verificá que esté bien escrita.');
            }
        })
       .catch(error => {
           console.error('Error en geocodificación:', error);
            alert('Hubo un problema al buscar la dirección.');
        });
}

// Mostrar el mapa
var mapa;
var marcador;
async function mostrarMapa(lat, lon) {
    document.getElementById("costoEnvio").innerHTML = '';
    document.getElementById("montoMinimoEnvio").innerHTML = '';
    document.getElementById("horarioEnvio").innerHTML = '';

    if (!mapa) {
        mapa = L.map('map').setView([lat, lon], 14);
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {}).addTo(mapa);
    } else {
        mapa.setView([lat, lon], 14);
    }

    if (marcador) {
        mapa.removeLayer(marcador);
    }

    marcador = L.marker([lat, lon]).addTo(mapa);
    var polygon = L.polygon([
        [-34.898105101853496, -56.119510537170825],
        [-34.92001826312844, -56.13672597949052],
        [-34.93027303620346, -56.16212401668288],
        [-34.92464641320345, -56.17247495784069],
        [-34.91546053166873, -56.17170406161529],
        [-34.91677736387601, -56.19201675764329],
        [-34.91268892868928, -56.21271374699464],
        [-34.906132101963244, -56.21712711470293],
        [-34.8985731620726, -56.19605632172359],
        [-34.89065798064833, -56.19578952808001],
        [-34.89018341948082, -56.187549723082554],
        [-34.890582856196524, -56.185634304886904],
        [-34.88926005505177, -56.180764374703955],
        [-34.88861242587751, -56.17329007141494],
        [-34.887921621793325, -56.16472831521082],
        [-34.88537401063112, -56.16320108098917],
        [-34.88098384430765, -56.15776358114795],
        [-34.87929958402445, -56.15523065173761],
        [-34.88198396510064, -56.152465064198864],
        [-34.876424634034606, -56.14290653854805],
        [-34.8868155565402, -56.13004570776158],
        [-34.88837518254909, -56.128076458052746],
        [-34.898239083693696, -56.11944680867062]
    ], {
        color: 'blue',  // Color del borde
        fillColor: 'blue',  // Color del relleno
        fillOpacity: 0.3  // Opacidad del relleno
    }).addTo(mapa); //Zona 1
    var polygon2 = L.polygon([
        [-34.890583006666525, -56.19575726678218],
        [-34.89008276880163, -56.18741436507864],
        [-34.89047758603518, -56.185617331832546],
        [-34.88916158660389, -56.18077229798121],
        [-34.887845643679334, -56.16476252723815],
        [-34.88529297576688, -56.1633508173548],
        [-34.88084530641939, -56.157736425428666],
        [-34.879187483978676, -56.155170200637315],
        [-34.87924770222046, -56.15527245055405],
        [-34.87408906899146, -56.15922588984749],
        [-34.87343982051765, -56.1601805495274],
        [-34.869601324944846, -56.16687273077382],
        [-34.86809924435006, -56.16986448064594],
        [-34.86645058772304, -56.17258831261829],
        [-34.863885953714515, -56.17683035001453],
        [-34.862933351779766, -56.17906299917169],
        [-34.862860074250634, -56.179866752869074],
        [-34.86307990664071, -56.18084911849863],
        [-34.8631317258909, -56.1810473190297],
        [-34.86241058990351, -56.18340472039171],
        [-34.861043235887706, -56.186414386110826],
        [-34.85914099386281, -56.190114238728796],
        [-34.85833097688302, -56.19243318170895],
        [-34.85910012493832, -56.193000516548224],
        [-34.86045619172928, -56.196552684512795],
        [-34.860982488740106, -56.19655267351061],
        [-34.8614682842913, -56.199364942699006],
        [-34.86388281631998, -56.20269192695372],
        [-34.86937573592941, -56.2049074467986],
        [-34.86799266123742, -56.20640051451926],
        [-34.869968472790134, -56.20907359018541],
        [-34.87113418070232, -56.21290258643896],
        [-34.873070403336094, -56.217526280279856],
        [-34.89059805691438, -56.19576190142932]
    ], {
        color: 'red',  // Color del borde
        fillColor: 'red',  // Color del relleno
        fillOpacity: 0.3  // Opacidad del relleno
    }).addTo(mapa); //Zona 2
    var polygon3 = L.polygon([
        [-34.86729827833944, -56.16075526336054],
        [-34.8695018183822, -56.16688500927391],
        [-34.86797231258434, -56.16979190297212],
        [-34.86405767497157, -56.17636400168337],
        [-34.86291695903444, -56.178891733644875],
        [-34.86278733048137, -56.179902825847975],
        [-34.86305526710211, -56.18099826687963],
        [-34.86236541058, -56.18339440920825],
        [-34.85910575852283, -56.19003635003703],
        [-34.85827788951211, -56.19247453042988],
        [-34.85905402238427, -56.19304203630456],
        [-34.860399290276845, -56.196615230895034],
        [-34.8609166960417, -56.196615230895034],
        [-34.86141685104888, -56.199431749455755],
        [-34.86384859489396, -56.20279475410432],
        [-34.86919473565581, -56.20489663295193],
        [-34.86791859067675, -56.20636794870609],
        [-34.86988453462326, -56.209079372668214],
        [-34.871091669516666, -56.21294683020723],
        [-34.87298923570232, -56.2174838997677],
        [-34.87236343319416, -56.241498499281875],
        [-34.87053830468055, -56.24185685154603],
        [-34.85453544243848, -56.22196412498657],
        [-34.85019004973636, -56.21770020180462],
        [-34.8511008309573, -56.215990476225],
        [-34.849760755720496, -56.214824082985615],
        [-34.853252303003615, -56.20847421744524],
        [-34.85198593815659, -56.20687742154014],
        [-34.85164137476028, -56.206004080997545],
        [-34.85146220122352, -56.20633998120634],
        [-34.848788335982846, -56.204055860502464],
        [-34.84863672494198, -56.204022270392386],
        [-34.84714814013196, -56.200310573088316],
        [-34.84600475574305, -56.202293115382474],
        [-34.833685967863104, -56.18975371958415],
        [-34.834934405887935, -56.18765255480946],
        [-34.83692445778811, -56.181910191778016],
        [-34.835536666589796, -56.16854324542588],
        [-34.8330228724428, -56.14443862249351],
        [-34.82883303498989, -56.14102510669122],
        [-34.8357985223475, -56.1397490260177],
        [-34.83781185506937, -56.13892308983374],
        [-34.84404348132207, -56.13579669218318],
        [-34.84645222036407, -56.13503104496044],
        [-34.84888706813343, -56.133372140084376],
        [-34.84835211837318, -56.13379679466986],
        [-34.85345518694508, -56.13833647272429],
        [-34.853769315982696, -56.13951676401399],
        [-34.856334654923536, -56.14213254588738],
        [-34.8571984766572, -56.142036846593314],
        [-34.85800993729465, -56.14254724282709],
        [-34.86389968940118, -56.15203980450846],
        [-34.86730863499264, -56.16081161228027]
    ], {
        color: 'green',  // Color del borde
        fillColor: 'green',  // Color del relleno
        fillOpacity: 0.3  // Opacidad del relleno
    }).addTo(mapa); //Zona 3
    var polygon4 = L.polygon([
        [-34.82888909748878, -56.14087035940656],
        [-34.83559915654402, -56.13973180027513],
        [-34.83784210025236, -56.13884367328859],
        [-34.844140423582374, -56.1357012264711],
        [-34.84642045637445, -56.13497245598987],
        [-34.84896531823363, -56.1332921242485],
        [-34.84844213660614, -56.133770300661325],
        [-34.853505969059235, -56.13827868152595],
        [-34.85380447167637, -56.139495134254275],
        [-34.85635050713157, -56.1420806757735],
        [-34.85719916819873, -56.14200809941835],
        [-34.85802548760672, -56.14252520818148],
        [-34.86398289231507, -56.15201020386672],
        [-34.86955335114019, -56.16644912426345],
        [-34.873983859312624, -56.15917058835454],
        [-34.879299201641174, -56.155132821143624],
        [-34.88187950311789, -56.15245675909691],
        [-34.876295490302454, -56.14283378494076],
        [-34.884738444231466, -56.132558710461296],
        [-34.88840118656057, -56.12796520614344],
        [-34.89818962723537, -56.119444735150154],
        [-34.89757359064738, -56.08959996176537],
        [-34.893573187861875, -56.09256665702301],
        [-34.88527146793793, -56.099402812070224],
        [-34.88353849274074, -56.100552310643835],
        [-34.8759898520905, -56.10674673955526],
        [-34.870273972654644, -56.11144257247463],
        [-34.86996965200116, -56.10899125301596],
        [-34.853880714613936, -56.10206777823083],
        [-34.853763028511565, -56.10613113166566],
        [-34.847609569301184, -56.126419860724795],
        [-34.8497026252327, -56.12724435810968],
        [-34.855147889288745, -56.13165074144227],
        [-34.8548075708432, -56.132376498697],
        [-34.848797314704335, -56.13336884769744]
    ], {
        color: 'yellow',  // Color del borde
        fillColor: 'yellow',  // Color del relleno
        fillOpacity: 0.3  // Opacidad del relleno
    }).addTo(mapa); //Zona 4

    document.getElementById("costoEnvio").innerHTML = '<div class="spinner-border spinner-border-sm" role="status"><span class="visually-hidden">Cargando...</span></div>';
    document.getElementById("montoMinimoEnvio").innerHTML = '<div class="spinner-border spinner-border-sm" role="status"><span class="visually-hidden">Cargando...</span></div>';
    document.getElementById("horarioEnvio").innerHTML = '<div class="spinner-border spinner-border-sm" role="status"><span class="visually-hidden">Cargando...</span></div>';

    var punto = L.latLng(lat, lon);
    var polygonPoints = polygon.getLatLngs();
    var zonaEncontrada;
    if (isMarkerInsidePolygon(marcador, polygon)) {
        zonaEncontrada = await obtenerZona(1);
    } else if (isMarkerInsidePolygon(marcador, polygon2)) {
        zonaEncontrada = await obtenerZona(2);
    } else if (isMarkerInsidePolygon(marcador, polygon3)) {
        zonaEncontrada = await obtenerZona(3);
    } else if (isMarkerInsidePolygon(marcador, polygon4)) {
        zonaEncontrada = await obtenerZona(4);
    } else {

    }
    actualizarValores(zonaEncontrada);
}

function isMarkerInsidePolygon(marker, polygon) {
    const point = marker.getLatLng();
    const latlngs = polygon.getLatLngs()[0]; // Asumiendo un solo polígono (sin huecos)

    let inside = false;
    for (let i = 0, j = latlngs.length - 1; i < latlngs.length; j = i++) {
        const xi = latlngs[i].lat, yi = latlngs[i].lng;
        const xj = latlngs[j].lat, yj = latlngs[j].lng;

        const intersect = ((yi > point.lng) !== (yj > point.lng)) &&
            (point.lat < (xj - xi) * (point.lng - yi) / (yj - yi) + xi);
        if (intersect) inside = !inside;
    }
    return inside;
}

function actualizarValores(zona) {
    if (zona) {
        // Actualizar los valores obtenidos de obtenerZona()
        document.getElementById("costoEnvio").innerHTML = `$${zona.precio}`;
        document.getElementById("montoMinimoEnvio").innerHTML = `${zona.minimoDeEnvio}`;
        document.getElementById("horarioEnvio").innerHTML = `${zona.horario}`;
    } else {
        // Si no se encuentra zona, se muestran valores predeterminados
        document.getElementById("costoEnvio").innerHTML = 'No disponible';
        document.getElementById("montoMinimoEnvio").innerHTML = 'No disponible';
        document.getElementById("horarioEnvio").innerHTML = 'No disponible';
    }
}
async function obtenerZona(id) {
    try {
        const response = await fetch(`/Zona/Details?id=${id}`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            const errorData = await response.json();
            let mensajeError = errorData.error || "Error desconocido.";
            if (errorData.detalle) {
                mensajeError += " Detalle: " + errorData.detalle;
            }
            throw new Error(mensajeError);
        }

        const zona = await response.json();
        console.log("Zona recibida:", zona);
        return zona;
        // Acá podés usar "zona" normalmente
    } catch (error) {
        console.error("Error al obtener la zona:", error.message);
        alert(error.message);
    }
}

