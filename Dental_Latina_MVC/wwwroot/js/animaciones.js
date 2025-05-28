document.addEventListener('DOMContentLoaded', function () {
    const section = document.getElementById('animated-section');

    // Configura el Intersection Observer
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                // Agrega las clases de animación a los elementos
                entry.target.classList.add('animate__fadeIn');

                const h2 = entry.target.querySelector('h2');
                const p = entry.target.querySelector('p');

                h2.classList.add('animate__fadeInUp');
                p.classList.add('animate__fadeInUp');

                // Opcional: Dejar de observar después de la animación
                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.1 });

    // Comienza a observar la sección
    observer.observe(section);
});
        document.addEventListener('DOMContentLoaded', function () {
            // Configuración mejorada del Intersection Observer
            const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    // Animación para el título
                    const title = entry.target.querySelector('h2');
                    if (title) {
                        title.classList.add('animate__animated', 'animate__fadeInDown');
                        title.style.opacity = '1';
                        title.style.transform = 'translateY(0)';
                    }

                    // Animación para las tarjetas (versión escritorio)
                    const cards = entry.target.querySelectorAll('.animate-on-scroll:not(.d-block.d-md-none)');
                    cards.forEach((card, index) => {
                        setTimeout(() => {
                            card.style.opacity = '1';
                            card.style.transform = 'translateY(0)';
                        }, 100 * index);
                    });

                    // Animación para el carrusel (versión móvil)
                    const carousel = entry.target.querySelector('.d-block.d-md-none');
                    if (carousel) {
                        carousel.style.opacity = '1';
                        carousel.style.transform = 'translateY(0)';
                    }
                }
            });
            }, {
            threshold: 0.2, // Aumentamos el threshold para mayor seguridad
        rootMargin: '0px 0px -50px 0px' // Activa la animación un poco antes
            });

        // Observar la sección
        const section = document.getElementById('featured-products');
        if (section) {
            observer.observe(section);
            }
        });