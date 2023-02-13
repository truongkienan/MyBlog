ClassicEditor
    .create(document.querySelector('#Content'), {
        toolbar: {
            items: [
                'heading', '|',
                'fontfamily', 'fontsize', 'fontColor', 'fontBackgroundColor', '|', 'alignment', '|',
                'bold', 'italic', 'strikethrough', 'underline', 'subscript', 'superscript', '|',
                'link', '|',
                'bulletedList', 'numberedList', 'todoList',
                '-',
                'insertTable', '|',
                'outdent', 'indent', '|',
                'insertImage', 'code', 'codeBlock', '|',
                'blockQuote', 'undo', 'redo', '|',
            ]
        },
        simpleUpload: {
            uploadUrl: '/dashboard/topic/upload',
            withCredentials: true,
            headers: {
                'X-CSRF-TOKEN': 'CSRF-Token',
                Authorization: 'Bearer <JSON Web Token>'
            }
        },
        shouldNotGroupWhenFull: true,
        language: 'en',
        heading: {
            options: [
                { model: 'paragraph', title: 'Paragraph', class: 'ck-heading_paragraph' },
                { model: 'heading1', view: 'h1', title: 'Heading 1', class: 'ck-heading_heading1' },
                { model: 'heading2', view: 'h2', title: 'Heading 2', class: 'ck-heading_heading2' },
                { model: 'heading3', view: 'h2', title: 'Heading 3', class: 'ck-heading_heading3' },
                { model: 'heading4', view: 'h2', title: 'Heading 4', class: 'ck-heading_heading4' }
            ]
        },
        fontSize: {
            options: [9, 10, 11, 12, 13, 'default', 14, 15, 16, 17, 18, 19, 20]
        },
        image: {
            toolbar: [
                'imageStyle:inline',
                'imageStyle:block',
                'imageStyle:side',
                '|',
                'toggleImageCaption',
                'imageTextAlternative',
                'imageStyle:wrapText',
                'imageStyle:breakText'
            ],
        },
        table: {
            contentToolbar: [
                'tableColumn',
                'tableRow',
                'mergeTableCells',
                'tableProperties',
                'tableCellProperties'
            ]
        },
        licenseKey: '',
    })
    .then(editor => {
        window.editor2 = editor;
    })
    .catch(error => {
        console.error('Oops, something went wrong!');
        console.error('Please, report the following error on https://github.com/ckeditor/ckeditor5/issues with the build id and the error stack trace:');
        console.warn('Build id: fgydboej4r6a-nohdljl880ze');
        console.error(error);
    });