// per una descrizione di questa tecnica vedere:
// Jeremy Keith, Dom Scripting pag. 81.
// praticamente esegue un append di tutte le funzioni
// da eseguire quando viene caricata la pagina.
// N.B. windows.onload viene eseguita alla fine del
// caricamento della pagina.
function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != "function") {
        window.onload = func;
    }
    else {
        window.onload = function () {
            if (oldonload) {
                oldonload();
            }
            func();
        };
    }
}

addLoadEvent(function () {
    // inserire qui il codice da eseguire al caricamento della pagina
});

// chiamata al completamento del caricamento della pagina.
$(function () {
    try {
        //alert("qua");
    } catch (e) {
        alert(e.message);
    }
});


// forza il reload della pagina
function PageReload() {
    window.location.href = window.location.href;
}

$("#BtnAdd").click(function() {
    // deve leggere gli elementi selezionati della listbox dei rpodotti
    // e poi aggiungerli alla listbox contenente i prodotti per listino.
    var selcode = [];       // array contenente i codici prodotti selezionati dalla listbox
    var selid = [];         // array contnente gli id prodotti relativi ai codici selezionati
    var s = document.getElementById("lbprodid");        // listbox degli id
    var s1 = document.getElementById("lbproducts");     // listbox dei codici

    //var s = $("#lbprodid");        // listbox degli id
    ///var s1 =$("#lbproducts");     // listbox dei codici

    var s2 = document.getElementById("lbprodplid");     // listbox degli id relativi ai prodotti contenuti nel listino (hidden) 
    var s3 = document.getElementById("lbprodpl");       // listbox dei codici relativi ai prodotti del listino


    //var numel = $("#lbproducts option").length;
    //for (var i = 0; i < numel; i++) {
    //    if ($("#lbproducts option")[i].selected) {
    //        selcode.push(s1.options[i].text);
    //        //selid.push(s.options[i].text);
    //    }
    //}

    // in selcode e in selid soltanto i codici e i relativi id selezionati
    var numel = s1.options.length;
    var i;
    for (i = 0; i < numel; i++) {
        if (s1.options[i].selected) {
            selcode.push(s1.options[i].text);
            selid.push(s.options[i].text);
        }
    }

    // aggiunge i prodotti al listino, toglie dalla lista dei prodotti disponibili
    for (i = 0; i < selid.length; i++) {
        var option = document.createElement("option");
        var option1 = document.createElement("option");
        option.text = selcode[i];
        s3.add(option);
        option1.text = selid[i];
        s2.add(option1);
        s1.remove(option.text);
        s.remove(option1.text);
    }
});



$("#BtnRemove").click(function() {
    //$('#RemoveProduct').CSS('color', "#00ff00");
    alert("quo");
});


$("#BtnSave").click(function () {
    var path = window.location.pathname; 		// e' l'ultima parte del path, compreso il carattere "/" iniziale
    var basepath = SetupBaseRoute(path);
    if (basepath.indexOf("Listino") != -1) {
        // soltanto se e' listino,
        var newpath = basepath + "/RequestUpdateListino";

        var idl = document.getElementById("idlistino").innerHTML;

        var s2 = document.getElementById("lbprodplid");     // listbox degli id relativi ai prodotti contenuti nel listino (hidden) 
        var idtosend = [];
        var numel = s2.options.length;
        //$.each(s.option()).push
        //{
            
        //}
        for (var i = 0; i < numel; i++) {
            idtosend.push(s2.options[i].text);
        }

        // serializza l'oggetto
        var jsonlist = JSON.stringify(idtosend);

        $.post(newpath, { idlistino: idl, jsonids: jsonlist })};
        //$.post(newpath);

        //$.post(newpath, null, UpdateKibsControls, "json");
});



// Ritorna soltanto l'url del controller. Utilizzato per costruire una nuova
// chiamata a una action dello stesso controller, via javascript
function SetupBaseRoute(partialpath) {
    try {
        var result = "";
        var index;
        if (partialpath == "/") {
            result = "../../Home";
        }
        else {
            while (1) {
                if ((index = partialpath.lastIndexOf("/")) == 0) {
                    result = "../.." + partialpath;
                    break;
                }
                else if (index > 0) {
                    partialpath = partialpath.substring(0, index);
                }
                else {
                    alert("errore: partialpath=" + partialpath);
                    break;
                }
            }
        }
        return result;
    } catch (e) {
        alert(e.message);
    }
}
