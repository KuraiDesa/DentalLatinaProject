
        const image = document.getElementById('zoom-image');
        const container = document.querySelector('.image-container');
        const zoomFactor = 2; // Factor de zoom (2x)

// Cargar imagen seleccionada por el usuario


// Efecto de zoom
container.addEventListener('mousemove', (e) => {
        if (!image.src) return; // No hacer nada si no hay imagen

        const rect = image.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;

        const xPercent = (x / rect.width) * 100;
        const yPercent = (y / rect.height) * 100;

        image.style.transform = `scale(${zoomFactor})`;
        image.style.transformOrigin = `${xPercent}% ${yPercent}%`;
    });

    container.addEventListener('mouseleave', () => {
        image.style.transform = 'scale(1)';
        image.style.transformOrigin = 'center center';
    });
    

