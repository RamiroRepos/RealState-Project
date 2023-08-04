
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
    // Obtener el contador de imágenes
    var contadorImagenes = document.getElementById('ContadorImagenes').value;

    document.getElementById("MaximoImg").style.display = "none";

    if (cantImg > (6 - contadorImagenes)) {
        inputImg.value = '';
        document.getElementById("MaximoImg").style.display = "block";
        previewContainer.innerHTML = ''; // Limpiar el contenedor
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

function quitarImagenEdit(imagenId, event) {
    event.preventDefault();

    // Obtener el div de la imagen por su ID
    var divImagen = document.getElementById(imagenId);

    // Obtener el input por su ID
    var inputImagenToDel = document.getElementById('del' + imagenId);

    // Establecer el valor del input con el id "del+@imagenToDel.imagen"
    if (inputImagenToDel) {
        inputImagenToDel.value = imagenId;
    }

    // Ocultar el div de la imagen
    if (divImagen) {
        divImagen.style.display = 'none';
    }
    
    // Obtener el contador de imágenes
    var contadorImagenes = document.getElementById('ContadorImagenes');
    
    // Restar 1 al contador de imágenes
    contadorImagenes.value = parseInt(contadorImagenes.value) - 1;

    //document.getElementById("SelectorImagenes").style.display = "block";
    $("#SelectorImagenes").show();
}

