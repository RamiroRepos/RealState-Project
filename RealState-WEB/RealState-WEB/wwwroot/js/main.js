/**
* Template Name: EstateAgency - v4.10.0
* Template URL: https://bootstrapmade.com/real-estate-agency-bootstrap-template/
* Author: BootstrapMade.com
* License: https://bootstrapmade.com/license/
*/


function EditarUsuario() {
    $('input[type="text"]').not('#Email').prop('readonly', false);
    $('select').removeClass('dropdown-disabled');
    $('input[value="Editar información"]').hide();
    $('input[type="submit"]').show();
}

function BuscarCorreo() {

    $("#btnRegistrar").prop("disabled", true);
    let correoValidar = $("#Email").val();


    $.ajax({
        type: "POST",
        url: "/Home/BuscarCorreo",
        dataType: "json",
        data: {
            "correoValidar": correoValidar
        },
        success: function (res) {

            if (res != "ERROR") {
                if (res == "") {
                    $("#btnRegistrar").prop("disabled", false);
                }
                else {
                    alert(res);
                }
            }

        }
    });

}

function ValidarConfirmacionContrasenna() {

    let contrasenna = $("#Contrasenna").val();
    let confirmarContrasenna = $("#ConfirmarContrasenna").val();

    if (contrasenna.trim() != "" && confirmarContrasenna.trim()) {
        if (contrasenna.trim() != confirmarContrasenna.trim()) {
            alert("Las contraseñas no coinciden");
            $("#ConfirmarContrasenna").val("");
        }
    }
}

var imagenCount = 0;

// Mostrar mensaje de máximo 6

function imagenesView() {
    var inputImg = document.getElementById("inputImg"); // Obtener el input de imágenes
    var imagenes = inputImg.files; // Obtener la lista de archivos seleccionados
    var cantImg = inputImg.files.length;

    document.getElementById("MaximoImg").style.display = "none";

    if (cantImg > 6) {
        inputImg.value = '';
        document.getElementById("MaximoImg").style.display = "block";
        return;
    }

    // Incrementar el contador de imágenes
    imagenCount += cantImg;

    var previewContainer = document.getElementById("previewContainer"); // Obtener el contenedor de la vista previa
    previewContainer.innerHTML = ''; // Limpiar el contenedor

    for (let i = 0; i < imagenes.length; i++) {
        var imagen = imagenes[i];

        // Crear un elemento de imagen para la vista previa
        var imgElement = document.createElement('img');
        imgElement.classList.add('preview-img');

        // Crear un objeto de lectura de archivos
        var reader = new FileReader();

        // Configurar el evento onload para mostrar la vista previa cuando se cargue el archivo
        reader.onload = (function (img) {
            return function (e) {
                img.src = e.target.result;
            };
        })(imgElement);

        // Leer el archivo como una URL de datos
        reader.readAsDataURL(imagen);

        // Crear un identificador único para el contenedor individual
        var containerId = imagen.name;

        // Crear un contenedor individual para cada imagen
        var imageContainer = document.createElement('div');
        imageContainer.classList.add('image-container');
        imageContainer.classList.add('col-4');
        imageContainer.setAttribute('id', containerId);

        // Agregar la imagen al contenedor individual
        imageContainer.appendChild(imgElement);

        // Crear un botón de eliminar para la imagen
        var deleteButton = document.createElement('button');
        deleteButton.classList.add('btn');
        deleteButton.classList.add('btn-danger');
        deleteButton.classList.add('delete-button');
        deleteButton.textContent = 'Quitar';

        // Agregar el evento onclick para eliminar el contenedor de la imagen
        deleteButton.onclick = function (id) {
            return function () {
                quitarImagen(id);
            };
        }(containerId);

        // Agregar el botón de eliminar al contenedor individual
        imageContainer.appendChild(deleteButton);

        // Agregar el contenedor individual al contenedor de la vista previa
        previewContainer.appendChild(imageContainer);
    }
}

function quitarImagen(containerId) {
    imagenCount--;
    var containerToRemove = document.getElementById(containerId);
    containerToRemove.remove();

    var inputImg = document.getElementById("inputImg"); // Obtener el input de imágenes
    var files = Array.from(inputImg.files); // Convertir la colección FileList en un array

    // Encontrar el índice de la imagen que se va a eliminar
    var indexToRemove = Array.from(files).findIndex(function (file) {
        return file.name === containerId;
    });

    // Eliminar la imagen del array
    if (indexToRemove > -1) {
        files.splice(indexToRemove, 1);
    }

    // Crear una nueva colección FileList a partir del array modificado
    var newFileList = new DataTransfer();
    files.forEach(function (file) {
        newFileList.items.add(file);
    });

    // Asignar la nueva colección FileList al elemento input
    inputImg.files = newFileList.files;
}

