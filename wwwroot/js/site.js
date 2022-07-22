// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

let fechaEntrada = document.getElementById("fechaEntrada");
let fechaSalida = document.getElementById("fechaSalida");
let totalHoras = document.getElementById("totalHoras");

let CostoHangar = document.getElementById("CostoHangar");
let totalMontoHangar = document.getElementById("totalMontoHangar");
CostoHangar.addEventListener("change", hangarPorHoras, true);

fechaSalida.addEventListener("blur", cantidadHoras, true);
function cantidadHoras() {
    let entrada = new Date(fechaEntrada.value);
    let salida = new Date(fechaSalida.value);
    let diff = salida.getTime() - entrada.getTime();

    totalHoras.value = diff / 1000 / 60 / 60;

    hangarPorHoras();
}

function hangarPorHoras() {
    let montoHangarPorHoras = CostoHangar.options[CostoHangar.selectedIndex].text * totalHoras.value;
    totalMontoHangar.value = montoHangarPorHoras.toFixed(2);
}

let AreaCopiloto = document.getElementById("AreaCopiloto");
AreaCopiloto.style("background", "#f90");