// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var flipbutton = document.getElementById('flipbutton');

flipbutton.addEventListener('click', function () {
    if (!this.classList.contains('on')) {
        this.classList.remove('off');
        this.classList.add('on');
    } else {
        this.classList.remove('on');
        this.classList.add('off');
    }
});
