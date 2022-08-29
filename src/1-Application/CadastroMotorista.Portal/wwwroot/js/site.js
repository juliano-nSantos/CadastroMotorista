// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    var cpfMascara = function (val) {
        return '000.000.000-000';
    },
        cpfOptions = {
            onKeyPress: function (val, e, field, options) {
                field.mask(cpfMascara.apply({}, arguments), options);
            }
        };
    $('.cpf').mask(cpfMascara, cpfOptions);
});



function VerificarCheckBox() {
    var check = document.getElementById('ativo');
    var ativo = document.getElementById('active');

    if (check.checked) {
        ativo.value = 'true';
    }
    else {
        ativo.value = 'false';
    }
};
