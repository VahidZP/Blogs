// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web /assets.

// Write your JavaScript code.
$(document).ready(() => {
    $('#ImageFile').change(function () {
        const file = this.files[0];
        console.log(file);
        if (file) {
            let reader = new FileReader();
            reader.onload = function (event) {
                console.log(event.target.result);
                $("#imgsPreview").attr("src", event.target.result);
            }
            reader.readAsDataURL(file);
        }
    });
}); 