
function TryParseInt(str) {
	var retorno = 0;
	if (str != null && str != undefined) {
		if (str.length > 0) {
			if (!isNaN(str)) {
				retorno = parseInt(str);
				return retorno;
			}
		}
	}
	return null;
}

function MostrarMensaje(mensaje, tipo){
	var lblError = document.getElementById('LabelError');
	lblError.className = '';
	if (lblError != null && lblError != undefined)
	{
		if (mensaje != null && mensaje != undefined) {
			if (mensaje.trim() != '') {
				lblError.className += tipo == 'error' ? "ErrorVisible" : "MensajeVisible";
				lblError.textContent = mensaje;
			}
		}
	}
	else
		alert(mensaje);
}

function EjecutarApi(metodo, nombresParametros, parametros, data, funcionSuccess, funcionError) {
	var url = "/Api/File/" + metodo.trim() + "?";
	url = FormatearUrl(url, nombresParametros, parametros);

	if (data == null)
		data = new FormData();

	$.ajax({
		type: "POST",
		url: url,
		contentType: false,
		processData: false,
		data: data,
		success: funcionSuccess,
		error: funcionError
	});
}

function EjecutarAjax(accion, nombresParametros, parametros, funcionSuccess, funcionError) {
	var url = "";
	if (accion.includes("/")) {
		url = nombresParametros.length > 0 ? accion.trim() + '?' : accion.trim();
	}
	else
		url = nombresParametros.length > 0 ? "/Home/" + accion.trim() + '?' : "/Home/" + accion.trim();
		
	url = FormatearUrl(url, nombresParametros, parametros);

	$.ajax({
		url: url,
		method: 'get',
		data: {},
		async: true,
		success: funcionSuccess,
		error: funcionError
	});
}

function FormatearUrl(url, nombresParametros, parametros) {
	for (var i = 0; i < nombresParametros.length; i++) {
		url += nombresParametros[i] + '=' + parametros[i];
		if (i < (nombresParametros.length - 1))
			url += '&';
	}
	return url;
}

function ABase64(str) {
	return window.btoa(unescape(encodeURIComponent(str)));
}

function AUTF8(str) {
	return decodeURIComponent(escape(window.atob(str)));
}

function StringIsNullOrEmpty(string) {
	if (string != null && string != undefined) {
		if (string.trim() == '')
			return true;
		else
			return false;
	} else
		return true;
}