function quitarImagenEdit(imagenName) {
    // Obtener el índice de la imagen en la lista imagenes
    var index = model.imagenes.findIndex(function (imagen) {
        return imagen.imagen === imagenName;
    });

    if (index !== -1) {
        // Eliminar la imagen de la lista imagenes
        model.imagenes.splice(index, 1);
    }
}


(function () {
    "use strict";

    /**
     * Easy selector helper function
     */
    const select = (el, all = false) => {
        el = el.trim()
        if (all) {
            return [...document.querySelectorAll(el)]
        } else {
            return document.querySelector(el)
        }
    }

    /**
     * Easy event listener function
     */
    const on = (type, el, listener, all = false) => {
        let selectEl = select(el, all)
        if (selectEl) {
            if (all) {
                selectEl.forEach(e => e.addEventListener(type, listener))
            } else {
                selectEl.addEventListener(type, listener)
            }
        }
    }

    /**
     * Easy on scroll event listener 
     */
    const onscroll = (el, listener) => {
        el.addEventListener('scroll', listener)
    }

    /**
     * Toggle .navbar-reduce
     */
    let selectHNavbar = select('.navbar-default')
    if (selectHNavbar) {
        onscroll(document, () => {
            if (window.scrollY > 100) {
                selectHNavbar.classList.remove('navbar-reduce')
                selectHNavbar.classList.add('navbar-trans')
            } else {
                selectHNavbar.classList.add('navbar-reduce')
                selectHNavbar.classList.remove('navbar-trans')
            }
        })
    }

    /**
     * Back to top button
     */
    let backtotop = select('.back-to-top')
    if (backtotop) {
        const toggleBacktotop = () => {
            if (window.scrollY > 100) {
                backtotop.classList.add('active')
            } else {
                backtotop.classList.remove('active')
            }
        }
        window.addEventListener('load', toggleBacktotop)
        onscroll(document, toggleBacktotop)
    }

    /**
     * Preloader
     */
    let preloader = select('#preloader');
    if (preloader) {
        window.addEventListener('load', () => {
            preloader.remove()
        });
    }

    /**
     * Search window open/close
     */
    let body = select('body');
    on('click', '.navbar-toggle-box', function (e) {
        e.preventDefault()
        body.classList.add('box-collapse-open')
        body.classList.remove('box-collapse-closed')
    })

    on('click', '.close-box-collapse', function (e) {
        e.preventDefault()
        body.classList.remove('box-collapse-open')
        body.classList.add('box-collapse-closed')
    })

    /**
     * Intro Carousel
     */
    new Swiper('.intro-carousel', {
        speed: 600,
        loop: true,
        autoplay: {
            delay: 5000,
            disableOnInteraction: false
        },
        slidesPerView: 'auto',
        pagination: {
            el: '.swiper-pagination',
            type: 'bullets',
            clickable: true
        }
    });

    /**
     * Property carousel
     */
    new Swiper('#property-carousel', {
        speed: 600,
        loop: true,
        autoplay: {
            delay: 5000,
            disableOnInteraction: false
        },
        slidesPerView: 'auto',
        pagination: {
            el: '.propery-carousel-pagination',
            type: 'bullets',
            clickable: true
        },
        breakpoints: {
            320: {
                slidesPerView: 1,
                spaceBetween: 20
            },

            1200: {
                slidesPerView: 3,
                spaceBetween: 20
            }
        }
    });

    /**
     * News carousel
     */
    new Swiper('#news-carousel', {
        speed: 600,
        loop: true,
        autoplay: {
            delay: 5000,
            disableOnInteraction: false
        },
        slidesPerView: 'auto',
        pagination: {
            el: '.news-carousel-pagination',
            type: 'bullets',
            clickable: true
        },
        breakpoints: {
            320: {
                slidesPerView: 1,
                spaceBetween: 20
            },

            1200: {
                slidesPerView: 3,
                spaceBetween: 20
            }
        }
    });

    /**
     * Testimonial carousel
     */
    new Swiper('#testimonial-carousel', {
        speed: 600,
        loop: true,
        autoplay: {
            delay: 5000,
            disableOnInteraction: false
        },
        slidesPerView: 'auto',
        pagination: {
            el: '.testimonial-carousel-pagination',
            type: 'bullets',
            clickable: true
        }
    });

    /**
     * Property Single carousel
     */
    new Swiper('#property-single-carousel', {
        speed: 600,
        loop: true,
        autoplay: {
            delay: 4000,
            disableOnInteraction: false
        },
        pagination: {
            el: '.property-single-carousel-pagination',
            type: 'bullets',
            clickable: true
        }
    });

})()