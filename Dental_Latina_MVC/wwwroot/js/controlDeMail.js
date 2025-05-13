document.addEventListener('DOMContentLoaded', function () {
    // Variables globales
    let codigoGenerado = null;
    let nombre, apellido, email, esEstudiante;
    let resendAttempts = 0;
    const MAX_RESEND_ATTEMPTS = 3;
    let resendCooldown = false;
    let cooldownInterval;

    // Elementos del formulario principal
    const clienteForm = document.getElementById("clienteForm");
    const passMessage = document.getElementById("success-message2");
    const errorMessage = document.getElementById("error-message2");

    // Elementos del modal de verificación
    const codeInputs = document.querySelectorAll('.code-input');
    const verificarBtn = document.getElementById('verificarBtn');
    const reenviarBtn = document.getElementById('reenviarBtn');
    const modalErrorMessage = document.querySelector('.status-message.error');
    const modalErrorMessage2 = document.getElementById('sotito')
    console.log(modalErrorMessage)
    const modalSuccessMessage = document.querySelector('.status-message.success');
    const resendSuccess = document.querySelector('.status-message.resend-success');
    const modal = document.getElementById('codigoModal');
    const submitBtn = document.querySelector('#clienteForm button[type="submit"]');
    const originalBtnText = submitBtn.innerHTML;
    // Funciones auxiliares
    const hideAllMessages = () => {
        modalErrorMessage.classList.remove('visible');
        modalSuccessMessage.classList.remove('visible');
        resendSuccess.classList.remove('visible');
        errorMessage.style.display = "none";
        passMessage.style.display = "none";
    };

    const clearInputs = () => {
        codeInputs.forEach(input => {
            input.value = '';
            input.classList.remove('filled', 'shake');
        });
    };

    const startCooldown = (seconds) => {
        resendCooldown = true;
        reenviarBtn.disabled = true;

        const updateButtonText = () => {
            reenviarBtn.innerHTML = `Espere ${seconds}s`;
            if (seconds <= 0) {
                clearInterval(cooldownInterval);
                resendCooldown = false;
                reenviarBtn.disabled = false;
                reenviarBtn.innerHTML = 'Reenviar correo';
                return;
            }
            seconds--;
        };

        updateButtonText();
        cooldownInterval = setInterval(updateButtonText, 1000);
    };

    // Animación de shake para inputs
    const shakeInputs = () => {
        codeInputs.forEach(input => {
            input.classList.add('shake');
            setTimeout(() => input.classList.remove('shake'), 400);
        });
    };

    // Manejo del formulario principal
    clienteForm.addEventListener("submit", async function (event) {
        event.preventDefault();

        nombre = document.getElementById("nombre").value;
        apellido = document.getElementById("apellido").value;
        email = document.getElementById("email2").value;
        esEstudiante = document.getElementById("esEstudiante").value === "true";

        try {
            // Mostrar loading en el botón de submit
            const submitBtn = event.target.querySelector('button[type="submit"]');
            
            submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Procesando...';
            submitBtn.disabled = true;

            const response = await fetch('/Home/ingresoCliente', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ nombre, apellido, email, esEstudiante })
            });

            const result = await response.json();
            hideAllMessages();

            if (result.success) {
                codigoGenerado = result.codigo;
                const modalInstance = new bootstrap.Modal(modal);
                modalInstance.show();
            } else {
                errorMessage.textContent = result.error;
                errorMessage.style.display = "block";
                errorMessage.classList.add('animate__animated', 'animate__headShake');
                setTimeout(() => {
                    errorMessage.classList.remove('animate__animated', 'animate__headShake');
                }, 1000);
            }
        } catch (error) {
            errorMessage.textContent = "Error de conexión. Intente nuevamente.";
            errorMessage.style.display = "block";
            errorMessage.classList.add('animate__animated', 'animate__headShake');
            setTimeout(() => {
                errorMessage.classList.remove('animate__animated', 'animate__headShake');
            }, 1000);
        } finally {
            // Restaurar botón
            const submitBtn = event.target.querySelector('button[type="submit"]');
            console.log(submitBtn);
            submitBtn.innerHTML = originalBtnText;
            submitBtn.disabled = false;
        }
    });

    // Manejo de inputs del código
    codeInputs.forEach((input, index) => {
        // Animación al enfocar
        input.addEventListener('focus', () => {
            input.classList.add('animate__animated', 'animate__pulse');
            setTimeout(() => {
                input.classList.remove('animate__animated', 'animate__pulse');
            }, 1000);
        });

        input.addEventListener('input', (e) => {
            if (e.target.value.length === 1) {
                e.target.classList.add('filled', 'animate__animated', 'animate__bounceIn');
                setTimeout(() => {
                    e.target.classList.remove('animate__animated', 'animate__bounceIn');
                }, 1000);

                if (index < codeInputs.length - 1) {
                    codeInputs[index + 1].focus();
                } else {
                    verificarCodigo(); // Verificación automática al completar
                }
            } else {
                e.target.classList.remove('filled');
            }
            //hideAllMessages();
        });

        input.addEventListener('keydown', (e) => {
            if (e.key === 'Backspace' && !e.target.value && index > 0) {
                codeInputs[index - 1].focus();
            }
        });
    });

    // Verificación del código
    const verificarCodigo = async () => {
        const codigoIngresado = Array.from(codeInputs).map(input => input.value).join('');

        if (codigoIngresado.length !== 6) {
            modalErrorMessage.textContent = 'Por favor complete todos los dígitos';
            modalErrorMessage.classList.add('visible', 'animate__animated', 'animate__headShake');
            setTimeout(() => {
                modalErrorMessage.classList.remove('animate__animated', 'animate__headShake');
            }, 1000);

            shakeInputs();
            return;
        }

        verificarBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Verificando...';
        verificarBtn.disabled = true;

        try {
            console.log(codigoGenerado)
            if (codigoIngresado === codigoGenerado) {
                const registroResponse = await fetch('/Home/registroCliente', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ nombre, apellido, email, esEstudiante })
                });

                const registroResult = await registroResponse.json();

                if (registroResult.success) {
                    modalSuccessMessage.classList.add('visible', 'animate__animated', 'animate__fadeIn');
                    setTimeout(() => {
                        modalSuccessMessage.classList.remove('animate__animated', 'animate__fadeIn');
                    }, 1000);

                    // Restaurar el botón del formulario principal ANTES de cerrar el modal
                    
                    if (submitBtn) {
                        submitBtn.innerHTML = 'Registrarse';
                        submitBtn.disabled = false;
                    }

                    setTimeout(() => {
                        clearInputs();
                        bootstrap.Modal.getInstance(modal).hide();
                        passMessage.textContent = "Registrado correctamente y código verificado.";
                        passMessage.style.display = "block";
                        passMessage.classList.add('animate__animated', 'animate__fadeIn');
                        setTimeout(() => {
                            passMessage.classList.remove('animate__animated', 'animate__fadeIn');
                        }, 1000);
                    }, 2000);
                } else {
                    modalErrorMessage.textContent = registroResult.error || "Error al registrar el cliente.";
                    modalErrorMessage.classList.add('visible', 'animate__animated', 'animate__headShake');
                    setTimeout(() => {
                        modalErrorMessage.classList.remove('animate__animated', 'animate__headShake');
                    }, 1000);
                }
            } else {
                modalErrorMessage.textContent = 'Código incorrecto. Intente nuevamente.';
                modalErrorMessage.classList.add('visible', 'animate__animated', 'animate__headShake');
                console.log("se supone" + modalErrorMessage)
                setTimeout(() => {
                    modalErrorMessage.classList.remove('animate__animated', 'animate__headShake');
                }, 1000);   

                shakeInputs();
                setTimeout(() => {
                    clearInputs();
                    codeInputs[0].focus();
                }, 1000);
            }
        } catch (error) {
            modalErrorMessage.textContent = "Error de conexión. Intente nuevamente.";
            modalErrorMessage.classList.add('visible', 'animate__animated', 'animate__headShake');
            modalErrorMessage2.style.opacity('1');
            setTimeout(() => {
                modalErrorMessage.classList.remove('animate__animated', 'animate__headShake');
            }, 1000);
        } finally {
            verificarBtn.innerHTML = 'Verificar';
            verificarBtn.disabled = false;
        }
    };

    // Botón de verificación manual
    verificarBtn.addEventListener('click', verificarCodigo);

    // Reenvío de código
    reenviarBtn.addEventListener('click', async () => {
        //hideAllMessages();
        resendAttempts++;

        if (resendAttempts >= MAX_RESEND_ATTEMPTS) {
            modalErrorMessage.textContent = 'Límite de reenvíos alcanzado. Espere 2 minutos.';
            modalErrorMessage.classList.add('visible', 'animate__animated', 'animate__headShake');
            setTimeout(() => {
                modalErrorMessage.classList.remove('animate__animated', 'animate__headShake');
            }, 1000);
            startCooldown(120);
            return;
        }

        if (resendCooldown) {
            modalErrorMessage.textContent = 'Espere antes de reenviar';
            modalErrorMessage.classList.add('visible', 'animate__animated', 'animate__headShake');
            setTimeout(() => {
                modalErrorMessage.classList.remove('animate__animated', 'animate__headShake');
            }, 1000);
            return;
        }

        reenviarBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Enviando...';
        reenviarBtn.disabled = true;

        try {
            const responseReenvio = await fetch('/Home/ingresoCliente', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ nombre, apellido, email, esEstudiante })
            });

            const reenvioResult = await responseReenvio.json();

            if (reenvioResult.success) {
                codigoGenerado = reenvioResult.codigo;
                resendSuccess.classList.add('visible', 'animate__animated', 'animate__fadeIn');
                setTimeout(() => {
                    resendSuccess.classList.remove('animate__animated', 'animate__fadeIn');
                }, 1000);

                clearInputs();
                startCooldown(30);

                setTimeout(() => {
                    resendSuccess.classList.remove('visible');
                }, 3000);
            } else {
                modalErrorMessage.textContent = "Hubo un error al reenviar el código.";
                modalErrorMessage.classList.add('visible', 'animate__animated', 'animate__headShake');
                setTimeout(() => {
                    modalErrorMessage.classList.remove('animate__animated', 'animate__headShake');
                }, 1000);
            }
        } catch (error) {
            modalErrorMessage.textContent = "Error de conexión. Intente nuevamente.";
            modalErrorMessage.classList.add('visible', 'animate__animated', 'animate__headShake');
            setTimeout(() => {
                modalErrorMessage.classList.remove('animate__animated', 'animate__headShake');
            }, 1000);
        } finally {
            if (resendAttempts < MAX_RESEND_ATTEMPTS && !resendCooldown) {
                reenviarBtn.disabled = false;
                reenviarBtn.innerHTML = 'Reenviar correo';
            }
        }
    });

    // Reset al abrir modal
    modal.addEventListener('shown.bs.modal', () => {
        clearInputs();
        //hideAllMessages();
        resendAttempts = 0;
        clearInterval(cooldownInterval);
        resendCooldown = false;
        reenviarBtn.disabled = false;
        reenviarBtn.innerHTML = 'Reenviar correo';
        verificarBtn.innerHTML = 'Verificar';
        verificarBtn.disabled = false;
        codeInputs[0].focus();
    });

    // Limpieza al cerrar
    modal.addEventListener('hidden.bs.modal', () => {
        clearInterval(cooldownInterval);
    });
});