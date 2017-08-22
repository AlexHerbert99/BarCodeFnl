$("#btnDetalle").click(function (eve) {

    $("#modal-content").load("/Productos/Details/" + $(this).data("id"));

});

$(".btnBorrar").click(function (eve) {

    $("#modal-content").load("/PRODUCTOes/Delete/" + $(this).data("id"));

});


$("#btnNuevo").click(function (eve) {

    $("#modal-content").load("/Productos/Details/");

});
