window.jsinterop = {
    initEntryEditor: function () {
        tinymce.init({
            selector: '#entryEditor',
            autoresize_min_height: 200,
            plugins: 'autosave preview searchreplace visualchars image link media fullscreen code codesample table hr pagebreak nonbreaking anchor insertdatetime advlist lists textcolor wordcount imagetools colorpicker',
            menubar: "edit view format insert table",
            toolbar1: 'preview | formatselect | bold blockquote forecolor backcolor | imageupload link | alignleft aligncenter alignright  | numlist bullist outdent indent | fullscreen',
            selection_toolbar: 'bold italic | quicklink h2 h3 blockquote',
            paste_data_images: true,
            image_advtab: true,
            file_picker_types: 'image',
            relative_urls: false,
            convert_urls: false,
            branding: false,
        });
    },

    getEditorContent: function () {
        var editor = tinymce.get()[0];
        return editor.getContent();
    }
}
