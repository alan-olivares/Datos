
function llenarSelect(select,valor,texto,json){
  if($(select).is(':empty')){
    $(select).append($('<option>', {
      value: "",
      text: ""
    }));
  }
  for (i = 0; i < json.length; i++) {
    $(select).append($('<option>', {
      value: json[i][valor],
      text: json[i][texto]
    }));
  }
}

function toFloat(valor, decimal) {
    return (Math.round(valor * 100) / 100).toFixed(decimal).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}

async function getCurrentWeek(campo, api) {
    var result = await conexion("GET", api + "currentWeek/", "");
    $(campo).val(result);
}


function conexion(method, url, params) {
    return new Promise(function (resolve, reject) {
        let xhttp = new XMLHttpRequest();
        xhttp.withCredentials = true;
        xhttp.open(method, url, true);
        xhttp.setRequestHeader("Content-type", "application/json;charset=UTF-8");
        //xhttp.setRequestHeader("Authorization", "basic " + btoa(localStorage['usuario'] + ":" + localStorage['contrasena'] + ":" + localStorage['dominio']));
        xhttp.onload = function () {
            if (this.readyState == 4 && this.status == 200) {
                resolve(xhttp.response);
            } else {
                reject({
                    status: this.status,
                    statusText: xhttp.statusText
                });
            }
        };
        xhttp.onerror = function () {
            reject({
                status: this.status,
                statusText: xhttp.statusText
            });
        };
        xhttp.send(params);
    });
}

function setSelectedValue(selectObj, valueToSet) {
    for (var i = 0; i < selectObj.options.length; i++) {
        if (selectObj.options[i].text.includes(valueToSet)) {
            selectObj.options[i].selected = true;
            return;
        }
    }
}
