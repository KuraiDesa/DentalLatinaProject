tinymce.init({
    selector: '#mensaje',
    height: 300,
    plugins: 'image link lists code paste',
    toolbar: 'undo redo | formatselect | bold italic underline | alignleft aligncenter alignright | bullist numlist | image link | code',
    menubar: false,
    branding: false,
    automatic_uploads: true,
    images_upload_handler: function (blobInfo) {
        return new Promise((resolve, reject) => {
            const formData = new FormData();
            formData.append('file', blobInfo.blob(), blobInfo.filename());

            fetch('/Admin/UploadImagen', {
                method: 'POST',
                body: formData
            })
                .then(response => response.json())
                .then(json => {
                    if (json.location) {
                        resolve(json.location);
                    } else {
                        reject('No se recibió la URL de la imagen');
                    }
                })
                .catch(() => {
                    reject('Error al subir la imagen');
                });
        });
    }
});