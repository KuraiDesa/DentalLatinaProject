function adjustPadding() {
    const navbar = document.getElementById('navbar');
    const carruselSec = document.getElementById('carruselSec');
    const navbarHeight = navbar.offsetHeight;
    if(navbarHeight < 200){
        carruselSec.style.paddingTop = `${navbarHeight}px`;
    }
    
}
document.addEventListener('DOMContentLoaded', function() {
    setTimeout(function() {
        adjustPadding();
    }, 50);
})
window.addEventListener('load', adjustPadding);
window.addEventListener('resize', adjustPadding);

document.addEventListener('DOMContentLoaded', () => {
    const observer = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          entry.target.classList.add('visible');
          observer.unobserve(entry.target); 
        }
      });
    }, { threshold: 0.1 });
  
    document.querySelectorAll('.hidden').forEach(element => {
      observer.observe(element);
    });
  });